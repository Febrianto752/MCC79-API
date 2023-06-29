using API.Contracts;
using API.DTOs.Rooms;
using API.Models;

namespace API.Services;

public class RoomService
{
    private readonly IRoomRepository _roomRepository;

    public RoomService(IRoomRepository roomRepository)
    {
        _roomRepository = roomRepository;
    }

    public IEnumerable<RoomDto>? GetRoom()
    {
        var rooms = _roomRepository.GetAll();
        if (!rooms.Any())
        {
            return null; // No rooms found
        }

        var toDto = rooms.Select(room =>
                                            new RoomDto
                                            {
                                                GUID = room.GUID,
                                                Name = room.Name,
                                                Floor = room.Floor,
                                                Capacity = room.Capacity,
                                            }).ToList();

        return toDto; // Universities found
    }

    public RoomDto? GetRoom(Guid guid)
    {
        var room = _roomRepository.GetByGuid(guid);
        if (room is null)
        {
            return null; // Room not found
        }

        var toDto = new RoomDto
        {
            GUID = room.GUID,
            Name = room.Name,
            Floor = room.Floor,
            Capacity = room.Capacity,
        };

        return toDto; // Universities found
    }

    public RoomDto? CreateRoom(NewRoomDto newRoomDto)
    {
        var room = new Room
        {
            GUID = new Guid(),
            Name = newRoomDto.Name,
            Floor = newRoomDto.Floor,
            Capacity = newRoomDto.Capacity,
            CreatedDate = DateTime.Now,
            ModifiedDate = DateTime.Now
        };

        var createdRoom = _roomRepository.Create(room);
        if (createdRoom is null)
        {
            return null; // Room not created
        }

        var toDto = new RoomDto
        {
            GUID = room.GUID,
            Name = room.Name,
            Floor = room.Floor,
            Capacity = room.Capacity,
        };

        return toDto; // Room created
    }

    public int UpdateRoom(RoomDto updateRoomDto)
    {
        var isExist = _roomRepository.IsExist(updateRoomDto.GUID);
        if (!isExist)
        {
            return -1; // Room not found
        }

        var getRoom = _roomRepository.GetByGuid(updateRoomDto.GUID);

        var room = new Room
        {
            GUID = updateRoomDto.GUID,
            Name = updateRoomDto.Name,
            Floor = updateRoomDto.Floor,
            Capacity = updateRoomDto.Capacity,
            ModifiedDate = DateTime.Now,
            CreatedDate = getRoom!.CreatedDate
        };

        var isUpdate = _roomRepository.Update(room);
        if (!isUpdate)
        {
            return 0; // Room not updated
        }

        return 1;

    }

    public int DeleteRoom(Guid guid)
    {
        var isExist = _roomRepository.IsExist(guid);
        if (!isExist)
        {
            return -1; // Room not found
        }

        var room = _roomRepository.GetByGuid(guid);
        var isDelete = _roomRepository.Delete(room!.GUID);
        if (!isDelete)
        {
            return 0; // Room not deleted
        }

        return 1;
    }
}

