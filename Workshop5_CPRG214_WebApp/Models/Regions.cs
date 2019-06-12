using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Workshop5_CPRG214_WebApp.Models
{
    public partial class Regions
    {
        public Regions()
        {
            BookingDetails = new HashSet<BookingDetails>();
        }
        [Key]
        public string RegionId { get; set; }
        public string RegionName { get; set; }

        public ICollection<BookingDetails> BookingDetails { get; set; }
    }
}
