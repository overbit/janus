using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using overapp.janus.Managers;
using overapp.janus.Models.Dtos.Request;
using overapp.janus.Models.Dtos.Response;
using Swashbuckle.AspNetCore.Annotations;

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
        /// Get details of a processed payment (transaction)
        /// </summary>
        /// <param name="client_id">Unique id per merchant</param>
        /// <param name="client_secret">Unique secret between client and server</param>
        /// <param name="id">Payment / transaction id</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [SwaggerResponse(200, type:typeof(TransactionDto))]
        [SwaggerResponse(400, "Payment / transaction id is invalid")]
        [SwaggerResponse(404, "Payment / transaction id is not found")]
        public async Task<ActionResult<TransactionDto>> GetTransaction([FromHeader]string client_id,
                                                                       [FromHeader]string client_secret,
                                                                       [FromRoute]string id)
        {
            if(!Guid.TryParseExact(id, "N", out var tmp))
            {
                return new BadRequestResult();
            }

            var transaction = await paymentManager.GetPaymentDetails(client_id, id);

            if (transaction != null)
            {
                return transaction;
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
        [SwaggerResponse(200, type: typeof(TransactionResultDto))]
        [SwaggerResponse(400, "Invalid transaction id is invalid")]
        public async Task<ActionResult<TransactionResultDto>> ProcessTransaction([FromHeader]string client_id, 
                                                                              [FromHeader]string client_secret, 
                                                                              [FromBody]TransactionRequest request)
        {
            // Authenticate merchant before proceeding 
            return await paymentManager.ProcessPayment(client_id, request);
        }
    }
}