using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using tl2_tp10_2023_JavvG.Models;

namespace tl2_tp10_2023_JavvG.ViewModels;

public class ModificarTareaViewModel 
{
    [Display(Name = "ID")]
    public int Id { get; set; }

    [Display(Name = "ID Tablero")]
    public int IdTablero { get; set; }

    [Required(ErrorMessage = "Este campo es requerido")]
    [Display(Name = "Nuevo título")]
    public string Nombre { get; set; }

    [Required(ErrorMessage = "Este campo es requerido")]
    [Display(Name = "Nueva descripción")]
    public string Descripcion { get; set; }

    [Required(ErrorMessage = "Este campo es requerido")]
    [Display(Name = "Nuevo color")]
    public string Color { get; set; }

    [Display(Name = "Nuevo estado")]
    public EstadoTarea Estado { get; set; }

    [Display(Name = "Modificar el ID del usuario asignado")]
    public int? IdUsuarioAsignado { get; set; }

    public ModificarTareaViewModel() {}

    public ModificarTareaViewModel(Tarea tarea)
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