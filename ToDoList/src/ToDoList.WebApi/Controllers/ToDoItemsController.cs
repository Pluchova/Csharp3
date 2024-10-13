using System.Linq.Expressions;
using Microsoft.AspNetCore.Mvc;
using ToDoList.Domain.DTOs;
using ToDoList.Domain.Models;

namespace ToDoList.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ToDoItemsController : ControllerBase
    {
        private static List<ToDoItem> items = [];

        //DTO - Data Transfer Object

        [HttpPost]
        public IActionResult Create(ToDoItemCreateRequestDto request)
        {
            try
            {
                var newToDoItem = request.ToDomain();
                var id = items.Count + 1;
                newToDoItem.ToDoItemId = id;
                items.Add(newToDoItem);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message, null, StatusCodes.Status500InternalServerError);
            }
            return Created();
        }


        [HttpGet]
        public IActionResult Read()
        {
            try
            {
                throw new Exception("Něco se pokazilo.");
            }
            catch (Exception ex)
            {
                return this.Problem(ex.Message, null, StatusCodes.Status500InternalServerError);
            }
            return Ok();
        }
        [HttpGet("{toDoItemId:int}")]
        public IActionResult ReadById(int toDoItemId)
        {
            return Ok();
        }

        [HttpPut("{toDoItemId:int}")]
        public IActionResult UpdateById(int toDoItemId, [FromBody] ToDoItemUpdateRequestDto request)
        {
            return Ok();
        }

        [HttpDelete("{toDoItemId:int}")]
        public IActionResult DeleteById(int toDoItemId)
        {
            return Ok();
        }

    }
}
