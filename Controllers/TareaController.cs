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
    private readonly IUsuarioRepository usuarioRepository;
    private readonly ITableroRepository tableroRepository;

    public TareaController(ILogger<TareaController> logger, ITareaRepository _tareaRepository, IUsuarioRepository _usuarioRepository, ITableroRepository _tableroRepository)
    {
        _logger = logger;
        tareaRepository = _tareaRepository;
        usuarioRepository = _usuarioRepository;
        tableroRepository = _tableroRepository;
    }


    // Endpoints

    // Listar tareas

    public IActionResult Index()
    {
        try
        {
            if(!User.Identity.IsAuthenticated && HttpContext.Session.GetString("rol") != "administrador" && HttpContext.Session.GetString("rol") != "operador") return RedirectToLogin();
            
            int idUsuario = int.Parse(HttpContext.Session.GetString("id"));

            List<Tarea> tareas = new();
            List<Tarea> tareasAsignadas = new();
            List<Tarea> tareasCreadas = new();
            List<Tablero> tableros = new();

            if(isAdmin())
            {
                tareas = tareaRepository.GetAll();
                return View(new ListarTareasViewModel(tareas, idUsuario));
            }
            else
            {
                tareas = tareaRepository.GetAll();
                tareasAsignadas = tareaRepository.GetByUsuarioId(idUsuario);
                tableros = tableroRepository.GetByUserId(idUsuario);

                /* foreach (Tarea task in tareas)
                {
                    int idTablero = task.IdTablero;
                    foreach (Tablero board in tableros)
                    {
                        if(board.Id == idTablero)
                        {
                            tareasCreadas.Add(task);
                        }
                    }
                } */

                tareasCreadas.AddRange(tareas.Where(task => tableros.Any(board => board.Id == task.IdTablero)));
                return View(new ListarTareasViewModel(tareasAsignadas, tareasCreadas, idUsuario));
            }
        }
        catch(Exception ex)
        {
            _logger.LogError(ex.ToString());
            return BadRequest();
        } 
    }

    public IActionResult ShowTasksOnBoard(int idTablero, int idUsuario)
    {
        try
        {
            if(!User.Identity.IsAuthenticated && HttpContext.Session.GetString("rol") != "administrador" && HttpContext.Session.GetString("rol") != "operador") return RedirectToLogin();

            List<Tarea> tareas = tareaRepository.GetByTableroId(idTablero);
            
            return View(new ListarTareasViewModel(tareas, idUsuario));
            
        }
        catch(Exception ex)
        {
            _logger.LogError(ex.ToString());
            return BadRequest();
        } 
    }

    // Crear tarea

    public IActionResult Create(int idUsuario) 
    {
        try
        {
            List<Usuario> usuarios = usuarioRepository.GetAll();
            List<Tablero> tableros = new();

            if(isAdmin()) 
            {
                tableros = tableroRepository.GetAll();
            }
            else 
            {
                tableros = tableroRepository.GetByUserId(idUsuario);
            }

            return View(new CrearTareaViewModel(usuarios, tableros));
        }
        catch(Exception ex)
        {
            _logger.LogError(ex.ToString());
            return BadRequest();
        }
    }

    [HttpPost]
    public IActionResult Create(CrearTareaViewModel tareaVM)
    {
        try
        {
            if(!ModelState.IsValid) return RedirectToAction("Index");

            var tarea = new Tarea(tareaVM);
            tareaRepository.Create(tarea.IdTablero, tarea);

            return RedirectToAction("Index");
        }
        catch(Exception ex)
        {
            _logger.LogError(ex.ToString());
            return BadRequest();
        }
    }

    // Modificar tarea

    public IActionResult Update(int idTarea, int idUsuario, int taskType)
    {
        try
        {
            List<Usuario> usuarios = usuarioRepository.GetAll();
            List<Tablero> tableros = new();

            if(isAdmin())
            {
                tableros = tableroRepository.GetAll();
            }
            else 
            {
                tableros = tableroRepository.GetByUserId(idUsuario);
            }

            var tarea = tareaRepository.GetById(idTarea);
            var tareaVM = new ModificarTareaViewModel(tarea, usuarios, tableros);

            switch (taskType)
            {
                case 0:
                case 1:
                    return View("Update", tareaVM);
                case 2:
                    return View("UpdateAssignedTask", tareaVM);
                default:
                    return BadRequest(); // Agregamos una ruta por defecto para manejar otros valores de taskType
            }
        }
        catch(Exception ex)
        {
            _logger.LogError(ex.ToString());
            return BadRequest();
        }
    }

    [HttpPost]
    public IActionResult Update(ModificarTareaViewModel tareaVM)
    {
        try
        {
            if(!ModelState.IsValid) return RedirectToAction("Index");

            var tarea = new Tarea(tareaVM);
            tareaRepository.Update(tarea.Id, tarea);

            return RedirectToAction("Index");
        }
        catch(Exception ex)
        {
            _logger.LogError(ex.ToString());
            return BadRequest();
        }
    }

    // Eliminar tarea

    public IActionResult Delete(int idTarea)
    {
        try
        {
            return View(tareaRepository.GetById(idTarea));
        }
        catch(Exception ex)
        {
            _logger.LogError(ex.ToString());
            return BadRequest();
        }
    }

    [HttpPost]
    public IActionResult DeleteConfirmed(int id)
    {
        try
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
        catch(Exception ex)
        {
            _logger.LogError(ex.ToString());
            return BadRequest();
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
