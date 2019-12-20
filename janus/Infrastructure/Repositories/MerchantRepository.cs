using overapp.janus.Models.Domain;
using System.Threading.Tasks;

namespace overapp.janus.Infrastructure.Repositories
{
    public class MerchantRepository : IMerchantRepository
    {
        public Task<Merchant> Get(string clientId)
        {
            throw new System.NotImplementedException();
        }

        public Task<Merchant> Get(string clientId, string clientSecret)
        {
            throw new System.NotImplementedException();
        }
    }
}
