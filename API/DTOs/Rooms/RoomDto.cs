namespace API.DTOs.Rooms
{
    public class RoomDto
    {
        public Guid GUID { get; set; }
        public string Name { get; set; }
        public int Floor { get; set; }
        public int Capacity { get; set; }
    }
}
