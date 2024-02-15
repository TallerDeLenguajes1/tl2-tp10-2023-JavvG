using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using tl2_tp10_2023_JavvG.Models;

namespace tl2_tp10_2023_JavvG.ViewModels;

public class ListarTareasViewModel 
{
    public List<Tarea> Tareas { get; set; }

    public List<Tarea> TareasCreadas { get; set; }

    public int IdUsuario { get; set; }

    public Tarea TareaSeleccionada { get; set; }

    public ListarTareasViewModel() 
    {
        Tareas = new();
        TareasCreadas = new();
    }

    public ListarTareasViewModel(List<Tarea> tareas, List<Tarea> tareasCreadas, int idUsuario) 
    {
        Tareas = tareas;
        TareasCreadas = tareasCreadas;
        IdUsuario = idUsuario;
    }

    public ListarTareasViewModel(List<Tarea> tareas, int idUsuario)
    {
        Tareas = tareas;
        IdUsuario = idUsuario;
    }

    public ListarTareasViewModel(Tarea tarea, int idUsuario)
    {
        TareaSeleccionada = tarea;
        IdUsuario = idUsuario;
    }

}