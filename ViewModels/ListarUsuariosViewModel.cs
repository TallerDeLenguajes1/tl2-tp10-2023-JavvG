using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using tl2_tp10_2023_JavvG.Models;

namespace tl2_tp10_2023_JavvG.ViewModels;

public class ListarUsuariosViewModel 
{
    public List<Usuario> Usuarios { get; set; }

    public ListarUsuariosViewModel() {}

    public ListarUsuariosViewModel(List<Usuario> usuarios)
    {
        Usuarios = usuarios;
    }
    
}