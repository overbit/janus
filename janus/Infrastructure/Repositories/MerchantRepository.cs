using Microsoft.EntityFrameworkCore;
using overapp.janus.Models.Domain;
using System.Linq;
using System.Threading.Tasks;

namespace overapp.janus.Infrastructure.Repositories
{
    public class MerchantRepository : IMerchantRepository
    {
        private readonly JanusContext context;

        public MerchantRepository(JanusContext context)
        {
            this.context = context;
        }

        public async Task<Merchant> Get(string clientId)
        {
            return await context.Merchants.Where(m => m.ClientId == clientId).FirstAsync();
        }
    }
}
