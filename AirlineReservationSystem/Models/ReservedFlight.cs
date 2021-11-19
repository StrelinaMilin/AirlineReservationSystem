using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AirlineReservationSystem.Models
{
    public class ReservedFlight
    {
        [Key]
        public string Rid { get; set; }
        public int Userid { get; set; }
        public int Tid { get; set; }
        public string FlightName { get; set; }
        public string Source { get; set; }
        public string Destination { get; set; }
        public string ClassType { get; set; }
        [Display(Name ="SeatsReserved")]
        public int TotalSeatsReserved { get; set; }
        public string DepatureDate { get; set; }
        public string DepatureTime { get; set; }
        public string TicketType { get; set; }
        public string ReturnDate { get; set; }
        public decimal Amount { get; set; }
    }
}
