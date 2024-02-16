using tl2_tp10_2023_JavvG.Models;

namespace tl2_tp10_2023_JavvG.Repositories;

public interface ITableroRepository 
{
    Tablero Create(Tablero tablero);
    void Update(int id, Tablero tablero);
    Tablero GetById(int id);
    List<Tablero> GetAll();
    List<Tablero> GetByUserId(int idUsuario);
    int Delete(int id);
    void SetDefaultUsuarioId(int idUsuario);
}