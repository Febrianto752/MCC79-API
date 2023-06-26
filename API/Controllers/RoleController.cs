using API.Contracts;
using API.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;


[ApiController]
[Route("api/v1/roles")]
public class RoleController : GeneralController<IRoleRepository, Role>
{
    public RoleController(IRoleRepository repository) : base(repository)
    {
    }
}

