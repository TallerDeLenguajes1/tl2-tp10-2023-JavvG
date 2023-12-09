using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using tl2_tp10_2023_JavvG.Models;
using tl2_tp10_2023_JavvG.Repositories;
using tl2_tp10_2023_JavvG.ViewModels;

namespace tl2_tp10_2023_JavvG.Controllers;

public class TareaController : Controller
{
     private readonly ILogger<TareaController> _logger;
    private readonly ITareaRepository tareaRepository;

    public TareaController(ILogger<TareaController> logger)
    {
        _logger = logger;
        tareaRepository = new TareaRepository();
    }

    // Endpoints

    // Listar tareas

    public IActionResult Index()
    {
        if (!isAdmin()) return RedirectToRoute(new { controller = "Login", action = "Index" });
        var tareas = tareaRepository.GetAll();
        var tareasVM = new ListarTareasViewModel(tareas);
        return View(tareasVM);
    }

    // Crear tarea

    [HttpGet]
    public IActionResult Create() 
    {
        if(!isAdmin()) return RedirectToAction("Index");
        return View(new CrearTareaViewModel());
    }

    [HttpPost]
    public IActionResult Create(CrearTareaViewModel tareaVM)
    {
        if(!ModelState.IsValid) return RedirectToAction("Index");
        if(!isAdmin()) return RedirectToAction("Index");
        
        var tarea = new Tarea(tareaVM);
        tareaRepository.Create(1, tarea);

        return RedirectToAction("Index");
    }

    // Modificar tarea

    [HttpGet]
    public IActionResult Update(int id)
    {
        return View(tareaRepository.GetById(id));
    }

    [HttpPost]
    public IActionResult Update(int id, Tarea tarea)
    {
        tareaRepository.Update(id, tarea);
        return RedirectToAction("Index");
    }

    // Eliminar tarea

    [HttpGet]
    public IActionResult Delete(int id)
    {
        return View(tareaRepository.GetById(id));
    }

    [HttpPost]
    public IActionResult DeleteConfirmed(int id)
    {

        if(tareaRepository.Delete(id) > 0)
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
