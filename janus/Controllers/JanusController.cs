using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using overapp.janus.Models.Dtos.Request;
using overapp.janus.Models.Dtos.Response;

namespace overapp.janus.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class JanusController : Controller
    {
        // GET: /<controller>/
        [HttpGet]
        [Route("{merchantkey}")]
        public async Task<ActionResult<IEnumerable<TransactionDetails>>> ListTransactions([FromRoute]string merchantkey)
        {
            return new List<TransactionDetails>();
        }

        [HttpPost]
        [Route("{merchantkey}")]
        public async Task<ActionResult<TransactionDetails>> ProcessTransaction([FromRoute]string merchantkey, [FromBody]TransactionRequest request)
        { 
            // Success
            return new OkResult();
        }
    }
}
