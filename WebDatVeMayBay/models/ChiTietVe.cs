using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebDatVeMayBay.Models
{
    [Table("ChiTietVe")]
    public class ChiTietVe
    {
        [Key]
        [Column(Order = 1)]
        public string MaVe { get; set; }

        [Key]
        [Column(Order = 2)]
        public string MaCTVe { get; set; }

        [Required]
        [StringLength(50)]
        public string DanhXung { get; set; }

        [Required]
        [StringLength(100)]
        public string Ho { get; set; }

        [Required]
        [StringLength(100)]
        public string Ten { get; set; }

        [Required]
        public DateTime NgaySinh { get; set; }

        [Required]
        [StringLength(50)]
        public string LoaiHanhKhach { get; set; }

        [StringLength(100)]
        public string Email { get; set; }

        [StringLength(50)]
        public string LoaiDienThoai { get; set; }

        [StringLength(20)]
        public string SoDienThoai { get; set; }

        // Foreign key relationship with Ve table
        [ForeignKey("MaVe")]
        public virtual Ve Ve { get; set; }
    }
}