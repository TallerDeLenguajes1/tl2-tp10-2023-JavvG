using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using tl2_tp10_2023_JavvG.Models;

namespace tl2_tp10_2023_JavvG.ViewModels;

public class ModificarUsuarioViewModel 
{
    [Display(Name = "ID")]
    public int Id { get; set; }
    
    [Required(ErrorMessage = "Este campo es requerido")]
    [Display(Name = "Nombre de usuario")]
    [StringLength(30, ErrorMessage = "El nombre de usuario no puede tener mas de 30 caracteres")]
    public string Nombre { get; set; }
    
    [Required(ErrorMessage = "Este campo es requerido")]
    [Display(Name = "Contraseña")]
    [StringLength(30, MinimumLength = 4, ErrorMessage = "La contraseña debe tener entre 8 y 30 caracteres")]
    public string Password { get; set; }

    [Required(ErrorMessage = "Este campo es requerido")]
    [Display(Name = "Rol del usuario")]
    public Rol Rol { get; set; }

    public ModificarUsuarioViewModel() {}

    public ModificarUsuarioViewModel(Usuario usuario)
    {
        Id = usuario.Id;
        Nombre = usuario.Nombre;
        Password = usuario.Password;
        Rol = usuario.Rol;
    }
    
}