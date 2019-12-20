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

        /// <summary>
        /// Get the list of payment processed in behalf of the merchant
        /// </summary>
        /// <param name="client_id">Unique id per merchant</param>
        /// <param name="client_secret">Unique secret between client and server</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TransactionDetails>>> ListTransactions([FromHeader]string client_id,
                                                                                          [FromHeader]string client_secret,
                                                                                          [FromQuery]int? skip = null, 
                                                                                          [FromQuery]int? take = null)
        {
            return NotFound();
        }

        /// <summary>
        /// Get details of a processed payment (transaction)
        /// </summary>
        /// <param name="client_id">Unique id per merchant</param>
        /// <param name="client_secret">Unique secret between client and server</param>
        /// <param name="id">Payment / transaction id</param>
        /// <returns></returns>
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

        /// <summary>
        /// Process a new payment (transaction)
        /// </summary>
        /// <param name="client_id">Unique id per merchant</param>
        /// <param name="client_secret">Unique secret between client and server</param>
        /// <param name="request">Details of the transaction to process</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<TransactionResult>> ProcessTransaction([FromHeader]string client_id, 
                                                                              [FromHeader]string client_secret, 
                                                                              [FromBody]TransactionRequest request)
        {
            // Authenticate merchant before proceeding 
            return await paymentManager.ProcessPayment(client_id, request);
        }
    }
}