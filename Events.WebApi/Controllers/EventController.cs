using Microsoft.AspNetCore.Mvc;

namespace Events.WebApi.Controllers
{
    public class EventController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
