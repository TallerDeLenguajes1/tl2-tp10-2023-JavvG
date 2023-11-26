using tl2_tp10_2023_JavvG.Models;

namespace tl2_tp10_2023_JavvG.Repositories;

public interface ITareaRepository 
{

    public Tarea Create(int idTablero, Tarea tarea);
    public void Update(int id, Tarea tarea);
    public List<Tarea> GetAll();
    public Tarea GetById(int id);
    public List<Tarea> GetByUsuarioId(int idUsuario);
    public List<Tarea> GetByTableroId(int idTablero);
    public int Delete(int idTarea);
    public void Assign(int idUsuario, int idTarea);

}