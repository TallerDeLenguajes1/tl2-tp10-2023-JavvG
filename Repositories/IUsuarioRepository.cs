using tl2_tp10_2023_JavvG.Models;

namespace tl2_tp10_2023_JavvG.Repositories;

public interface IUsuarioRepository 
{
    void Create(Usuario usuario);
    void Update(int id, Usuario usuario);
    List<Usuario> GetAll();
    Usuario GetById(int id);
    int Delete(int id);
    Usuario GetLoggedUser(string nombre, string password);
}
