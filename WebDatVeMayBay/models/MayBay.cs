using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebDatVeMayBay.Models
{
    [Table("MayBay")]
    public class MayBay
    {
        [Key]
        [StringLength(50)]
        public string MaMayBay { get; set; } // Khóa chính

        [StringLength(50)]
        public string MaGhe { get; set; }

        [StringLength(50)]
        public string TenMayBay { get; set; }

        // Navigation property
        public ICollection<ChuyenBay> ChuyenBay { get; set; } // Liên kết với các chuyến bay
    }
}