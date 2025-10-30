using Microsoft.AspNetCore.Mvc;
using ShopSync_API.Interfaces;
using ShopSync_API.Models;

namespace ShopSync_API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TaskItemsController : ControllerBase
{
    private ITaskRepository taskRepository;
    public TaskItemsController(ITaskRepository taskRepository)
    {
        this.taskRepository = taskRepository;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var taskItems = await taskRepository.GetAllTaskItemsAsync();
        if (taskItems == null)
            return NotFound();
        return Ok(taskItems);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var taskItem = await taskRepository.GetTaskItemAsync(id);
        if (taskItem == null)
            return NotFound();
        return Ok(taskItem);
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromForm] TaskItem taskItem)
    {
        bool isAdded = await taskRepository.PostTaskItemAsync(taskItem);
        if (isAdded)
        {
            return StatusCode(StatusCodes.Status201Created);
        }
        return BadRequest("Something went wrong");
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put([FromForm] TaskItem taskItem, int id)
    {
        bool isUpdated = await taskRepository.PutTaskItemAsync(taskItem, id);
        if (isUpdated)
            return Ok("Record successfully updated");

        return BadRequest("Something went wrong");
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        bool isDeleted = await taskRepository.DeleteTaskItemAsync(id);
        if (isDeleted)
            return Ok("Record has been deleted");

        return BadRequest("Something went wrong");
    }
}
