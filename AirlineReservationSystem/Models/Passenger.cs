using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AirlineReservationSystem.Models
{
    public class Passenger
    {
        [Key]
        public int pid { get; set; }
        [Required]
        public string PassengerName { get; set; }
        [Required]
        public int? Age { get; set; }
        [Required]
        public string Seatno { get; set; }
    }
}
