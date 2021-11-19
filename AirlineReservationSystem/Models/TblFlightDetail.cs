using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace AirlineReservationSystem.Models
{
    public partial class TblFlightDetail
    {
        public TblFlightDetail()
        {
            TblPassengers = new HashSet<TblPassenger>();
            Tbltickets = new HashSet<Tblticket>();
        }
        
        public int FlightDetailsId { get; set; }
        [Required]
        public int? Flightid { get; set; }
        [Required]
        public string Source { get; set; }
        [Required]
        public string Destination { get; set; }
        [Required]
        [Display(Name ="EconomySeats")]
        public int? AvailableEconomySeats { get; set; }
        [Required]
        [Display(Name = "BusinessSeats")]
        public int? AvailableBusinessSeats { get; set; }
        [Required]
        public decimal? EconomyclassFare { get; set; }
        [Required]
        public decimal? BusinessclassFare { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime? DepatureDate { get; set; }
        [Required]
        public TimeSpan? DepatureTime { get; set; }

        public virtual TblFlightMaster Flight { get; set; }
        public virtual ICollection<TblPassenger> TblPassengers { get; set; }
        public virtual ICollection<Tblticket> Tbltickets { get; set; }
    }
}
