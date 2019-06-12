using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Workshop5_CPRG214_WebApp.Models
{
    public partial class TripTypes
    {
        [Key]
        public string TripTypeId { get; set; }
        public string Ttname { get; set; }

        public ICollection<Bookings> Bookings { get; set; }
    }
}
