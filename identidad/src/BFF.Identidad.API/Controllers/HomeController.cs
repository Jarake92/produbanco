using Microsoft.AspNetCore.Mvc;

namespace BFF.Identidad.API.Controllers;

public class HomeController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}