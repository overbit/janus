using System.Threading.Tasks;
using overapp.janus.Models.Domain;

namespace overapp.janus.Infrastructure.Repositories
{
    public interface IMerchantRepository
    {
        Task<Merchant> Get(string clientId);
    }
}
