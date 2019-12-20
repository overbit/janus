using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using overapp.janus.Managers;
using overapp.janus.Models.Dtos.Request;
using overapp.janus.Models.Dtos.Response;

namespace overapp.janus.Controllers
{
    [ApiController]
    [Route("payment")]
    public class JanusController : Controller
    {
        private readonly IPaymentManager paymentManager;

        public JanusController(IPaymentManager paymentManager)
        {
            this.paymentManager = paymentManager;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TransactionDetails>>> ListTransactions([FromHeader]string client_id,
                                                                                          [FromHeader]string client_secret)
        {

            return new List<TransactionDetails>();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TransactionDetails>> GetTransaction([FromHeader]string client_id,
                                                                           [FromHeader]string client_secret,
                                                                           [FromRoute]string id)
        {
            if (Guid.TryParseExact(id, "N", out var transactionGuid))
            {
                var transaction = await paymentManager.GetPaymentDetails(client_id, transactionGuid);

                if (transaction != null)
                {
                    return transaction;
                }
            }
            
            return new NotFoundResult();
        }

        // TODO Validate CurrencyCode against RegionInfo 
        [HttpPost]
        public async Task<ActionResult<TransactionResult>> ProcessTransaction([FromHeader]string client_id, 
                                                                              [FromHeader]string client_secret, 
                                                                              [FromBody]TransactionRequest request)
        {

            // Authenticate merchant before

            return await paymentManager.ProcessPayment(client_id, request);
        }
    }
}