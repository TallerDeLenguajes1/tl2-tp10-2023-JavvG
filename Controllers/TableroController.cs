using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using tl2_tp10_2023_JavvG.Models;
using tl2_tp10_2023_JavvG.Repositories;
using tl2_tp10_2023_JavvG.ViewModels;

namespace tl2_tp10_2023_JavvG.Controllers;

public class TableroController : Controller
{
    private readonly ILogger<TableroController> _logger;
    private readonly ITableroRepository tableroRepository;

    public TableroController(ILogger<TableroController> logger)
    {
        _logger = logger;
        tableroRepository = new TableroRepository();
    }

    // Endpoints

    // Listar tableros

    public IActionResult Index() 
    {
        if(!User.Identity.IsAuthenticated && HttpContext.Session.GetString("rol") != "administrador" && HttpContext.Session.GetString("rol") != "operador") return RedirectToLogin();
        if(isAdmin())
        {
            var tableros = tableroRepository.GetAll();
            return View(new ListarTablerosViewModel(tableros));
        }
        else 
        {
            var tableros = tableroRepository.GetByUserId(int.Parse(HttpContext.Session.GetString("id")));
            return View(new ListarTablerosViewModel(tableros));
        } 
    }

    // Crear tablero

    public IActionResult Create() 
    {
        if(!User.Identity.IsAuthenticated && HttpContext.Session.GetString("rol") != "administrador" && HttpContext.Session.GetString("rol") != "operador") return RedirectToLogin();
        return View(new CrearTableroViewModel());
    }

    [HttpPost]
    public IActionResult Create(CrearTableroViewModel tableroVM)
    {
        if(!ModelState.IsValid) return RedirectToAction("Index");
        if(!User.Identity.IsAuthenticated && HttpContext.Session.GetString("rol") != "administrador" && HttpContext.Session.GetString("rol") != "operador") return RedirectToLogin();
        var tablero = new Tablero(tableroVM);
        tableroRepository.Create(tablero);
        return RedirectToAction("Index");
    }

    // Modificar tablero

    public IActionResult Update(int id) 
    {
        if(!User.Identity.IsAuthenticated && HttpContext.Session.GetString("rol") != "administrador" && HttpContext.Session.GetString("rol") != "operador") return RedirectToLogin();
        var tablero = tableroRepository.GetById(id);
        return View(new ModificarTableroViewModel(tablero));
    }

    [HttpPost]
    public IActionResult Update(int id, ModificarTableroViewModel tableroVM)
    {
        if(!ModelState.IsValid) return RedirectToAction("Index");
        if(!User.Identity.IsAuthenticated && HttpContext.Session.GetString("rol") != "administrador" && HttpContext.Session.GetString("rol") != "operador") return RedirectToLogin();
        var tablero = new Tablero(tableroVM);
        tableroRepository.Update(id, tablero);
        return RedirectToAction("Index");
    }

    
    // Eliminar tablero

    public IActionResult Delete(int id) 
    {
        return View(tableroRepository.GetById(id));
    }

    [HttpPost]
    public IActionResult DeleteConfirmed(int id)
    {

        if(tableroRepository.Delete(id) > 0)
        {
            return RedirectToAction("Index");
        }
        else 
        {
            return RedirectToAction("Error");
        }
    }

    // Método que verifica que el usuario logueado sea 'administrador'

    private bool isAdmin()
    {
        return (HttpContext.Session != null && HttpContext.Session.GetString("rol") == "administrador");
    }

    // Método que redirecciona a la pantalla de inicio mostrando un mensaje de error correspondiente

    private IActionResult RedirectOperatorUser()
    {
        TempData["ErrorMessage"] = "No puedes acceder a este sitio porque no eres administrador";        // Almacena el mensaje en TempData (se utiliza para pasar datos entre acciones durante redirecciones) para mostrarlo en la página de inicio
        return RedirectToRoute(new { controller = "Tablero", action = "Index" });
    }

    // Método que redirecciona a la pantalla de inicio de sesión

    private IActionResult RedirectToLogin()
    {
        TempData["ErrorMessage"] = "Inicie sesión antes de acceder a este sitio";
        return RedirectToRoute(new { controller = "Login", action = "Index" });
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}