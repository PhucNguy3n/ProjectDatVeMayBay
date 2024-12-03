using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebDatVeMayBay.Models
{
    [Table("TaiKhoan")]
    public class TaiKhoan
    {
        [Key]
        public int IDTK { get; set; } // ID Tài khoản (Khóa chính)

        [Required]
        [StringLength(50)]
        public string MatKhau { get; set; } // Mật khẩu

        [Required]
        [StringLength(50)]
        public string VaiTro { get; set; } // Vai trò (VD: Admin, Khách hàng, Nhân viên)

        [StringLength(50)]
        public string MaNV { get; set; }

        [StringLength(100)]
        public string EmailKH { get; set; } 

        // Navigation Properties
        [ForeignKey("EmailKH")]
        public virtual KhachHang KhachHang { get; set; } // Liên kết đến Khách Hàng

        [ForeignKey("MaNV")]
        public virtual NhanVien NhanVien { get; set; } // Liên kết đến Nhân Viên
    }
}