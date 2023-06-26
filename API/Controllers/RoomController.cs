using API.Contracts;
using API.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/v1/rooms")]
public class RoomController : GeneralController<IRoomRepository, Room>
{
    public RoomController(IRoomRepository repository) : base(repository)
    {
    }
}

