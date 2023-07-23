using Microsoft.AspNetCore.Mvc;
using ProductsAPI.Application.Contract.Services;
using ProjectsAPI.DTO.Task;

namespace ProductsAPI.Api.Controllers;

[ApiController]
[Route($"api/[controller]")]
public class TasksController : ControllerBase
{
    private readonly ITaskService _tasksService;
    private readonly ILogger<TasksController> _logger;

    public TasksController(ITaskService tasksService, ILogger<TasksController> logger)
    {
        _tasksService = tasksService;
        _logger = logger;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<Task>))]
    public async Task<IActionResult> GetAll()
    {
        _logger.LogInformation("Getting all tasks");
        var tasks = await _tasksService.GetAll();

        return Ok(tasks);
    }

    [HttpGet("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Task))]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        _logger.LogInformation("Getting a task by id");
        var task = await _tasksService.GetById(id);

        return Ok(task);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateTaskDto updateTaskDto)
    {
        _logger.LogInformation("Updating a task");
        var updatedTask = await _tasksService.Update(updateTaskDto);

        return Ok(updatedTask);
    }

    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Task))]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        _logger.LogInformation("Deleting a task");
        var deletedTask = await _tasksService.Delete(id);

        return Ok(deletedTask);
    }
}