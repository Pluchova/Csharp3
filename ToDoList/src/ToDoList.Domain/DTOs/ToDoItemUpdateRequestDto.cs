namespace ToDoList.Domain.DTOs;

using System;
using ToDoList.Domain.Models;

public record class ToDoItemUpdateRequestDto(string Name, string Description, bool IsCompleted)
{
    public ToDoItem ToDomain(int toDoItemId)
    {
        return new ToDoItem
        {
            ToDoItemId = toDoItemId,
            Name = Name,
            Description = Description,
            IsCompleted = IsCompleted
        };
    }

    public ToDoItem ToDomain() => throw new NotImplementedException();
}
