using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace AirlineReservationSystem.Models
{
    public partial class Tblticket
    {
        public Tblticket()
        {
            TblPassengers = new HashSet<TblPassenger>();
        }

        public int Tid { get; set; }
        public int? Userid { get; set; }
        public int? FlightDetailsId { get; set; }
        [Required]
        public string Classtype { get; set; }
        [Required]
        public int? TotalSeatsReserved { get; set; }
        [Required]
        public string Type { get; set; }
        public string ReturnDate { get; set; }
       
        public decimal? TotalAmount { get; set; }

        public virtual TblFlightDetail FlightDetails { get; set; }
        public virtual TblUser User { get; set; }
        public virtual ICollection<TblPassenger> TblPassengers { get; set; }
    }
}
