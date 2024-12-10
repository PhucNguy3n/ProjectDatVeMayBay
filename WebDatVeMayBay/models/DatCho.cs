using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebDatVeMayBay.Models
{
    [Table("DatCho")]
    public class DatCho
    {
        [Key]
        [StringLength(50)]
        public string MaDatCho { get; set; }

        public int SoLuong { get; set; }

        [StringLength(100)]
        public string EmailKH { get; set; }

        // Mối quan hệ với bảng KhachHang
        [ForeignKey("EmailKH")]
        public virtual KhachHang KhachHang { get; set; }

        public virtual ICollection<Ve> Ves { get; set; }
    }
}