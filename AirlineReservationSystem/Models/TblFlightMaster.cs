using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace AirlineReservationSystem.Models
{
    public partial class TblFlightMaster
    {
        public TblFlightMaster()
        {
            TblFlightDetails = new HashSet<TblFlightDetail>();
        }

        public int Flightid { get; set; }
        [Required]
        public string Fname { get; set; }
        [Required]
        public int? EconomySeats { get; set; }
        [Required]
        public int? BusinessSeats { get; set; }

        public virtual ICollection<TblFlightDetail> TblFlightDetails { get; set; }
    }
}
