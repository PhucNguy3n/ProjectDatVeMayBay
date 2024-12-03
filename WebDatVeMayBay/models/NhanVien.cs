using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebDatVeMayBay.Models
{
    [Table("NhanVien")]
    public class NhanVien
    {
        [Key]
        [Required]
        [StringLength(50)]
        public string MaNV { get; set; } // Mã nhân viên (Primary Key)

        [Required]
        [StringLength(50)]
        public string TenKH { get; set; } // Tên nhân viên

        [StringLength(3)]
        public string GioiTinh { get; set; } // Giới tính (VD: Nam, Nữ)

        [DataType(DataType.Date)]
        public DateTime? NgaySinh { get; set; } // Ngày sinh (nullable)

        [StringLength(100)]
        public string DiaChi { get; set; } // Địa chỉ

        [StringLength(11)]
        [Phone]
        public string SDT { get; set; } // Số điện thoại

        [StringLength(50)]
        public string TinhTrang { get; set; } // Tình trạng
    }
}