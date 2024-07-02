using Microsoft.AspNetCore.Mvc;

namespace Zoro.Web.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class AboutUsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
