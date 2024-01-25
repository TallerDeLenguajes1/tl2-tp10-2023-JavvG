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
    private readonly IUsuarioRepository usuarioRepository;

    public TableroController(ILogger<TableroController> logger, ITableroRepository _tableroRepository, IUsuarioRepository _usuarioRepository)
    {
        _logger = logger;
        tableroRepository = _tableroRepository;
        usuarioRepository = _usuarioRepository;
    }

    // Endpoints

    // Listar tableros

    public IActionResult Index() 
    {
        try
        {
            if(!User.Identity.IsAuthenticated && HttpContext.Session.GetString("rol") != "administrador" && HttpContext.Session.GetString("rol") != "operador") return RedirectToLogin();

            int idUsuario = int.Parse(HttpContext.Session.GetString("id"));

            if(isAdmin())
            {
                var tableros = tableroRepository.GetAll();
                return View(new ListarTablerosViewModel(tableros, idUsuario));
            }
            else 
            {
                var tableros = tableroRepository.GetByUserId(idUsuario);
                return View(new ListarTablerosViewModel(tableros, idUsuario));
            } 
        }
        catch(Exception ex)
        {
            _logger.LogError(ex.ToString());
            return BadRequest();
        }
    }

    // Crear tablero

    public IActionResult Create(int idUsuario) 
    {
        try
        {
            if(!User.Identity.IsAuthenticated && HttpContext.Session.GetString("rol") != "administrador" && HttpContext.Session.GetString("rol") != "operador") return RedirectToLogin();

            List<Usuario> usuarios = usuarioRepository.GetAll();

            return View(new CrearTableroViewModel(idUsuario, usuarios));
        }
        catch(Exception ex)
        {
            _logger.LogError(ex.ToString());
            return BadRequest();
        }
    }

    [HttpPost]
    public IActionResult Create(CrearTableroViewModel tableroVM)
    {
        try
        {
            if(!ModelState.IsValid) return RedirectToAction("Index");
            if(!User.Identity.IsAuthenticated && HttpContext.Session.GetString("rol") != "administrador" && HttpContext.Session.GetString("rol") != "operador") return RedirectToLogin();
            var tablero = new Tablero(tableroVM);
            tableroRepository.Create(tablero);
            return RedirectToAction("Index");
        }
        catch(Exception ex)
        {
            _logger.LogError(ex.ToString());
            return BadRequest();
        }
    }

    // Modificar tablero

    public IActionResult Update(int idTablero, int idUsuario) 
    {
        try
        {
            if(!User.Identity.IsAuthenticated && HttpContext.Session.GetString("rol") != "administrador" && HttpContext.Session.GetString("rol") != "operador") return RedirectToLogin();

            var tablero = tableroRepository.GetById(idTablero);
            var usuarios = usuarioRepository.GetAll();

            return View(new ModificarTableroViewModel(tablero, idUsuario, usuarios));
        }
        catch(Exception ex)
        {
            _logger.LogError(ex.ToString());
            return BadRequest();
        }
    }

    [HttpPost]
    public IActionResult Update(ModificarTableroViewModel tableroVM)
    {
        try
        {
            if(!ModelState.IsValid) return RedirectToAction("Index");
            if(!User.Identity.IsAuthenticated && HttpContext.Session.GetString("rol") != "administrador" && HttpContext.Session.GetString("rol") != "operador") return RedirectToLogin();
            var tablero = new Tablero(tableroVM);
            tableroRepository.Update(tablero.Id, tablero);
            return RedirectToAction("Index");
        }
        catch(Exception ex)
        {
            _logger.LogError(ex.ToString());
            return BadRequest();
        }
    }

    
    // Eliminar tablero

    public IActionResult Delete(int id) 
    {
        try
        {
            return View(tableroRepository.GetById(id));
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
            if(tableroRepository.Delete(id) > 0)
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