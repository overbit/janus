using Microsoft.AspNetCore.Mvc;

namespace overapp.janus.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class JanusController : Controller
    {
        // GET: /<controller>/
        [HttpGet]
        [Route("")]
        public IActionResult List()
        {
            return View();
        }

        public IActionResult UnAuthorised()
        {
            re
        }
    }
}
