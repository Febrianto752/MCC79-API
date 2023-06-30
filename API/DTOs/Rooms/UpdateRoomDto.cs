using System.ComponentModel.DataAnnotations;

namespace API.DTOs.Rooms
{
    public class UpdateRoomDto
    {
        [Required]
        public Guid Guid { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        [Range(0, 30)]
        public int Floor { get; set; }
        [Required]
        [Range(0, 20)]
        public int Capacity { get; set; }
    }
}
