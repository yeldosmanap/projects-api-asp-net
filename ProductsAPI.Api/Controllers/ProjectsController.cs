using Microsoft.AspNetCore.Mvc;
using ProductsAPI.Application.Contract.Services;
using ProductsAPI.Application.DTO.Project;
using ProductsAPI.Application.DTO.Task;
using ProductsAPI.Domain.Entities;
using ProductsAPI.Domain.Exceptions;
using ProjectsAPI.DTO.Project;

namespace ProductsAPI.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProjectsController : ControllerBase
{
    private readonly IProjectService _projectsService;
    private readonly ILogger<ProjectsController> _logger;

    public ProjectsController(IProjectService projectsService, ILogger<ProjectsController> logger)
    {
        _projectsService = projectsService;
        _logger = logger;
    }
    
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<Project>))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(void))]
    public async Task<IActionResult> GetAll()
    {
        try
        {
            _logger.LogInformation("Getting all projects");
            var projects = await _projectsService.GetAll();
            
            return Ok(projects);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error while getting projects");
            
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }
    
    [HttpGet("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Project))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(void))]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        try
        {
            _logger.LogInformation("Getting project by id");
            var project = await _projectsService.GetById(id);

            return Ok(project);
        }
        catch (EntityNotFoundException e)
        {
            _logger.LogError(e, "Project has not found");
            
            return NotFound();
        }
    }
    
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(Project))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Add([FromBody] CreateProjectDto createProjectDto)
    {
        try
        {
            _logger.LogInformation("Adding a project");
            var project = await _projectsService.Add(createProjectDto);

            return CreatedAtAction(nameof(Add), new {id = project.Id, createdAt = project.DateCreated}, project);
        } 
        catch (Exception e)
        {
            _logger.LogError(e, "Error while adding project");
            
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }
    
    [HttpPut("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Project))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(void))]
    public async Task<IActionResult> Update(
        [FromRoute] Guid id, 
        [FromBody] UpdateProjectDto updateProjectDto)
    {
        try
        {
            _logger.LogInformation("Updating a project");
            var project = await _projectsService.Update(id, updateProjectDto);

            return Ok(project);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "An error occurred while updating a project {EMessage}", e.Message);
            
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }
    
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Project))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(void))]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        try
        {
            _logger.LogInformation("Deleting a project");
            var project = await _projectsService.Delete(id);

            return Ok(project);
        } 
        catch (Exception e)
        {
            _logger.LogError(e, "Error while deleting a project");
            
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }
    
    [HttpPost("{projectId:guid}/Tasks")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Project))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(void))]
    public async Task<IActionResult> AddTask([FromRoute] Guid projectId, [FromBody] CreateTaskDto createTaskDto)
    {
        try
        {
            _logger.LogInformation("Adding a task to project");       
            var project = await _projectsService.AddTaskToProject(projectId, createTaskDto);

            return Ok(project);
        } 
        catch (Exception e)
        {
            _logger.LogError(e, "Error while adding a task to project");
            
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }
    
    [HttpGet("{projectId:guid}/Tasks")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<EntityTask>))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(void))]
    public async Task<IActionResult> GetTasks([FromRoute] Guid projectId)
    {
        try
        {
            _logger.LogInformation("Getting tasks of project");
            var tasks = await _projectsService.GetTasks(projectId);

            return Ok(tasks);
        } 
        catch (Exception e)
        {
            _logger.LogError(e, "Error while getting tasks of project");
            
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }
}