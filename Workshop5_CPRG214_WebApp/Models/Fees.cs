using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Workshop5_CPRG214_WebApp.Models
{
    public partial class Fees
    {
        [Key]
        public string FeeId { get; set; }
        public string FeeName { get; set; }
        public decimal FeeAmt { get; set; }
        public string FeeDesc { get; set; }

        public ICollection<BookingDetails> BookingDetails { get; set; }
    }
}
