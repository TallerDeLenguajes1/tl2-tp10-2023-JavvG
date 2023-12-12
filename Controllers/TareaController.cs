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

    public TareaController(ILogger<TareaController> logger, ITareaRepository _tareaRepository)
    {
        _logger = logger;
        tareaRepository = _tareaRepository;
    }

    // Endpoints

    // Listar tareas

    public IActionResult Index()
    {
        if(!User.Identity.IsAuthenticated && HttpContext.Session.GetString("rol") != "administrador" && HttpContext.Session.GetString("rol") != "operador") return RedirectToLogin();
        if(isAdmin())
        {
            var tareas = tareaRepository.GetAll();
            return View(new ListarTareasViewModel(tareas));
        }
        else 
        {
            var tareas = tareaRepository.GetByUsuarioId(int.Parse(HttpContext.Session.GetString("id")));
            return View(new ListarTareasViewModel(tareas));
        } 
    }

    // Crear tarea

    public IActionResult Create() 
    {
        if(!isAdmin()) return RedirectOperatorUser();
        return View(new CrearTareaViewModel());
    }

    [HttpPost]
    public IActionResult Create(CrearTareaViewModel tareaVM)
    {
        if(!ModelState.IsValid) return RedirectToAction("Index");
        if(!isAdmin()) return RedirectOperatorUser();

        var tarea = new Tarea(tareaVM);
        tareaRepository.Create(1, tarea);

        return RedirectToAction("Index");
    }

    // Modificar tarea

    public IActionResult Update(int id)
    {
        if(!isAdmin()) return RedirectOperatorUser();

        var tarea = tareaRepository.GetById(id);
        var tareaVM = new ModificarTareaViewModel(tarea);

        return View(tareaVM);
    }

    [HttpPost]
    public IActionResult Update(int id, ModificarTareaViewModel tareaVM)
    {
        if(!ModelState.IsValid) return RedirectToAction("Index");
        if(!isAdmin()) return RedirectOperatorUser();

        var tarea = new Tarea(tareaVM);
        tareaRepository.Update(id, tarea);

        return RedirectToAction("Index");
    }

    // Eliminar tarea

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
