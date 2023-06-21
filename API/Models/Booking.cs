﻿using API.Utilities.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models
{
    [Table("tb_tr_bookings")]
    public class Booking : BaseEntity
    {
        [Key]

        [Column("start_date")]
        public DateTime StartDate { get; set; }

        [Column("end_date")]
        public DateTime EndDate { get; set; }

        [Column("remarks", TypeName = "nvarchar(255)")]
        public string Remarks { get; set; }

        // create status here
        [Column("status")]
        public StatusLevel Status { get; set; }

        [Column("room_guid")]
        public Guid RoomGuid { get; set; }

        [Column("employee_id")]
        public Guid EmployeeId { get; set; }
    }
}
