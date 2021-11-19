using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace AirlineReservationSystem.Models
{
    public partial class TblPassenger
    {
        public int PassengerId { get; set; }
        public int? Userid { get; set; }
        public int? FlightDetailsId { get; set; }
        [Required]
        public string PassengerName { get; set; }
        [Required]
        public int? Age { get; set; }
        [Required]
        public string Seatno { get; set; }
        public int? Tid { get; set; }

        public virtual TblFlightDetail FlightDetails { get; set; }
        public virtual Tblticket TidNavigation { get; set; }
        public virtual TblUser User { get; set; }
    }
}
