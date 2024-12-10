using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace WebDatVeMayBay.Models
{
    [Table("Ve")]
    public class Ve
    {
        [Key]
        [StringLength(50)]
        public string MaVe { get; set; }

        [StringLength(50)]
        public string MaChuyenBay { get; set; }

        [StringLength(50)]
        public string MaNV { get; set; }

        [StringLength(50)]
        public string MaDatCho { get; set; }

        public DateTime ThoiGianDatVe { get; set; }

        public decimal ThanhTien { get; set; }

        public bool DaThanhToan { get; set; }

        [StringLength(50)]
        public string TrangThai { get; set; }

        [StringLength(50)]
        public string Hang { get; set; }

        // Mối quan hệ với các bảng khác
        [ForeignKey("MaChuyenBay")]
        public virtual ChuyenBay ChuyenBay { get; set; }

        [ForeignKey("MaNV")]
        public virtual NhanVien NhanVien { get; set; }

        [ForeignKey("MaDatCho")]
        public virtual DatCho DatCho { get; set; }

        public virtual ICollection<ChiTietVe> ChiTietVes { get; set; }
    }
}