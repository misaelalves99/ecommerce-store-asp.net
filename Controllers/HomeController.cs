// Caminho: Controllers/HomeController.cs
using Microsoft.AspNetCore.Mvc;

namespace ECommerceStore.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
