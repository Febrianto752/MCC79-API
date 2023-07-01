using System.ComponentModel.DataAnnotations;

namespace API.DTOs.Bookings
{
    public class DateBookingDto
    {
        [Required]
        public DateTime BookingDate { get; set; }
    }
}
