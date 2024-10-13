using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ToDoList.Domain.Models
{
    public record ToDoItemUpdateRequestDto(string Name, string Description, bool IsCompleted)
    {

    }
}
