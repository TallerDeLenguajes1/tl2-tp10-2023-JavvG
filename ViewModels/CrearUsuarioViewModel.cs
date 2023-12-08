using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using tl2_tp10_2023_JavvG.Models;

namespace tl2_tp10_2023_JavvG.ViewModels;

public class CrearUsuarioViewModel 
{
    [Display(Name = "ID")]
    public int Id { get; set; }
    
    [Required(ErrorMessage = "Este campo es requerido")]
    [Display(Name = "Nombre de usuario")]
    public string Nombre { get; set; }
    
    [Required(ErrorMessage = "Este campo es requerido")]
    [Display(Name = "Contraseña")]
    public string Password { get; set; }

    [Required(ErrorMessage = "Este campo es requerido")]
    [Display(Name = "Rol del usuario")]
    public Rol Rol { get; set; }

    public CrearUsuarioViewModel() {}

    public CrearUsuarioViewModel(Usuario usuario) 
    {
        Id = usuario.Id;
        Nombre = usuario.Nombre;
        Password = usuario.Password;
        Rol = usuario.Rol;
    }
}