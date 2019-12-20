using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using overapp.janus.Models.Domain;

namespace overapp.janus.Infrastructure.Repositories
{
    public class PaymentRepository : IPaymentRepository
    {
        public Task<Transaction> Get(Guid paymentGuid)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Transaction>> List(IEnumerable<Guid> paymentsGuid)
        {
            throw new NotImplementedException();
        }
    }
}