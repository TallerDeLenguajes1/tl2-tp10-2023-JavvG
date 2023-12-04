using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using tl2_tp10_2023_JavvG.Models;
using tl2_tp10_2023_JavvG.Repositories;

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
        return View(tableroRepository.GetAll());
    }

    // Crear tablero

    [HttpGet]
    public IActionResult Create() 
    {
        return View(new Tablero());
    }

    [HttpPost]
    public IActionResult Create(Tablero tablero)
    {
        tableroRepository.Create(tablero);
        return RedirectToAction("Index");
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

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}