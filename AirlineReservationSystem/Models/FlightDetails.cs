using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AirlineReservationSystem.Models
{
    public class FlightDetails
    {
        [Key]
        public int fid { get; set; }
        public int Detailsid { get; set; }
        [Display(Name ="Flight")]
        public string FlightName { get; set; }
        public string Source { get; set; }
        public string Destination { get; set; }
        public string DepartureDate { get; set; }
        public string DepartureTime { get; set; }
        [Display(Name ="EconomySeats")]
        public int? AvailableEconomySeats { get; set; }
        public decimal? EconomyClassFare { get; set; }
        [Display(Name = "BusinessSeats")]
        public int? AvailableBusinessSeats { get; set; }
        public decimal? BusinessClassFare { get; set; }
    }
}
