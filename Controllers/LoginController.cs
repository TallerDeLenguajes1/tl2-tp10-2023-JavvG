using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using tl2_tp10_2023_JavvG.Models;
using tl2_tp10_2023_JavvG.Repositories;
using tl2_tp10_2023_JavvG.ViewModels;

namespace tl2_tp10_2023_JavvG.Controllers;

public class LoginController : Controller
{

    private readonly ILogger<LoginController> _logger;
    private readonly IUsuarioRepository usuarioRepository;

    public LoginController(ILogger<LoginController> logger, IUsuarioRepository _usuarioRepository)
    {
        _logger = logger;
        usuarioRepository = _usuarioRepository;
    }

    // Endpoints

    public IActionResult Index()
    {
        return View();
    }

    // Endpoint de control de inicio de sesión

    [HttpPost]     
    public IActionResult Login(LoginViewModel usuario)
    {
        try 
        {
            var usuarioLogueado = usuarioRepository.GetLoggedUser(usuario.NombreUsuario, usuario.Password);

            if(usuarioLogueado.Nombre == null) 
            {
                // Acceso rechazado
                _logger.LogWarning($"Intento de acceso inválido - Usuario: {usuario.NombreUsuario} - Clave ingresada: {usuario.Password}");

                return RedirectToAction("Index");       // Si el usuario no existe, retorna a 'Index'
            }
            else 
            {
                // Acceso exitoso
                _logger.LogInformation($"El usuario {usuario.NombreUsuario} ingresó correctamente.");

                LoguearUsuario(usuarioLogueado);        // Registrar el usuario

                if (HttpContext.Session.GetString("rol") == Rol.administrador.ToString())
                {
                    return RedirectToRoute(new { controller = "Usuario", action = "Index" });
                }
                else
                {
                    return RedirectToRoute(new { controller = "Tablero", action = "Index" });
                }
            }
        }
        catch (Exception ex)
        {
            // Loggeo de errores
            _logger.LogError($"Error durante el inicio de sesión: {ex.ToString()}");
            return BadRequest();

            /* var message = "Error message: " + ex.Message;       // Qué ha sucedido
            if (ex.InnerException != null)      // Información sobre la excepción
            {
                message = message + " Inner exception: " + ex.InnerException.Message;
            }
            message = message + " Stack trace: " + ex.StackTrace;       // Dónde ha sucedido */
        }
    }

    private void LoguearUsuario(Usuario usuario)
    {
        HttpContext.Session.SetString("id", usuario.Id.ToString());         // Establece una variable de sesión, "id", con el valor 'usuario.Id'
        HttpContext.Session.SetString("nombreUsuario", usuario.Nombre);
        HttpContext.Session.SetString("rol", usuario.Rol.ToString());
        HttpContext.Session.SetString("password", usuario.Password);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
