

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;



#nullable disable

namespace AirlineReservationSystem.Models
{
    public partial class TblUser
    {
        public TblUser()
        {
            TblPassengers = new HashSet<TblPassenger>();
            Tbltickets = new HashSet<Tblticket>();
        }

        public int Userid { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        [DataType(DataType.EmailAddress, ErrorMessage = "Enter Valid Email Id")]
        public string Emailid { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$", ErrorMessage = "Password should contain minimum 8 Characters & atleast one Uppercase & One number & one special charcter")]
        public string Password { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Password and Confirm password does not match")]
        public string Confirmpassword { get; set; }
        [Required]
        public DateTime? Dateofbirth { get; set; }
        [Required]
        [RegularExpression(@"[789][0-9]{9}", ErrorMessage = "Please enter Valid Phone number")]
        public string Phoneno { get; set; }

        public virtual ICollection<TblPassenger> TblPassengers { get; set; }
        public virtual ICollection<Tblticket> Tbltickets { get; set; }
    }
}
