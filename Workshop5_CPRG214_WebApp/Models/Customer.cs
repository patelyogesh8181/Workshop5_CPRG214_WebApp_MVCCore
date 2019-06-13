using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Workshop5_CPRG214_WebApp.Models
{
    public class Customer
    {
        [Key]
        public int CustomerId { get; set; }

        public string CustFirstName { get; set; }
        public string CustLastName { get; set; }
        public string CustAddress { get; set; }
        public string CustCity { get; set; }
        public string CustProv { get; set; }
        [Required(ErrorMessage = "Postal Code is Required")]
        [RegularExpression(@"^[ABCEGHJKLMNPRSTVXY][0-9][ABCEGHJKLMNPRSTVWXYZ] ?[0-9][ABCEGHJKLMNPRSTVWXYZ][0-9]$", ErrorMessage = "Invalid Postal Code")]
        public string CustPostal { get; set; }
        public string CustCountry { get; set; }

        
        [RegularExpression("^[0-9]*$", ErrorMessage = "Home phone must be numeric")]
        public string CustHomePhone { get; set; }

        
        [RegularExpression("^[0-9]*$", ErrorMessage = "Business phone must be numeric")]
        public string CustBusPhone { get; set; }

        [DataType(DataType.EmailAddress)]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string CustEmail { get; set; }

        public int AgentId { get; set; }

        //[NotMapped]
        public string PasswordNotHashed { get; set; }

        [NotMapped]
        [Compare("PasswordNotHashed", ErrorMessage = "Password doesn't match.")]
        public string ConfirmPassword { get; set; }
        [NotMapped]
        public byte[] PasswordHashed { get; set; }
        [NotMapped]
        public string PasswordSalt { get; set; }
    }
}
