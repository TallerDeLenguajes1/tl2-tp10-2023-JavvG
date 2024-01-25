using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using tl2_tp10_2023_JavvG.Models;

namespace tl2_tp10_2023_JavvG.ViewModels;

public class CrearTableroViewModel 
{
    [Display(Name = "ID")]
    public int Id { get; set; }

    [Display(Name = "ID del Usuario Propietario")]
    public int IdUsuarioPropietario { get; set; }

    [Required(ErrorMessage = "Este campo es requerido")]
    [StringLength(100, ErrorMessage = "El nombre del tablero no puede tener mas de 100 caracteres")]
    [Display(Name = "Título")]
    public string Nombre { get; set; }

    [Required(ErrorMessage = "Este campo es requerido")]
    [StringLength(100, ErrorMessage = "La descripción del tablero no puede tener mas de 100 caracteres")]
    [Display(Name = "Descripción")]
    public string Descripcion { get; set; }

    public List<Usuario> UsuariosRegistrados { get; set; }

    public CrearTableroViewModel() {}

    public CrearTableroViewModel(int idUsuario, List<Usuario> usuarios)
    {
        IdUsuarioPropietario = idUsuario;
        UsuariosRegistrados = usuarios;
    }

    public CrearTableroViewModel(Tablero tablero)
    {
        Id = tablero.Id;
        IdUsuarioPropietario = tablero.IdUsuarioPropietario;
        Nombre = tablero.Nombre;
        Descripcion = tablero.Descripcion;
    }
}