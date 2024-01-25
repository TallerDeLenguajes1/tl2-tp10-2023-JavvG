using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using tl2_tp10_2023_JavvG.Models;

namespace tl2_tp10_2023_JavvG.ViewModels;

public class ListarTablerosViewModel 
{
    [Display(Name = "Tableros")]
    public List<Tablero> Tableros { get; set; }

    public int IdUsuario { get; set; }

    public ListarTablerosViewModel() {}

    public ListarTablerosViewModel(List<Tablero> tableros, int idUsuario)
    {
        Tableros = tableros;
        IdUsuario = idUsuario;
    }
}