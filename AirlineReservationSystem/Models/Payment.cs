using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AirlineReservationSystem.Models
{
    public class Payment
    {
        [Key]
        [Required]
        public string CardHolderName { get; set; }
        [Required]
        public string CardType { get; set; }
        [Required]
        [RegularExpression(@"[0-9]{16}", ErrorMessage = "Please enter Valid Card number")]
        public string CardNumber { get; set; }
        [Required]
        [RegularExpression(@"[0-9]{3}", ErrorMessage = "Please enter Valid CVV")]
        public string CVV { get; set; }
        public decimal Amount { get; set; }
    }
}
