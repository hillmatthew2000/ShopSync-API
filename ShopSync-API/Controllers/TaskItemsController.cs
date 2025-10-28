using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShopSync_API.Data;
using ShopSync_API.Models;

namespace ShopSync_API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TaskItemsController : ControllerBase
{
    private ApiDbContext dbContext;
    public TaskItemsController(ApiDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllTaskItemsAsync()
    {
        return Ok(await dbContext.TaskItems.ToListAsync());
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetTaskItemAsync(int id)
    {
        var existingTaskItem = await dbContext.TaskItems.FirstOrDefaultAsync(t => t.Id == id);
        if (existingTaskItem == null)
            return NotFound($"The task item with id: {id} could not be found");
        return Ok(existingTaskItem);
    }

    [HttpPost]
    public async Task<IActionResult> PostTaskItemAsync([FromForm] TaskItem taskItem)
    {
        await dbContext.TaskItems.AddAsync(taskItem);
        await dbContext.SaveChangesAsync();
        return StatusCode(StatusCodes.Status201Created);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateTaskItem([FromForm] TaskItem taskItem, int id)
    {
        var existingTaskItem = await dbContext.TaskItems.FirstOrDefaultAsync(t => t.Id == id);
        if (existingTaskItem == null)
            return NotFound($"The task item with id: {id} could not be found");

        taskItem.Id = existingTaskItem.Id;
        dbContext.TaskItems.Entry(existingTaskItem).CurrentValues.SetValues(taskItem);
        await dbContext.SaveChangesAsync();
        return StatusCode(StatusCodes.Status202Accepted);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTaskItem(int id)
    {
        var existingTaskItem = await dbContext.TaskItems.FirstOrDefaultAsync(t => t.Id == id);
        if (existingTaskItem == null)
            return NotFound($"The task item with id: {id} could not be found");

        dbContext.TaskItems.Remove(existingTaskItem);
        await dbContext.SaveChangesAsync();
        return Ok();
    }
}
