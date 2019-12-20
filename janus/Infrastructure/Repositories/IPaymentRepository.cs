using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using overapp.janus.Models.Domain;

namespace overapp.janus.Infrastructure.Repositories
{
    public interface IPaymentRepository
    {
        Task<Transaction> Get(Guid paymentGuid);
        
        Task<IEnumerable<Transaction>> List(IEnumerable<Guid> paymentsGuid);
    }
}