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
    [Display(Name = "Título")]
    [StringLength(100, ErrorMessage = "El título de la tarea no puede tener mas de 100 caracteres")]
    public string Nombre { get; set; }

    [Required(ErrorMessage = "Este campo es requerido")]
    [Display(Name = "Descripción")]
    [StringLength(200, ErrorMessage = "La descripción de la tarea no puede tener mas de 200 caracteres")]
    public string Descripcion { get; set; }

    [Required(ErrorMessage = "Este campo es requerido")]
    [Display(Name = "Color")]
    [StringLength(20, ErrorMessage = "El nombre del color no puede tener mas de 20 caracteres")]
    public string Color { get; set; }

    [Display(Name = "Estado")]
    public EstadoTarea Estado { get; set; }

    [Display(Name = "ID del usuario asignado")]
    public int? IdUsuarioAsignado { get; set; }

    public List<Usuario> UsuariosRegistrados { get; set; }
    public List<Tablero> TablerosRegistrados { get; set; }

    public ModificarTareaViewModel() {}

    public ModificarTareaViewModel(Tarea tarea, List<Usuario> usuarios, List<Tablero> tableros)
    {
        Id = tarea.Id;
        IdTablero = tarea.IdTablero;
        Nombre = tarea.Nombre;
        Descripcion = tarea.Descripcion;
        Color = tarea.Color;
        Estado = tarea.Estado;
        IdUsuarioAsignado = tarea.IdUsuarioAsignado;
        UsuariosRegistrados = usuarios;
        TablerosRegistrados = tableros;
    }
}