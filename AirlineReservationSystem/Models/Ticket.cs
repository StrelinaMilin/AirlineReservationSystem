using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AirlineReservationSystem.Models
{
    public class Ticket
    {
        [Key]
        public string FlightName { get; set; }
        public string PassengerName { get; set; }
        public string seatno { get; set; }
        public int? Age { get; set; }
    }
}
