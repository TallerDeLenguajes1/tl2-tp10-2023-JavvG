using EspacioTarea;

namespace EspacioInterfazTarea;

public interface ITareaRepository {

    public Tarea Create(int idTablero, Tarea tarea);
    public void Update(int id, Tarea tarea);
    public List<Tarea> GetAll();
    public Tarea GetById(int id);
    public List<Tarea> GetByUsuarioId(int idUsuario);
    public List<Tarea> GetByTableroId(int idTablero);
    public void Delete(int idTarea);
    public void Assign(int idUsuario, int idTarea);

}