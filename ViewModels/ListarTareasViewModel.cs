using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using tl2_tp10_2023_JavvG.Models;

namespace tl2_tp10_2023_JavvG.ViewModels;

public class ListarTareasViewModel 
{
    [Display(Name = "Tareas")]
    public List<Tarea> Tareas { get; set; }

    public int IdUsuario { get; set; }

    public ListarTareasViewModel() {}

    public ListarTareasViewModel(List<Tarea> tareas) 
    {
        Tareas = tareas;
    }

    public ListarTareasViewModel(List<Tarea> tareas, int idUsuario)
    {
        Tareas = tareas;
        IdUsuario = idUsuario;
    }

}