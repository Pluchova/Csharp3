namespace ToDoList.Persistence.Repositories;

using ToDoList.Domain.Models;

public class ToDoItemsRepository : IRepository<ToDoItem>
{
    private readonly ToDoItemsContext context;

    public ToDoItemsRepository(ToDoItemsContext context)
    {
        this.context = context;
    }

    public void Create(ToDoItem item)
    {
        context.ToDoItems.Add(item);
        context.SaveChanges();
    }
 public IEnumerable<ToDoItem> Read()
        {
            return context.ToDoItems.ToList();
        }
     public ToDoItem? ReadById(int id)
        {
            return context.ToDoItems.Find(id);
        }

        public void UpdateById(int id, ToDoItem updatedItem)
        {
            var item = context.ToDoItems.Find(id);
            if (item != null)
            {
                context.Entry(item).CurrentValues.SetValues(updatedItem);
                context.SaveChanges();
            }
        }

        public void DeleteById(int id)
        {
            var item = context.ToDoItems.Find(id);
            if (item != null)
            {
                context.ToDoItems.Remove(item);
                context.SaveChanges();
            }
        }
}
