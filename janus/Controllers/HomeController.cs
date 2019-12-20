using Microsoft.AspNetCore.Mvc;

namespace overapp.janus.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return new RedirectResult("~/swagger");
        }
    }
}
