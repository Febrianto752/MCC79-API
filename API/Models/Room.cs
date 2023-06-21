﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models
{
    [Table("tb_m_rooms")]
    public class Room : BaseEntity
    {
        [Key]

        [Column("name", TypeName = "nvarchar(50)")]
        public string Name { get; set; }

        [Column("floor")]
        public int Floor { get; set; }

        [Column("capacity")]
        public int Capacity { get; set; }
    }
}
