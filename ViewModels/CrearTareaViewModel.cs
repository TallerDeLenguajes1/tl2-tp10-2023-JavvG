using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using tl2_tp10_2023_JavvG.Models;

namespace tl2_tp10_2023_JavvG.ViewModels;

public class CrearTareaViewModel 
{
    [Display(Name = "ID")]
    public int Id { get; set; }

    [Display(Name = "ID Tablero")]
    public int IdTablero { get; set; }

    [Required(ErrorMessage = "Este campo es requerido")]
    [Display(Name = "Título")]
    public string Nombre { get; set; }

    [Required(ErrorMessage = "Este campo es requerido")]
    [Display(Name = "Descripción")]
    public string Descripcion { get; set; }

    [Required(ErrorMessage = "Este campo es requerido")]
    [Display(Name = "Color")]
    public string Color { get; set; }

    [Display(Name = "Estado")]
    public EstadoTarea Estado { get; set; }

    [Display(Name = "ID del usuario asignado")]
    public int? IdUsuarioAsignado { get; set; }

    public CrearTareaViewModel() {}

    public CrearTareaViewModel(Tarea tarea)
    {
        Id = tarea.Id;
        IdTablero = tarea.IdTablero;
        Nombre = tarea.Nombre;
        Descripcion = tarea.Descripcion;
        Color = tarea.Color;
        Estado = tarea.Estado;
        IdUsuarioAsignado = tarea.IdUsuarioAsignado;
    }
}