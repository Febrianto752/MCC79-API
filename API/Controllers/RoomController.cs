using API.DTOs.Rooms;
using API.Services;
using API.Utilities;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace API.Controllers;

[ApiController]
[Route("api/v1/rooms")]
public class RoomController : ControllerBase
{
    private readonly RoomService _service;

    public RoomController(RoomService service)
    {
        _service = service;
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        var rooms = _service.GetRoom();

        if (!rooms.Any())
        {
            return NotFound(new ResponseHandler<RoomDto>
            {
                Code = StatusCodes.Status404NotFound,
                Status = HttpStatusCode.NotFound.ToString(),
                Message = "Data not found"
            });
        }

        return Ok(new ResponseHandler<IEnumerable<RoomDto>>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Data found",
            Data = rooms
        });
    }

    [HttpGet("{guid}")]
    public IActionResult GetByGuid(Guid guid)
    {
        var room = _service.GetRoom(guid);
        if (room is null)
        {
            return NotFound(new ResponseHandler<RoomDto>
            {
                Code = StatusCodes.Status404NotFound,
                Status = HttpStatusCode.NotFound.ToString(),
                Message = "Data not found"
            });
        }

        return Ok(new ResponseHandler<RoomDto>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Data found",
            Data = room
        });
    }

    [HttpPost]
    public IActionResult Create(NewRoomDto newRoomDto)
    {
        var createdRoom = _service.CreateRoom(newRoomDto);
        if (createdRoom is null)
        {
            return BadRequest(new ResponseHandler<RoomDto>
            {
                Code = StatusCodes.Status400BadRequest,
                Status = HttpStatusCode.BadRequest.ToString(),
                Message = "Data not created"
            });
        }

        return Ok(new ResponseHandler<RoomDto>
        {
            Code = StatusCodes.Status201Created,
            Status = HttpStatusCode.Created.ToString(),
            Message = "Successfully created",
            Data = createdRoom
        });
    }

    [HttpPut]
    public IActionResult Update(RoomDto updateRoomDto)
    {
        var update = _service.UpdateRoom(updateRoomDto);
        if (update is -1)
        {
            return NotFound(new ResponseHandler<RoomDto>
            {
                Code = StatusCodes.Status404NotFound,
                Status = HttpStatusCode.NotFound.ToString(),
                Message = "Id not found"
            });
        }
        if (update is 0)
        {
            return BadRequest(new ResponseHandler<RoomDto>
            {
                Code = StatusCodes.Status500InternalServerError,
                Status = HttpStatusCode.InternalServerError.ToString(),
                Message = "Check your data"
            });
        }
        return Ok(new ResponseHandler<RoomDto>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Successfully updated"
        });
    }

    [HttpDelete]
    public IActionResult Delete(Guid guid)
    {
        var delete = _service.DeleteRoom(guid);

        if (delete is -1)
        {
            return NotFound(new ResponseHandler<RoomDto>
            {
                Code = StatusCodes.Status404NotFound,
                Status = HttpStatusCode.NotFound.ToString(),
                Message = "Id not found"
            });
        }
        if (delete is 0)
        {
            return BadRequest(new ResponseHandler<RoomDto>
            {
                Code = StatusCodes.Status500InternalServerError,
                Status = HttpStatusCode.InternalServerError.ToString(),
                Message = "Check connection to database"
            });
        }

        return Ok(new ResponseHandler<RoomDto>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Successfully deleted"
        });
    }


}

