
using Microsoft.AspNetCore.Mvc;
using ToDoList.Domain.DTOs;
using ToDoList.Domain.Models;
using ToDoList.Persistence;

[ApiController]
[Route("api/[controller]")]
public class ToDoItemsController : ControllerBase
{
    public readonly List<ToDoItem> items = []; // po dopsání úkolu již není potřeba a můžeme tento field smazat ;)
    private readonly ToDoItemsContext context;
    public ToDoItemsController(ToDoItemsContext context)
    {
        this.context = context;
    }

    [HttpPost]
    public ActionResult<ToDoItemGetResponseDto> Create(ToDoItemCreateRequestDto request)
    {
        var item = request.ToDomain();
        try
        {
            context.ToDoItems.Add(item);
            context.SaveChanges();
        }
        catch (Exception ex)
        {
            return Problem(ex.Message, null, StatusCodes.Status500InternalServerError); //500
        }

        //respond to client
        return CreatedAtAction(
            nameof(ReadById),
            new { toDoItemId = item.ToDoItemId },
            ToDoItemGetResponseDto.FromDomain(item)); //201
    }

    [HttpGet]

    public ActionResult<IEnumerable<ToDoItemGetResponseDto>> Read()
    {
        List<ToDoItem> itemsToGet;
        try
        {
            itemsToGet = context.ToDoItems.ToList();
        }
        catch (Exception ex)
        {
            return Problem(ex.Message, null, StatusCodes.Status500InternalServerError); //500
        }

        //respond to client
        return (itemsToGet is null || itemsToGet.Count is 0)
            ? NotFound() //404
            : Ok(itemsToGet.Select(ToDoItemGetResponseDto.FromDomain)); //200
    }

    [HttpGet("{toDoItemId:int}")]
    public ActionResult<ToDoItemGetResponseDto> ReadById(int toDoItemId)
    {
        //try to retrieve the item by id
        ToDoItem? itemToGet;
        try
        {
            itemToGet = context.ToDoItems.Find(toDoItemId);
        }
        catch (Exception ex)
        {
            return Problem(ex.Message, null, StatusCodes.Status500InternalServerError); //500
        }

        //respond to client
        return (itemToGet is null)
            ? NotFound() //404
            : Ok(ToDoItemGetResponseDto.FromDomain(itemToGet)); //200
    }

    [HttpPut("{toDoItemId:int}")]
    public IActionResult UpdateById(int toDoItemId, [FromBody] ToDoItemUpdateRequestDto request)
    {
        //map to Domain object as soon as possible
        var updatedItem = request.ToDomain();

        //try to update the item by retrieving it with given id
        try
        {
            //retrieve the item
            var itemIndexToUpdate = context.ToDoItems.Find(toDoItemId);
              if (updatedItem is null)
        {
            return NotFound(); //404
        }

        // Aktualizace hodnot (např. názvu, popisu atd.)
        itemIndexToUpdate.Name = updatedItem.Name;
        itemIndexToUpdate.Description = updatedItem.Description;
        itemIndexToUpdate.IsCompleted = updatedItem.IsCompleted;

        context.SaveChanges();
        }
        catch (Exception ex)
        {
            return Problem(ex.Message, null, StatusCodes.Status500InternalServerError); //500
        }

        //respond to client
        return NoContent(); //204
    }

    [HttpDelete("{toDoItemId:int}")]
    public IActionResult DeleteById(int toDoItemId)
    {
        //try to delete the item
        try
        {
            var itemToDelete = context.ToDoItems.Find(toDoItemId);
            if (itemToDelete is null)
            {
                return NotFound(); //404
            }
            context.ToDoItems.Remove(itemToDelete);
            context.SaveChanges();
        }
        catch (Exception ex)
        {
            return Problem(ex.Message, null, StatusCodes.Status500InternalServerError);
        }

        //respond to client
        return NoContent(); //204
    }
}
