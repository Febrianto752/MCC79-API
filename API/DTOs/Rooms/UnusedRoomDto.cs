using System.ComponentModel.DataAnnotations;

namespace API.DTOs.Rooms
{
    public class UnusedRoomDto
    {
        [Required]
        public Guid RoomGuid { get; set; }
        [Required]
        public string RoomName { get; set; }
        [Required]
        public int Floor { get; set; }
        [Required]
        public int Capacity { get; set; }
    }
}
