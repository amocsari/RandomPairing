using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using RandomPairer.Bll.Pairing;
using RandomPairer.Transfer.Pairing;

namespace RandomPairer.Api.Controllers
{
    [Route("api/[controller]/[Action]")]
    [ApiController]
    public class PairingController : ControllerBase
    {
        private readonly IPairingService pairingService;

        public PairingController(IPairingService airportService)
        {
            this.pairingService = airportService;
        }

        [HttpGet]
        public Task<PairingDto> Pair(string name)
        {
            return pairingService.Pair(name);
        }

        [HttpPost]
        public Task AddUser(string name, string adminPassword)
        {
            return pairingService.AddUser(name, adminPassword);
        }

        [HttpGet]
        public Task<string> GetPair(string userSecret)
        {
            return pairingService.GetPair(userSecret);
        }

        [HttpGet]
        public Task<string> GetSecretForUser(string name, string adminPassword)
        {
            return pairingService.GetSecretForUser(name, adminPassword);
        }

        [HttpDelete]
        public Task DeleteUser(string name, string adminPassword)
        {
            return pairingService.DeleteUser(name, adminPassword);
        }

        [HttpDelete]
        public Task ClearPairings(string adminPassword)
        {
            return pairingService.ClearPairings(adminPassword);
        }
    }
}
