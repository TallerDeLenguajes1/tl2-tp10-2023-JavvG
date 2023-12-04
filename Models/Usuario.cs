namespace tl2_tp10_2023_JavvG.Models;

public class Usuario 
{

    public enum Rol 
    {
        administrador,
        operador
    }

    private int id;
    private string nombre;
    private string password;
    private Rol rol;

    public int Id { get => id; set => id = value; }
    public string Nombre { get => nombre; set => nombre = value; }
    public string Password { get => password; set => password = value;}
    public Rol Rol { get => rol; set => rol = value;}

}