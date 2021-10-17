using Microsoft.AspNetCore.Mvc;

namespace Brewery.Controllers
{
    public class ODataControllerBase : Controller
    {
        // GET
        public IActionResult Index()
        {
            return View();
        }
    }
}