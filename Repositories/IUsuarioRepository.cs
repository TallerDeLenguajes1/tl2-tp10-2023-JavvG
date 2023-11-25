using EspacioUsuario;

namespace EspacioInterfazUsuario;

public interface IUsuarioRepository {
    public void Create(Usuario usuario);
    public void Update(int id, Usuario usuario);
    public List<Usuario> GetAll();
    public Usuario GetById(int id);
    public void Delete(int id);
}
