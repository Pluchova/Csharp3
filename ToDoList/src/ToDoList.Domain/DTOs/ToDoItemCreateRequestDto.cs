namespace ToDoList.Domain.DTOs;

using ToDoList.Domain.Models;

namespace ToDoList.Domain.DTOs
{
    public record ToDoItemCreateRequestDto(string Name, string Description, bool IsCompleted)
    {
        public ToDoItem ToDomain()
        {
            var newToDoItem = new ToDoItem()
            {
                Name = Name,
                Description = Description,
                IsCompleted = IsCompleted,
            };
            return newToDoItem;
        }
    }
}
