﻿using API.Utilities.Enums;

namespace API.DTOs.Rooms;

public class UsedRoomDto
{
    public Guid BookingGuid { get; set; }
    public string RoomName { get; set; }
    public StatusLevel Status { get; set; }
    public int Floor { get; set; }
    public string BookedBy { get; set; }
}

