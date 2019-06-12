using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Workshop5_CPRG214_WebApp.Models
{
    public class Bookings
    {
        [Key]
        public int BookingId { get; set; }
        public DateTime? BookingDate { get; set; }
        public string BookingNo { get; set; }
        public double? TravelerCount { get; set; }

        [ForeignKey("CustomerId")]
        public int? CustomerId { get; set; }

        [ForeignKey("TripTypeId")]
        public string TripTypeId { get; set; }

        [ForeignKey("PackageId")]
        public int? PackageId { get; set; }

        public Customer Customer { get; set; }
        public Packages Package { get; set; }
        public TripTypes TripType { get; set; }
        public ICollection<BookingDetails> BookingDetails { get; set; }
    }
}
