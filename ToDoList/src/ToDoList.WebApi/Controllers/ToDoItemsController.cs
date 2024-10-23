
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
            return Problem(ex.Message, null, StatusCodes.Status500InternalServerError);
        }

        return CreatedAtAction(
            nameof(ReadById),
            new { toDoItemId = item.ToDoItemId },
            ToDoItemGetResponseDto.FromDomain(item));

    }

    [HttpGet]
    public ActionResult<IEnumerable<ToDoItemGetResponseDto>> Read()
    public ActionResult<IEnumerable<ToDoItemGetResponseDto>> Read()
    {
        try
        {
            if (!items.Any())
            {
                return NotFound();
            }
            var result = items.Select(ToDoItemGetResponseDto.FromDomain).ToList();
            return Ok(result);
        }
        catch (Exception ex)
        {
            return Problem(ex.Message, null, StatusCodes.Status500InternalServerError);
        }
    }

    [HttpGet("{toDoItemId:int}")]
    public ActionResult<ToDoItemGetResponseDto> ReadById(int toDoItemId)
    {
        try
        {
            var item = items.Find(i => i.ToDoItemId == toDoItemId);
            if (item == null)
            {
                return NotFound();
            }
            return Ok(ToDoItemGetResponseDto.FromDomain(item));
        }
        catch (Exception ex)
        {
            return Problem(ex.Message, null, StatusCodes.Status500InternalServerError);
        }
    }

    [HttpPut("{toDoItemId:int}")]
    public IActionResult UpdateById(int toDoItemId, [FromBody] ToDoItemUpdateRequestDto request)
    {
        try
        {
            var index = items.FindIndex(i => i.ToDoItemId == toDoItemId);
            if (index == -1)
            {
                return NotFound();
            }
            var updatedToDoItem = request.ToDomain(toDoItemId);
            items[index] = updatedToDoItem;

            return NoContent();
        }
        catch (Exception ex)
        {
            return Problem(ex.Message, null, StatusCodes.Status500InternalServerError);
        }
    }

    [HttpDelete("{toDoItemId:int}")]
    public IActionResult DeleteById(int toDoItemId)
    {
        try
        {
            var item = items.Find(i => i.ToDoItemId == toDoItemId);
            if (item == null)
            {
                return NotFound();
            }
            items.Remove(item);
            return NoContent();
        }
        catch (Exception ex)
        {
            return Problem(ex.Message, null, StatusCodes.Status500InternalServerError);
        }
    }
}
