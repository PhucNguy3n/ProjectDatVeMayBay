using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace WebDatVeMayBay.Models
{
    public class DBContext : DbContext
    {
        public DBContext() : base("name=ketnoiSQL")
        {
        }
        public DbSet<MayBay> MayBay { get; set; }
        public DbSet<ChuyenBay> ChuyenBay { get; set; }
        public DbSet<TaiKhoan> TaiKhoan { get; set; }
        public DbSet<KhachHang> KhachHang { get; set; }
        public DbSet<NhanVien> NhanVien { get; set; }
        public DbSet<Ve> Ve { get; set; }
        public DbSet<DatCho> DatCho { get; set; }
        public DbSet<ChiTietVe> ChiTietVe { get; set; }
    }
}