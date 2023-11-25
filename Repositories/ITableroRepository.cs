using tl2_tp10_2023_JavvG.Models;

namespace tl2_tp10_2023_JavvG.Repositories;

public interface ITableroRepository 
{

    public Tablero Create(Tablero tablero);
    public void Update(int id, Usuario usuario);
    public Tablero GetById(int id);
    public List<Tablero> GetAll();
    public List<Tablero> GetByUserId(int idUsuario);
    public void Delete(int id);
    
}