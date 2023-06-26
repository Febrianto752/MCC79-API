using API.Contracts;
using API.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/v1/universities")]
public class UniversityController : GeneralController<IUniversityRepository, University>
{

    public UniversityController(IUniversityRepository repository) : base(repository)
    {
    }

    [HttpGet("by-name/{name}")]
    public IActionResult GetByName(string name)
    {
        Console.WriteLine(name);
        var entities = _repository.GetByName(name);

        if (!entities.Any())
        {
            return NotFound();
        }

        return Ok(entities);
    }
}