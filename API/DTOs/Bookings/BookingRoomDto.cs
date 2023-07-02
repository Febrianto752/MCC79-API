namespace API.DTOs.Bookings;

public class BookingRoomDto
{
    public Guid RoomGuid { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public string RoomName { get; set; }
}

