using AutoMapper;
using RandomPairer.Dal.Context;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.Extensions.Options;
using RandomPairer.Common.Options;
using RandomPairer.Bll.Extensions;
using System;
using RandomPairer.Transfer.Pairing;
using RandomPairer.Common.Exceptions;

namespace RandomPairer.Bll.Pairing
{
    public class PairingService : IPairingService
    {
        private readonly RandomPairerDbContext dbContext;
        private readonly IMapper mapper;
        private readonly AdminOptions adminOptions;

        public PairingService(RandomPairerDbContext dbContext, IMapper mapper, IOptions<AdminOptions> adminOptions)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
            this.adminOptions = adminOptions.Value;
        }

        public async Task AddUser(string name, string adminPassword)
        {
            if (adminOptions.AdminPassword != adminPassword)
                throw new RandomPairerValidationException("Incorrect Admin Password!");

            var alreadyAdded = await dbContext.User.AsNoTracking().AnyAsync(n => n.Name == name.ToLower());

            if (alreadyAdded)
                throw new RandomPairerValidationException($"User already added: {name}");

            dbContext.User.Add(new Dal.Entities.User
            {
                Name = name
            });

            await dbContext.SaveChangesAsync();
        }

        public async Task ClearPairings(string adminPassword)
        {
            if (adminOptions.AdminPassword != adminPassword)
                throw new RandomPairerValidationException("Incorrect Admin Password!");

            var users = await dbContext.User.ToListAsync();

            foreach(var user in users)
            {
                user.Secret = null;
                user.Pair = null;
                user.InversePair.Clear();
            }

            await dbContext.SaveChangesAsync();
        }

        public async Task DeleteUser(string name, string adminPassword)
        {
            if (adminOptions.AdminPassword != adminPassword)
                throw new RandomPairerValidationException("Incorrect Admin Password!");

            var user = await dbContext.User.AsNoTracking().FirstOrDefaultAsync(n => n.Name.ToLower() == name.ToLower())
                ?? throw new RandomPairerValidationException($"Unknown User: {name}");

            dbContext.User.Remove(user);

            await dbContext.SaveChangesAsync();
        }

        public async Task<string> GetPair(string userSecret)
        {
            var user = await dbContext.User.AsNoTracking().Include(u => u.Pair).FirstOrDefaultAsync(u => u.Secret == userSecret)
                ?? throw new RandomPairerValidationException($"Invalid secret: {userSecret}.\nIf you forgot your secret, contact Mocsi to retrieve it.\n(Or try cracking the admin password and retrieve it yourself :P)");

            return user.Pair.Name;
        }

        public async Task<string> GetSecretForUser(string name, string adminPassword)
        {
            if (adminOptions.AdminPassword != adminPassword)
                throw new RandomPairerValidationException("Incorrect Admin Password!");

            var user = await dbContext.User.AsNoTracking().FirstOrDefaultAsync(n => n.Name.ToLower() == name.ToLower())
                ?? throw new RandomPairerValidationException($"Unknown User: {name}");

            if (string.IsNullOrEmpty(user.Secret))
            {
                return "User has no secret!";
            }

            return user.Secret;
        }

        public async Task<PairingDto> Pair(string name)
        {
            var names = await dbContext.User.Include(n => n.Pair).Include(n => n.InversePair).AsNoTracking().ToListAsync();

            var namesString = string.Join(", ", names.Select(n => n.Name));

            var user = await dbContext.User.Include(n => n.Pair).FirstOrDefaultAsync(n => n.Name.ToLower() == name.ToLower())
                ?? throw new RandomPairerValidationException($"Unknown User: {name}\nAvailable names: {namesString}");

            if (user.Pair != null)
                throw new RandomPairerValidationException($"User {name} is already paired with someone! To find out, who you are paired with, user the \"GetPair\" command");

            var pair = names.Where(n => n.InversePair.Count == 0 && n.Name.ToLower() != name.ToLower()).ToList().GetRandomElement();

            var secret = Guid.NewGuid().ToString();

            user.PairId = pair.Id;
            user.Secret = secret;

            await dbContext.SaveChangesAsync();

            var pairing = mapper.Map<PairingDto>(user);
            pairing.Whom = pair.Name;

            return pairing;
        }
    }
}
