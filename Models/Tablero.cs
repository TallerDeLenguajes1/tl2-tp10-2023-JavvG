using tl2_tp10_2023_JavvG.ViewModels;

namespace tl2_tp10_2023_JavvG.Models;

public class Tablero 
{
    private int id;
    private int idUsuarioPropietario;
    private string nombre;
    private string descripcion;

    public int Id { get => id; set => id = value; }
    public int IdUsuarioPropietario { get => idUsuarioPropietario; set => idUsuarioPropietario = value; }
    public string Nombre { get => nombre; set => nombre = value; }
    public string Descripcion { get => descripcion; set => descripcion = value; }

    public Tablero() {}

    public Tablero(CrearTableroViewModel tableroVM)
    {
        Id = tableroVM.Id;
        IdUsuarioPropietario = tableroVM.IdUsuarioPropietario;
        Nombre = tableroVM.Nombre;
        Descripcion = tableroVM.Descripcion;
    }

    public Tablero(ModificarTableroViewModel tableroVM)
    {
        Id = tableroVM.Id;
        IdUsuarioPropietario = tableroVM.IdUsuarioPropietario;
        Nombre = tableroVM.Nombre;
        Descripcion = tableroVM.Descripcion;
    }
}