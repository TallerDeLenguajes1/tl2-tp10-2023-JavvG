using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using tl2_tp10_2023_JavvG.Models;

namespace tl2_tp10_2023_JavvG.ViewModels;

public class ModificarUsuarioViewModel 
{
    [Display(Name = "ID")]
    public int Id { get; set; }

    [Required(ErrorMessage = "Este campo es requerido")]
    [Display(Name = "Nuevo nombre")]
    public string Nombre { get; set; }

    [Required(ErrorMessage = "Este campo es requerido")]
    [Display(Name = "Nueva contraseña")]
    public string Password { get; set; }

    [Required(ErrorMessage = "Este campo es requerido")]
    [Display(Name = "Nuevo rol")]
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