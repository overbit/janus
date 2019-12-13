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
        [Route("{apikey}/")]
        public async Task<ActionResult<IEnumerable<TransactionDetails>>> List(string apikey)
        {
            return View();
        }

        [HttpPost]
        [Route("{apikey}/")]
        public async Task<ActionResult<TransactionDetails>> ProcessTransaction(TransactionRequest request)
        {
            return View();
        }

        public IActionResult UnAuthorised()
        {
            return new UnauthorizedResult();
        }
    }
}
