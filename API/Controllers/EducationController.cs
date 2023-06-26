using API.Contracts;
using API.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;


[ApiController]
[Route("api/v1/educations")]
public class EducationController : GeneralController<IEducationRepository, Education>
{
    public EducationController(IEducationRepository repository) : base(repository)
    {
    }

}

