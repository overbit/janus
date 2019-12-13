using System.Collections.Generic;
using System.Threading.Tasks;
using System.Xml.Serialization;
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
        public async Task<ActionResult<IEnumerable<TransactionDetails>>> ListTransactions(string merchantkey)
        {
            return new List<TransactionDetails>();
        }

        [HttpPost]
        [Route("{merchantkey}")]
        public async Task<ActionResult<TransactionDetails>> ProcessTransaction(string merchantkey, TransactionRequest request)
        {
            return new TransactionDetails();
        }
    }
}
