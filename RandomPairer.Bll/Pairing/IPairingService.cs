using RandomPairer.Transfer.Pairing;
using System.Threading.Tasks;

namespace RandomPairer.Bll.Pairing
{
    public interface IPairingService
    {
        Task<PairingDto> Pair(string name);
        Task ClearPairings(string adminPassword);
        Task<string> GetSecretForUser(string name, string adminPassword);
        Task AddUser(string name, string adminPassword);
        Task DeleteUser(string name, string adminPassword);
        Task<string> GetPair(string userSecret);
    }
}
