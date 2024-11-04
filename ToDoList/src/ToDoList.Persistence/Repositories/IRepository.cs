namespace ToDoList.Persistence.Repositories;

public interface IRepository<T> where T : class
{
   public void Create(T item);
    IEnumerable<T> Read();
    T? ReadById(int id);
    void UpdateById(int id, T item);
    void DeleteById(int id);
}
