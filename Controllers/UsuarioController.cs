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
        var users = usuarioRepository.GetAll();
        var usersVM = new ListarUsuariosViewModel(users);
        return View(usersVM);
    }

    // Creación de usuario. Recibe los datos de un usuario desde un formulario y los carga en la BD

    [HttpGet]       // Para obtener la vista
    public IActionResult Create()
    {
        return View(new Usuario());
    }

    [HttpPost]      // Para añadir el usuario en la BD
    public IActionResult Create(Usuario usuario)
    {
        usuarioRepository.Create(usuario);
        return RedirectToAction("Index");       // Una vez creado el usuario, retorna a Index
    }

    // Modificación de un usuario

    [HttpGet]
    public IActionResult Update(int id)
    {
        return View(usuarioRepository.GetById(id));
    }

    [HttpPost]      // No funciona con [HttpPut] - Consultar (!)
    public IActionResult Update(int id, Usuario usuario)
    {
        usuarioRepository.Update(id, usuario);
        return RedirectToAction("Index");
    }

    // Eliminación de un usuario

    [HttpGet]
    public IActionResult Delete(int id) 
    {
        return View(usuarioRepository.GetById(id));
    }

    [HttpPost]      // No funciona con [HttpDelete] - Consultar (!)
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

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}