using System.Data.SQLite;
using tl2_tp10_2023_JavvG.Models;

namespace tl2_tp10_2023_JavvG.Repositories;

public class TableroRepository : ITableroRepository
{

    private readonly string connectionString = "Data Source=DB/kanban.db;Cache=Shared";

    public Tablero Create(Tablero tablero)
    {
        
        var query = @"INSERT INTO Tablero (id_usuario_propietario, nombre, descripcion) VALUES (@id_usuario, @nombre_tablero, @descripcion_tablero);";

        using (SQLiteConnection connection = new SQLiteConnection(connectionString))
        {

            connection.Open();

            var command = new SQLiteCommand(query, connection);

            command.Parameters.Add(new SQLiteParameter("@id_usuario", tablero.IdUsuarioPropietario));
            command.Parameters.Add(new SQLiteParameter("@nombre_tablero", tablero.Nombre));
            command.Parameters.Add(new SQLiteParameter("@descripcion_tablero", tablero.Descripcion));

            command.ExecuteNonQuery();

            connection.Close();

        }

        return tablero;

    }

    public List<Tablero> GetAll()
    {

        List<Tablero> tableros = new();
        
        var query = @"SELECT * FROM Tablero;";

        using (SQLiteConnection connection = new SQLiteConnection(connectionString))
        {

            connection.Open();

            var command = new SQLiteCommand(query, connection);

            using (var reader = command.ExecuteReader())
            {

                while (reader.Read())
                {

                    var tablero = new Tablero();

                    tablero.Id = Convert.ToInt32(reader["id"]);
                    tablero.IdUsuarioPropietario = Convert.ToInt32(reader["id_usuario_propietario"]);
                    tablero.Nombre = reader["nombre"].ToString();
                    tablero.Descripcion = reader["descripcion"].ToString();

                    tableros.Add(tablero);

                }

            }

            connection.Close();

        }

        return tableros;

    }

    public Tablero GetById(int id)
    {
        
        List<Tablero> tableros = new();

        tableros = GetAll();

        var tableroBuscado = tableros.FirstOrDefault(T => T.Id == id);

        return tableroBuscado;

    }

    public List<Tablero> GetByUserId(int idUsuario)
    {
        
        List<Tablero> tableros = new();

        tableros = GetAll();

        List<Tablero> tablerosBuscados = tableros.FindAll(T => T.IdUsuarioPropietario == idUsuario);

        return tablerosBuscados;

    }

    public void Delete(int id)
    {
        
        var query = @"DELETE FROM Tablero WHERE Tablero.id = (@id_buscado);";

        using (SQLiteConnection connection = new SQLiteConnection(connectionString))
        {

            connection.Open();

            var command = new SQLiteCommand(query, connection);

            command.Parameters.Add(new SQLiteParameter("@id_buscado", id));

            command.ExecuteNonQuery();

            connection.Close();

        }

    }

    public void Update(int id, Usuario usuario)
    {
        
        var query = @"UPDATE Tablero SET id_usuario_asignado = @nuevo_id_usuario, nombre = @nuevo_nombre, descripcion = @nueva_descripcion WHERE id = @id_buscado;";

        using (SQLiteConnection connection = new SQLiteConnection(connectionString))
        {

            connection.Open();

            var command = new SQLiteCommand(query, connection);

            command.Parameters.Add(new SQLiteParameter("@nuevo_id_usuario", usuario.Id));

            connection.Close();

        }

    }
    
}
