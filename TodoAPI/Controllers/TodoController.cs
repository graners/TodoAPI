using Microsoft.AspNetCore.Mvc;
using TodoAPI.Contracts;
using TodoAPI.Interface;

namespace TodoAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TodoController(ITodoServices todoServices) : ControllerBase
{
    private readonly ITodoServices _todoServices = todoServices;

    [HttpGet]
    public async Task<IActionResult> GetAllAsync()
    {
        try
        {
            var todo = await _todoServices.GetAllAsync();
            if (todo == null || !todo.Any())
            {
                return Ok(new { message = "No Todo items found." });
            }
            return Ok(new { message = "Successfully retrieved all Todo items.", data = todo });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "An error occurred while retrieving all Tood items.", error = ex.Message });
        }
    }

    [HttpPost]
    public async Task<IActionResult> CreateTodoAsync(CreateTodoRequest request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            await _todoServices.CreateTodoAsync(request);
            return Ok(new { message = "Todo item successfully created." });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "An error occurred while creating the Todo item.", error = ex.Message });
        }
    }
}
