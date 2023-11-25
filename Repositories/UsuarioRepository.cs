using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SQLite;
using EspacioUsuario;
using EspacioInterfazUsuario;


public class UsuarioRepository : IUsuarioRepository {

    private readonly string connectionString = "Data Source=DB/kanban.db;Cache=Shared";

    public void Create(Usuario usuario) {

        var query = @"INSERT INTO Usuario (nombre_de_usuario) VALUES (@nombre_de_usuario);";      // Consulta SQL

        using (SQLiteConnection connection = new SQLiteConnection(connectionString)) {
            
            connection.Open();      // Inicio de conexión con la BD
            
            var command = new SQLiteCommand(query, connection);

            command.Parameters.Add(new SQLiteParameter("@nombre_de_usuario", usuario.Nombre));
            
            command.ExecuteNonQuery();      // Ejecuta comandos como las instrucciones INSERT, DELETE, UPDATE.

            connection.Close();     // Fin de conexión con la BD

        }

    }

    public void Update(int id, Usuario usuario) {

        var query = @"UPDATE Usuario SET nombre_de_usuario = @nuevo_nombre WHERE id = @id_buscado;";

        using (SQLiteConnection connection = new SQLiteConnection(connectionString)) {

            connection.Open();

            var command = new SQLiteCommand(query, connection);

            command.Parameters.Add(new SQLiteParameter("@id_buscado", id));
            command.Parameters.Add(new SQLiteParameter("@nuevo_nombre", usuario.Nombre));

            command.ExecuteNonQuery();
            
            connection.Close();

        }

    }

    public List<Usuario> GetAll() {

        List<Usuario> usuarios = new();
        
        var query = @"SELECT * FROM Usuario;";
        
        using (SQLiteConnection connection = new SQLiteConnection(connectionString)) {

            connection.Open();

            var command = new SQLiteCommand(query, connection);

            using (var reader = command.ExecuteReader()) {      // Lectura de tuplas

                while (reader.Read()) {

                    var usuario = new Usuario();

                    usuario.Id = Convert.ToInt32(reader["id"]);
                    usuario.Nombre = reader["nombre_de_usuario"].ToString();

                    usuarios.Add(usuario);
                    
                }
            }

            connection.Close();

        }

        return usuarios;

    }

    public Usuario GetById(int id) {

        List<Usuario> usuarios = new();
        
        usuarios = GetAll();
        
        var usuarioBuscado = usuarios.FirstOrDefault(U => U.Id == id);

        return usuarioBuscado;
        
    }

    public void Delete(int id) {

        var query = @"DELETE FROM Usuario WHERE Usuario.id = (@id_buscado);";

        using (SQLiteConnection connection = new SQLiteConnection(connectionString)) {

            connection.Open();

            var command = new SQLiteCommand(query, connection);

            command.Parameters.Add(new SQLiteParameter("@id_buscado", id));
            
            command.ExecuteNonQuery();

            connection.Close();
            
        }

    }
    
}