using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using tl2_tp10_2023_JavvG.Models;

namespace tl2_tp10_2023_JavvG.ViewModels;

public class ListarTablerosViewModel 
{
    public int IdUsuario { get; set; }

    public List<Tablero> Tableros { get; set; }

    public List<Tablero> TablerosPropios { get; set; }

    public List<Tablero> TablerosConTareasAsignadas { get; set; }

    public ListarTablerosViewModel() 
    {
        Tableros = new();
        TablerosPropios = new();
        TablerosConTareasAsignadas = new();
    }

    public ListarTablerosViewModel(List<Tablero> tableros, int idUsuario)
    {
        Tableros = tableros;
        IdUsuario = idUsuario;
    }

    public ListarTablerosViewModel(List<Tablero> tableros1, List<Tablero> tableros2, int idUsuario)
    {
        TablerosPropios = tableros1;
        TablerosConTareasAsignadas = tableros2;
        IdUsuario = idUsuario;
    }
}