using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebDatVeMayBay.Models
{
    [Table("ChuyenBay")]
    public class ChuyenBay
    {
        [Key]
        [StringLength(50)]
        public string MaChuyenBay { get; set; } // Khóa chính

        [StringLength(50)]
        [ForeignKey("MayBay")]
        public string MaMayBay { get; set; } // Khóa ngoại

        [StringLength(100)]
        public string SanBayDen { get; set; }

        [StringLength(100)]
        public string SanBayDi { get; set; }

        [StringLength(100)]
        public string DiemDi { get; set; }

        [StringLength(100)]
        public string DiemDen { get; set; }

        public DateTime ThoiGianKhoiHanh { get; set; }

        public DateTime ThoiGianDen { get; set; }

        [StringLength(50)]
        public string ThoiGianBayDuTinh { get; set; }

        public int GiaChuyenBay { get; set; }
        // Navigation property
        public MayBay MayBay { get; set; }


    }
}