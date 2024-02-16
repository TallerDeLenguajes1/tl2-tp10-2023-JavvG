using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using tl2_tp10_2023_JavvG.Models;

namespace tl2_tp10_2023_JavvG.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        try
        {
            // Se verifica que el usuario esté logueado correctamente

            if(HttpContext.Session.GetString("rol") != "administrador" && HttpContext.Session.GetString("rol") != "operador") 
            {
                TempData["ErrorMessage"] = "Inicie sesión antes de acceder a este sitio";
                return RedirectToRoute(new { controller = "Login", action = "Index" });
            }
            else
            {
                if(HttpContext.Session.GetString("rol") == "administrador") return View("IndexAdministratorUser");
                return View("IndexOperatorUser");
            }
        }
        catch(Exception ex)
        {
            _logger.LogError(ex.ToString());
            return BadRequest();
        }
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
