using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SQLite;
using tl2_tp10_2023_JavvG.Models;

namespace tl2_tp10_2023_JavvG.Repositories;

public class TareaRepository : ITareaRepository 
{

    private readonly string connectionString = "Data Source=DB/kanban.db;Cache=Shared";

    public void Assign(int idUsuario, int idTarea) 
    {
        
        var query = @"UPDATE Tarea SET id_usuario_asignado = @id_usuario WHERE id = @id_tarea;";

        using (SQLiteConnection connection = new SQLiteConnection(connectionString)) 
        {

            connection.Open();

            var command = new SQLiteCommand(query, connection);

            command.Parameters.Add(new SQLiteParameter("@id_usuario", idUsuario));
            command.Parameters.Add(new SQLiteParameter("@id_tarea", idTarea));

            command.ExecuteNonQuery();
            
            connection.Close();

        }

    }

    public Tarea Create(int idTablero, Tarea tarea) 
    {

        var query = @"INSERT INTO tarea (id_tablero, nombre, estado, descripcion, color, id_usuario_asignado) VALUES (@id_tablero, @nombre, @estado, @descripcion, @color, @id_usuario_asignado);";

        using (var connection = new SQLiteConnection(connectionString)) 
        {

            connection.Open();

            var command = new SQLiteCommand(query, connection);

            command.Parameters.Add(new SQLiteParameter("@id_tablero", idTablero));
            command.Parameters.Add(new SQLiteParameter("@nombre", tarea.Nombre));
            command.Parameters.Add(new SQLiteParameter("@estado", tarea.Estado));
            command.Parameters.Add(new SQLiteParameter("@descripcion", tarea.Descripcion));
            command.Parameters.Add(new SQLiteParameter("@color", tarea.Color));
            command.Parameters.Add(new SQLiteParameter("@id_usuario_asignado", tarea.IdUsuarioAsignado));

            command.ExecuteNonQuery();

            connection.Close();

        }

        return tarea;

    }

    public List<Tarea> GetAll() 
    {

        List<Tarea> tareas = new();

        var query = @"SELECT * FROM Tarea;";

        using (SQLiteConnection connection = new SQLiteConnection(connectionString)) 
        {

            connection.Open();

            var command = new SQLiteCommand(query, connection);

            using (var reader = command.ExecuteReader()) 
            {

                while (reader.Read()) 
                {

                    var tarea = new Tarea();

                    tarea.Id = Convert.ToInt32(reader["id"]);
                    tarea.IdTablero = Convert.ToInt32(reader["id_tablero"]);
                    tarea.Nombre = reader["nombre"].ToString();

                    var read = reader["estado"].ToString();
                    EstadoTarea estado;
                    if(Enum.TryParse(read, out estado)) 
                    {
                        tarea.Estado = estado;
                    }

                    tarea.Descripcion = reader["descripcion"].ToString();
                    tarea.Color = reader["color"].ToString();
                    tarea.IdUsuarioAsignado = Convert.ToInt32(reader["id_usuario_asignado"]);

                    tareas.Add(tarea);

                }

            }

            connection.Close();

        }

        return tareas;
    }

    public List<Tarea> GetByTableroId(int idTablero) 
    {
        
        List<Tarea> tareas = new();

        var query = @"SELECT * FROM Tarea WHERE id_tablero = @id_tablero;";

        using (SQLiteConnection connection = new SQLiteConnection(connectionString)) 
        {

            connection.Open();

            var command = new SQLiteCommand(query, connection);

            command.Parameters.Add(new SQLiteParameter("@id_tablero", idTablero));

            using (var reader = command.ExecuteReader()) 
            {

                while (reader.Read()) 
                {

                    var tarea = new Tarea();

                    tarea.Id = Convert.ToInt32(reader["id"]);
                    tarea.IdTablero = Convert.ToInt32(reader["id_tablero"]);
                    tarea.Nombre = reader["nombre"].ToString();

                    var read = reader["estado"].ToString();
                    EstadoTarea estado;
                    if(Enum.TryParse(read, out estado)) 
                    {
                        tarea.Estado = estado;
                    }

                    tarea.Descripcion = reader["descripcion"].ToString();
                    tarea.Color = reader["color"].ToString();
                    tarea.IdUsuarioAsignado = Convert.ToInt32(reader["id_usuario_asignado"]);

                    tareas.Add(tarea);

                }

            }

            connection.Close();

        }

        return tareas;

    }

    public Tarea GetById(int id) 
    {
        
        Tarea tarea = new();

        var query = @"SELECT * FROM Tarea WHERE id = @id_tarea;";

        using (SQLiteConnection connection = new SQLiteConnection(connectionString)) 
        {

            connection.Open();

            var command = new SQLiteCommand(query, connection);

            command.Parameters.Add(new SQLiteParameter("@id_tarea", id));

            using (var reader = command.ExecuteReader()) 
            {

                while (reader.Read()) 
                {

                    tarea.Id = Convert.ToInt32(reader["id"]);
                    tarea.IdTablero = Convert.ToInt32(reader["id_tablero"]);
                    tarea.Nombre = reader["nombre"].ToString();

                    var read = reader["estado"].ToString();
                    EstadoTarea estado;
                    if(Enum.TryParse(read, out estado)) 
                    {
                        tarea.Estado = estado;
                    }

                    tarea.Descripcion = reader["descripcion"].ToString();
                    tarea.Color = reader["color"].ToString();
                    tarea.IdUsuarioAsignado = Convert.ToInt32(reader["id_usuario_asignado"]);

                }

            }

            connection.Close();

        }

        return tarea;

    }

    public List<Tarea> GetByUsuarioId(int idUsuario) 
    {

        List<Tarea> tareas = new();

        var query = @"SELECT * FROM Tarea  WHERE id_usuario_asignado = @id_usuario;";

        using (SQLiteConnection connection = new SQLiteConnection(connectionString)) 
        {

            connection.Open();

            var command = new SQLiteCommand(query, connection);

            command.Parameters.Add(new SQLiteParameter("@id_usuario", idUsuario));

            using (var reader = command.ExecuteReader()) 
            {

                while (reader.Read()) 
                {

                    var tarea = new Tarea();

                    tarea.Id = Convert.ToInt32(reader["id"]);
                    tarea.IdTablero = Convert.ToInt32(reader["id_tablero"]);
                    tarea.Nombre = reader["nombre"].ToString();

                    var read = reader["estado"].ToString();
                    EstadoTarea estado;
                    if(Enum.TryParse(read, out estado)) 
                    {
                        tarea.Estado = estado;
                    }

                    tarea.Descripcion = reader["descripcion"].ToString();
                    tarea.Color = reader["color"].ToString();
                    tarea.IdUsuarioAsignado = Convert.ToInt32(reader["id_usuario_asignado"]);

                    tareas.Add(tarea);

                }

            }

            connection.Close();

        }

        return tareas;

    }

    public void Delete(int idTarea) 
    {
        
        var query = @"DELETE FROM Tarea WHERE Tarea.id = (@id_buscado);";

        using (SQLiteConnection connection = new SQLiteConnection(connectionString)) 
        {

            connection.Open();

            var command = new SQLiteCommand(query, connection);

            command.Parameters.Add(new SQLiteParameter("@id_buscado", idTarea));

            command.ExecuteNonQuery();

            connection.Close();

        }

    }

    public void Update(int id, Tarea tarea) 
    {
        
        var query = @"UPDATE Tarea SET id_tablero = @nuevo_id_talero, nombre = @nuevo_nombre, estado = @nuevo_estado, descripcion = @nueva_descripcion, color = @nuevo_color, id_usuario_asignado = @nuevo_id_usuario WHERE id = @id_tarea;";

        using (SQLiteConnection connection = new SQLiteConnection(connectionString)) 
        {

            connection.Open();

            var command = new SQLiteCommand(query, connection);

            command.Parameters.Add(new SQLiteParameter("@nuevo_id_talero", tarea.IdTablero));
            command.Parameters.Add(new SQLiteParameter("@nuevo_nombre", tarea.Nombre));
            command.Parameters.Add(new SQLiteParameter("@nuevo_estado", tarea.Estado));
            command.Parameters.Add(new SQLiteParameter("@nueva_descripcion", tarea.Descripcion));
            command.Parameters.Add(new SQLiteParameter("@nuevo_color", tarea.Color));
            command.Parameters.Add(new SQLiteParameter("@nuevo_id_usuario", tarea.IdUsuarioAsignado));
            command.Parameters.Add(new SQLiteParameter("@id_tarea", id));

            command.ExecuteNonQuery();

            connection.Close();

        }

    }
}
