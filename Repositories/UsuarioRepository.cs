using System.Data.SQLite;
using tl2_tp10_2023_JavvG.Models;

namespace tl2_tp10_2023_JavvG.Repositories;


public class UsuarioRepository : IUsuarioRepository 
{

    private readonly string _connectionString;

    public UsuarioRepository(string connectionString) 
    {
        _connectionString = connectionString;
    }

    public void Create(Usuario usuario) 
    {
        var query = @"INSERT INTO Usuario (nombre_de_usuario, password, rol) VALUES (@nombre_de_usuario, @password, @rol);";      // Consulta SQL

        using (SQLiteConnection connection = new SQLiteConnection(_connectionString)) 
        {
            
            connection.Open();      // Inicio de conexión con la BD
            
            var command = new SQLiteCommand(query, connection);

            command.Parameters.Add(new SQLiteParameter("@nombre_de_usuario", usuario.Nombre));
            command.Parameters.Add(new SQLiteParameter("@password", usuario.Password));
            command.Parameters.Add(new SQLiteParameter("@rol", usuario.Rol));
            
            command.ExecuteNonQuery();      // Ejecuta comandos como las instrucciones INSERT, DELETE, UPDATE.

            connection.Close();     // Fin de conexión con la BD

        }
    }

    public void Update(int id, Usuario usuario) 
    {
        var query = @"UPDATE Usuario SET nombre_de_usuario = @nuevo_nombre, password = @password, rol = @rol WHERE id = @id_buscado;";

        using (SQLiteConnection connection = new SQLiteConnection(_connectionString)) 
        {

            connection.Open();

            var command = new SQLiteCommand(query, connection);

            command.Parameters.Add(new SQLiteParameter("@id_buscado", id));
            command.Parameters.Add(new SQLiteParameter("@nuevo_nombre", usuario.Nombre));
            command.Parameters.Add(new SQLiteParameter("@password", usuario.Password));
            command.Parameters.Add(new SQLiteParameter("@rol", usuario.Rol));

            command.ExecuteNonQuery();
            
            connection.Close();

        }
    }

    public List<Usuario> GetAll() 
    {
        List<Usuario> usuarios = new();
        
        var query = @"SELECT * FROM Usuario;";
        
        using (SQLiteConnection connection = new SQLiteConnection(_connectionString)) 
        {

            connection.Open();

            var command = new SQLiteCommand(query, connection);

            using (var reader = command.ExecuteReader()) 
            {      // Lectura de tuplas

                while (reader.Read()) 
                {

                    var usuario = new Usuario();

                    usuario.Id = Convert.ToInt32(reader["id"]);
                    usuario.Nombre = reader["nombre_de_usuario"].ToString();
                    usuario.Password = reader["password"].ToString();
                    
                    var read = reader["rol"].ToString();
                    Rol rolUsuario;
                    if(Enum.TryParse(read, out rolUsuario))
                    {
                        usuario.Rol = rolUsuario;
                    }

                    usuarios.Add(usuario);
                    
                }
            }

            connection.Close();

        }

        return usuarios;
    }

    public Usuario GetById(int id) 
    {
        List<Usuario> usuarios = new();
        
        usuarios = GetAll();
        
        var usuarioBuscado = usuarios.FirstOrDefault(U => U.Id == id);

        return usuarioBuscado;
    }

    public int Delete(int id) 
    {
        var query = @"DELETE FROM Usuario WHERE Usuario.id = (@id_buscado);";

        int result = 0;

        using (SQLiteConnection connection = new SQLiteConnection(_connectionString)) 
        {

            connection.Open();

            var command = new SQLiteCommand(query, connection);

            command.Parameters.Add(new SQLiteParameter("@id_buscado", id));
            
            result = command.ExecuteNonQuery();     // Indica el número de tuplas afectadas en la BD. Si se elimina efectivamente una tupla, entonces retorna un número distinto de cero

            connection.Close();
            
        }

        return result;
    }

    public Usuario GetLoggedUser(string nombre, string password) 
    {
        var usuarioBuscado = new Usuario();

        var query = @"SELECT * FROM Usuario WHERE (nombre_de_usuario = @nombreUsuario AND password = @password);";

        using (SQLiteConnection connection = new SQLiteConnection(_connectionString))
        {
            connection.Open();

            var command = new SQLiteCommand(query, connection);

            command.Parameters.Add(new SQLiteParameter("@nombreUsuario", nombre));
            command.Parameters.Add(new SQLiteParameter("@password", password));


            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    usuarioBuscado.Id = Convert.ToInt32(reader["id"]);
                    usuarioBuscado.Nombre = reader["nombre_de_usuario"].ToString();
                    usuarioBuscado.Password = reader["password"].ToString();
                    string read = reader["rol"].ToString();
                    Rol rolUsuario;
                    if (Enum.TryParse(read, out rolUsuario))
                    {
                        usuarioBuscado.Rol = rolUsuario;
                    }
                }
            }

            connection.Close();
        }
        return usuarioBuscado;
    }
}