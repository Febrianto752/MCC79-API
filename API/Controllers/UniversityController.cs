using API.Contracts;
using API.Models;
using API.Utilities;
using Microsoft.AspNetCore.Mvc;
using System.Net;

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
        var universities = _repository.GetByName(name);

        if (!universities.Any())
        {
            return NotFound(new ResponseHandler<University>()
            {
                Code = StatusCodes.Status404NotFound,
                Status = HttpStatusCode.NotFound.ToString(),
                Message = "University not found by given name",
            });
        }

        return Ok(new ResponseHandler<IEnumerable<University>>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "university data found by given name",
            Data = universities
        });
    }
}