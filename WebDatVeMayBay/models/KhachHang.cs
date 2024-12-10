using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebDatVeMayBay.Models
{
    [Table("KhachHang")]

    public class KhachHang
    {
        [Key]
        [Required]
        [StringLength(100)]
        public string EmailKH { get; set; } // Email khách hàng (Primary Key)

        [Required]
        [StringLength(50)]
        public string TenKH { get; set; } // Tên khách hàng

        [StringLength(50)]
        public string CCCD { get; set; } // Căn cước công dân

        [DataType(DataType.Date)]
        public DateTime? NgaySinh { get; set; } // Ngày sinh (có thể null)

        [StringLength(11)]
        [Phone]
        public string SDT { get; set; } // Số điện thoại

        [StringLength(3)]
        public string GioiTinh { get; set; } // Giới tính (VD: Nam, Nữ)

        public virtual ICollection<DatCho> DatChos { get; set; }
    }
}