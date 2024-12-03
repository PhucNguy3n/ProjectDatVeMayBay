using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebDatVeMayBay.Models;

namespace WebDatVeMayBay.Controllers
{
    public class AccountController : Controller
    {
        private readonly DBContext db;
        public AccountController()
        {
            db = new DBContext();
        }
        // GET: Account
        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Register(string emailKH, string tenKH, string cccd, DateTime ngaySinh, string sdt, string gioiTinh, string matKhau, string xacNhanMatKhau)
        {
            if (string.IsNullOrEmpty(emailKH) || string.IsNullOrEmpty(tenKH) || string.IsNullOrEmpty(cccd) || string.IsNullOrEmpty(sdt) ||
                string.IsNullOrEmpty(gioiTinh) || string.IsNullOrEmpty(matKhau) || string.IsNullOrEmpty(xacNhanMatKhau))
            {
                ViewBag.ErrorMessage = "Vui lòng điền đầy đủ thông tin.";
                return View();
            }

            if (matKhau != xacNhanMatKhau)
            {
                ViewBag.ErrorMessage = "Mật khẩu không khớp.";
                return View();
            }
            var existingCustomer = db.KhachHang.SingleOrDefault(k => k.EmailKH == emailKH);
            if (existingCustomer != null)
            {
                ViewBag.ErrorMessage = "Email đã tồn tại.";
                return View();
            }
            var khachHang = new KhachHang
            {
                EmailKH = emailKH,
                TenKH = tenKH,
                CCCD = cccd,
                NgaySinh = ngaySinh,
                SDT = sdt,
                GioiTinh = gioiTinh
            };
            db.KhachHang.Add(khachHang);

            var taiKhoan = new TaiKhoan
            {
                EmailKH = emailKH,
                MatKhau = matKhau, 
                VaiTro = "KhachHang"
            };

            db.TaiKhoan.Add(taiKhoan);
            db.SaveChanges();


            TempData["SuccessMessage"] = "Đăng ký thành công. Vui lòng đăng nhập.";
            return RedirectToAction("Login", "Account");
        }
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(string email, string password)
        {
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                ViewBag.ErrorMessage = "Email và mật khẩu không được để trống.";
                return View();
            }

            // Kiểm tra tài khoản dựa trên email hoặc mã nhân viên
            var taiKhoan = db.TaiKhoan.SingleOrDefault(t =>
                (email.StartsWith("NV") && t.MaNV == email) ||
                (!email.StartsWith("NV") && t.EmailKH == email)
            );

            if (taiKhoan == null)
            {
                ViewBag.ErrorMessage = "Tài khoản không tồn tại.";
                return View();
            }        
            if (taiKhoan.MatKhau != password) 
            {
                ViewBag.ErrorMessage = "Mật khẩu không chính xác.";
                return View();
            }

            // Đăng nhập thành công, lưu thông tin người dùng vào session
            Session["EmailKH"] = taiKhoan.EmailKH;
            Session["Role"] = taiKhoan.VaiTro;
            //Session["MaNV"] = taiKhoan.MaNV;


            return RedirectToAction("Index", "Home"); 
        }
        [HttpPost]
        public ActionResult Logout()
        {
            Session["EmailKH"] = null;
            Session["Role"] = null;
            return RedirectToAction("Index", "Home");
        }
        public ActionResult PassReset()
        {
            return View();
        }
    }
}