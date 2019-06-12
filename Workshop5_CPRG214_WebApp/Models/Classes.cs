using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Workshop5_CPRG214_WebApp.Models
{
    public partial class Classes
    {
        public Classes()
        {
            BookingDetails = new HashSet<BookingDetails>();
        }
        [Key]
        public string ClassId { get; set; }
        public string ClassName { get; set; }
        public string ClassDesc { get; set; }

        public ICollection<BookingDetails> BookingDetails { get; set; }
    }
}
