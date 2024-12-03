using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebDatVeMayBay.Models;

namespace WebDatVeMayBay.Controllers
{
    public class HomeController : Controller
    {
        private readonly DBContext db;
        public HomeController()
        {
            db = new DBContext();
        }
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult InfoTraveler()
        {
            return View();
        }
        [HttpPost]
        public ActionResult SaveTraveler(FormCollection form)
        {
            string maVe = Session["MaVe"] as string;

            if (string.IsNullOrEmpty(maVe))
            {
                // Nếu không có mã vé, redirect hoặc báo lỗi.
                return RedirectToAction("Error");
            }

            // Lưu thông tin người lớn (nếu có)
            if (form["Ten"] != null && form["Ho"] != null)
            {
                var travelerAdult = new ChiTietVe
                {
                    MaVe = maVe,
                    MaCTVe = Guid.NewGuid().ToString(),
                    DanhXung = form["DanhXung"],
                    Ho = form["Ho"],
                    Ten = form["Ten"],
                    NgaySinh = DateTime.Parse(form["NgaySinh"]),
                    LoaiHanhKhach = "NGƯỜI LỚN",
                    Email = form["email"],
                    LoaiDienThoai = form["phone-type"],
                    SoDienThoai = form["phone-number"]
                };

                // Lưu dữ liệu vào database (chỉnh sửa theo cách lưu dữ liệu của bạn)
                db.ChiTietVe.Add(travelerAdult);
            }

            // Lưu thông tin trẻ em (nếu có)
            if (form["TenTE"] != null && form["HoTE"] != null)
            {
                var travelerChild = new ChiTietVe
                {
                    MaVe = maVe,
                    MaCTVe = Guid.NewGuid().ToString(),
                    DanhXung = form["DanhXungTE"],
                    Ho = form["HoTE"],
                    Ten = form["TenTE"],
                    NgaySinh = DateTime.Parse(form["NgaySinhTE"]),
                    LoaiHanhKhach = "TRẺ EM",
                    Email = string.IsNullOrEmpty(form["email"]) ? null : form["email"],
                    LoaiDienThoai = string.IsNullOrEmpty(form["phone-type"]) ? null : form["phone-type"],
                    SoDienThoai = string.IsNullOrEmpty(form["phone-number"]) ? null : form["phone-number"]
                };

                // Lưu dữ liệu vào database (chỉnh sửa theo cách lưu dữ liệu của bạn)
                db.ChiTietVe.Add(travelerChild);
            }

            db.SaveChanges();

            return RedirectToAction("FlightShopping");
        }



        public ActionResult FlightShopping()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ThanhToan()
        {
            string maVe = Session["MaVe"]?.ToString();

            if (string.IsNullOrEmpty(maVe))
            {
                return RedirectToAction("Error", "Home");
            }
            var ve = db.Ve.FirstOrDefault(v => v.MaVe == maVe);
            if (ve == null)
            {
                return RedirectToAction("Error", "Home");
            }

            ve.DaThanhToan = true;
            ve.TrangThai = "Đã Thanh Toán";

            db.SaveChanges();

            return RedirectToAction("ThongBaoThanhToan", "Home");
        }


        // Trang thông báo thanh toán thành công
        public ActionResult ThongBaoThanhToan()
        {
            return View();
        }

        public ActionResult ChooseFlight(string DiemDi, string DiemDen, DateTime? ThoiGianKhoiHanh)
        {

            var flights = db.ChuyenBay.AsQueryable();
            if (!string.IsNullOrEmpty(DiemDi))
            {
                flights = flights.Where(f => f.DiemDi.Contains(DiemDi));
            }
            if (!string.IsNullOrEmpty(DiemDen))
            {
                flights = flights.Where(f => f.DiemDen.Contains(DiemDen));
            }
            if (ThoiGianKhoiHanh.HasValue)
            {
                // Lấy phần ngày (ngày tháng năm) của ThoiGianKhoiHanh
                var thoiGianKhoiHanhNgay = ThoiGianKhoiHanh.Value.Date;

                // So sánh ngày của ThoiGianKhoiHanh trong cơ sở dữ liệu với phần ngày đã chọn
                flights = flights.Where(f =>
                    EntityFunctions.TruncateTime(f.ThoiGianKhoiHanh) == thoiGianKhoiHanhNgay);
            }

            ViewBag.Flights = flights.ToList();
            return View(flights.ToList());
        }

        [HttpPost]
        public ActionResult ChonChuyenBay(string maChuyenBay)
        {
            var flight = db.ChuyenBay.FirstOrDefault(f => f.MaChuyenBay == maChuyenBay);

            if (flight != null)
            {
                // Tạo mã vé ngẫu nhiên
                string maVe = GenerateRandomMaVe();
                Session["MaVe"] = maVe;

                var ve = new Ve
                {
                    MaVe = maVe,
                    MaChuyenBay = flight.MaChuyenBay,
                    MaNV = null,   
                    MaDatCho = null, 
                    ThoiGianDatVe = DateTime.Now, 
                    ThanhTien = flight.GiaChuyenBay, 
                    DaThanhToan = false, // Mặc định chưa thanh toán
                    TrangThai = "Chờ xác nhận",
                    Hang = "VietnamAirline"
                };

                // Lưu vé vào cơ sở dữ liệu
                db.Ve.Add(ve);
                db.SaveChanges();

                // Chuyển qua trang InfoTraveler, truyền mã vé và thông tin chuyến bay
                return RedirectToAction("InfoTraveler", new { maVe = maVe });
            }
            else
            {
                return View("Error");  // Nếu không tìm thấy chuyến bay, trả về trang lỗi
            }
        }
        private string GenerateRandomMaVe()
        {
            return Guid.NewGuid().ToString("N").Substring(0, 10);  // Mã vé dài 10 ký tự
        }

    }
}