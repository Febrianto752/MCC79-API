using API.Contracts;
using API.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/v1/employees")]
public class EmployeeController : GeneralController<IEmployeeRepository, Employee>
{
    public EmployeeController(IEmployeeRepository repository) : base(repository)
    {
    }
}

