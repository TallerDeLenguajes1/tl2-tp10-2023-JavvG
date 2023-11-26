using tl2_tp10_2023_JavvG.Models;

namespace tl2_tp10_2023_JavvG.Repositories;

public interface IUsuarioRepository 
{
    public void Create(Usuario usuario);
    public void Update(int id, Usuario usuario);
    public List<Usuario> GetAll();
    public Usuario GetById(int id);
    public int Delete(int id);
}
