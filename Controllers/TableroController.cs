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
        if(HttpContext.Session != null) 
        {
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
        else 
        {
            return RedirectToRoute(new { controller = "Login", action = "Index" });
        }  
    }

    // Crear tablero

    [HttpGet]
    public IActionResult Create() 
    {
        if(HttpContext.Session == null) return RedirectToRoute(new { controller = "Login", action = "Index" });
        return View(new CrearTableroViewModel());
    }

    [HttpPost]
    public IActionResult Create(CrearTableroViewModel tableroVM)
    {
        if(HttpContext.Session != null)
        {
            if(isAdmin())
            {
                var tablero = new Tablero(tableroVM);
                tableroRepository.Create(tablero);
                return RedirectToAction("Index");
            }
            else
            {
                var tablero = new Tablero(tableroVM);
                tableroRepository.Create(tablero);
                return RedirectToAction("Index");
            }
        }
        else
        {
            return RedirectToRoute(new { controller = "Login", action = "Index" });
        }

    }

    // Modificar tablero

    [HttpGet]
    public IActionResult Update(int id) 
    {
        return View(tableroRepository.GetById(id));
    }

    [HttpPost]
    public IActionResult Update(int id, Tablero tablero)
    {
        tableroRepository.Update(id, tablero);
        return RedirectToAction("Index");
    }

    
    // Eliminar tablero

    [HttpGet]
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

    // Función que verifica que el usuario logueado sea 'administrador'

    private bool isAdmin()
    {
        return (HttpContext.Session != null && HttpContext.Session.GetString("rol") == "administrador");
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}