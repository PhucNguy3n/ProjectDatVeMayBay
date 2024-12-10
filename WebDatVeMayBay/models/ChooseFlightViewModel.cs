using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebDatVeMayBay.Models
{
    public class ChooseFlightViewModel
    {
        public List<ChuyenBay> OutboundFlights { get; set; } // Chuyến bay chiều đi
        public List<ChuyenBay> ReturnFlights { get; set; }   // Chuyến bay chiều về (nếu có)
        public bool IsReturnTrip { get; set; }              // Cờ xác định có phải chiều về không   
    }   
}