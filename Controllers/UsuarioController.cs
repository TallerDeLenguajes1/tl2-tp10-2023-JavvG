using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using tl2_tp10_2023_JavvG.Models;
using tl2_tp10_2023_JavvG.Repositories;
using tl2_tp10_2023_JavvG.ViewModels;

namespace tl2_tp10_2023_JavvG.Controllers;

public class UsuarioController : Controller
{
    private readonly ILogger<UsuarioController> _logger;
    private readonly IUsuarioRepository usuarioRepository;

    public UsuarioController(ILogger<UsuarioController> logger)
    {
        _logger = logger;
        usuarioRepository = new UsuarioRepository();
    }

    // Endpoints

    // Página principal - muestra todos los usuarios en una tabla

    public IActionResult Index()
    {   
        // Se verifica que el usuario esté logueado correctamente

        if(!User.Identity.IsAuthenticated && HttpContext.Session.GetString("rol") != "administrador" && HttpContext.Session.GetString("rol") != "operador") return RedirectToLogin();       // Propiedad de ASP.NET Core, verifica que el usuario se haya autenticado correctamente. Se llena automáticamente cuando el usuario se loguea

        // El acceso a todos los usuarios esta permitido sólo para el usuario 'administrador'

        if(!isAdmin()) return RedirectOperatorUser();

        var users = usuarioRepository.GetAll();
        var usersVM = new ListarUsuariosViewModel(users);

        return View(usersVM);
    }

    // Creación de usuario. Recibe los datos de un usuario desde un formulario y los carga en la BD

    // Para obtener la vista (No es necesario escribir [HttpGet], pues se asume que ese es el verbo por defecto)

    public IActionResult Create()
    {
        if(!isAdmin()) return RedirectOperatorUser();
        return View(new CrearUsuarioViewModel());
    }

    [HttpPost]      // Para añadir el usuario en la BD
    public IActionResult Create(CrearUsuarioViewModel usuarioVM)        // Recibe el objeto view model desde el formulario
    {
        if(!ModelState.IsValid) return RedirectToAction("Index");   // Si el modelo no está en su estado válido, se redirecciona a la página de inicio (verifica que no haya errores de validación)   
        if(!isAdmin()) return RedirectOperatorUser();  // Si el usuario no es administrador, es redirigido a la página de inicio       

        var usuario = new Usuario(usuarioVM);
        usuarioRepository.Create(usuario);
        
        return RedirectToAction("Index");
    }

    // Modificación de un usuario

    public IActionResult Update(int id)
    {
        if(!isAdmin()) return RedirectOperatorUser();

        var usuario = usuarioRepository.GetById(id);
        var usuarioVM = new ModificarUsuarioViewModel(usuario);

        return View(usuarioVM);
    }

    [HttpPost]
    public IActionResult Update(int id, ModificarUsuarioViewModel usuarioVM)
    {
        if(!ModelState.IsValid) return RedirectToAction("Index");
        if(!isAdmin()) return RedirectOperatorUser();

        var usuario = new Usuario(usuarioVM);
        usuarioRepository.Update(id, usuario);

        return RedirectToAction("Index");
    }

    // Eliminación de un usuario

    public IActionResult Delete(int id) 
    {
        if(!isAdmin()) return RedirectOperatorUser();
        return View(usuarioRepository.GetById(id));
    }

    [HttpPost]
    public IActionResult DeleteConfirmed(int id)
    {
        if (usuarioRepository.Delete(id) > 0)       // Si la eliminación de tuplas fue efectiva, entonces se ejecuta lo que sigue
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