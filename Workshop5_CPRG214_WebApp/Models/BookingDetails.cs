using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Workshop5_CPRG214_WebApp.Models
{
    public partial class BookingDetails
    {
        [Key]
        public int BookingDetailId { get; set; }
        public double? ItineraryNo { get; set; }
        public DateTime? TripStart { get; set; }
        public DateTime? TripEnd { get; set; }
        public string Description { get; set; }
        public string Destination { get; set; }
        public decimal? BasePrice { get; set; }
        public decimal? AgencyCommission { get; set; }

        //[ForeignKey("BookingId")]
        public int? BookingId { get; set; }

        //[ForeignKey("RegionId")]
        public string RegionId { get; set; }

        //[ForeignKey("ClassId")]
        public string ClassId { get; set; }

        //[ForeignKey("FeeId")]
        public string FeeId { get; set; }

        //[ForeignKey("ProductSupplierId")]
        public int? ProductSupplierId { get; set; }

        public Bookings Booking { get; set; }
        public Classes Class { get; set; }
        public Fees Fee { get; set; }
        //public ProductsSuppliers ProductSupplier { get; set; }
        public Regions Region { get; set; }
    }
}
