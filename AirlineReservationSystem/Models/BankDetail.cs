using System;
using System.Collections.Generic;

#nullable disable

namespace AirlineReservationSystem.Models
{
    public partial class BankDetail
    {
        public int Bid { get; set; }
        public string Cardholdername { get; set; }
        public string Cardtype { get; set; }
        public string Cardno { get; set; }
        public string Cvv { get; set; }
        public decimal? Balance { get; set; }
    }
}
