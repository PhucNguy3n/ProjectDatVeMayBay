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
            Session.Clear();
            return View();
        }

        

        public ActionResult InfoTraveler(string maVe, string maveve, int songuoi)
        {
            // Xử lý các tham số nhận được ở đây
            ViewBag.MaVe = maVe;
            ViewBag.MaVeVe = maveve;
            ViewBag.SoNguoi = songuoi;

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
            int soNguoi = ViewBag.SoNguoi;
            // Lưu thông tin người lớn (nếu có)
            if (form["Ten"] != null && form["Ho"] != null)
            {
                for (int i = 0; i < soNguoi; i++)
                {
                    var travelerAdult = new ChiTietVe
                    {
                        MaVe = maVe,
                        MaCTVe = Guid.NewGuid().ToString(),
                        DanhXung = form["DanhXung[" + i + "]"],
                        Ho = form["Ho[" + i + "]"],
                        Ten = form["Ten[" + i + "]"],
                        NgaySinh = DateTime.Parse(form["NgaySinh[" + i + "]"]),
                        LoaiHanhKhach = "NGƯỜI LỚN",
                        Email = form["email"],
                        LoaiDienThoai = form["phone-type"],
                        SoDienThoai = form["phone-number"]
                    };
                    db.ChiTietVe.Add(travelerAdult);
                }

                // Lưu dữ liệu vào database (chỉnh sửa theo cách lưu dữ liệu của bạn)
                
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

        public ActionResult ChooseFlight(string DiemDi, string DiemDen, DateTime? ThoiGianKhoiHanh, DateTime? ThoiGianVe, int soKhach)
        {
            // Lưu thông tin người dùng vào Session
            Session["SoNguoi"] = soKhach;
            Session["DiemDi"] = DiemDi;
            Session["DiemDen"] = DiemDen;
            Session["check"] = 1;
            // Tạo ViewModel và lọc chuyến bay chiều đi
            var viewModel = new ChooseFlightViewModel
            {
                OutboundFlights = new List<ChuyenBay>(),
                ReturnFlights = new List<ChuyenBay>(),
                IsReturnTrip = false
            };

            // Lọc chuyến bay chiều đi
            var outboundFlights = db.ChuyenBay.AsQueryable();
            if (!string.IsNullOrEmpty(DiemDi))
            {
                outboundFlights = outboundFlights.Where(f => f.DiemDi.Contains(DiemDi));
            }
            if (!string.IsNullOrEmpty(DiemDen))
            {
                outboundFlights = outboundFlights.Where(f => f.DiemDen.Contains(DiemDen));
            }
            if (ThoiGianKhoiHanh.HasValue)
            {
                var thoiGianKhoiHanhNgay = ThoiGianKhoiHanh.Value.Date;
                outboundFlights = outboundFlights.Where(f =>
                    EntityFunctions.TruncateTime(f.ThoiGianKhoiHanh) == thoiGianKhoiHanhNgay);
            }
            viewModel.OutboundFlights = outboundFlights.ToList();

            // Nếu người dùng nhập thời gian về, lọc chuyến bay chiều về
            if (ThoiGianVe.HasValue)
            {
                // Nếu có chọn chuyến bay về, cài đặt IsReturnTrip thành true và thêm chuyến bay chiều về
                Session["check"] = 2;
                var returnFlights = db.ChuyenBay.AsQueryable();
                if (!string.IsNullOrEmpty(DiemDi))
                {
                    returnFlights = returnFlights.Where(f => f.DiemDen.Contains(DiemDi));
                }
                if (!string.IsNullOrEmpty(DiemDen))
                {
                    returnFlights = returnFlights.Where(f => f.DiemDi.Contains(DiemDen));
                }
                var thoiGianVeNgay = ThoiGianVe.Value.Date;
                returnFlights = returnFlights.Where(f =>
                    EntityFunctions.TruncateTime(f.ThoiGianKhoiHanh) == thoiGianVeNgay);

                viewModel.ReturnFlights = returnFlights.ToList();
            }

            return View(viewModel);
        }

        [HttpPost]
        public ActionResult ChooseFlight(string maChuyenBay, bool isReturn)
        {
            if (!isReturn)
            {
                // Vé một chiều
                Session["ChuyenBayDi"] = maChuyenBay;

                // Tạo mã vé và lưu thông tin chuyến bay đi
                string maVe = GenerateRandomMaVe();
                Session["MaVeDi"] = maVe;

                var flight = db.ChuyenBay.FirstOrDefault(f => f.MaChuyenBay == maChuyenBay);
                if (flight != null)
                {
                    var ve = new Ve
                    {
                        MaVe = maVe,
                        MaChuyenBay = maChuyenBay,
                        MaNV = null,
                        MaDatCho = null,
                        ThoiGianDatVe = DateTime.Now,
                        ThanhTien = flight.GiaChuyenBay,
                        DaThanhToan = false,
                        TrangThai = "Chờ xác nhận",
                        Hang = "VietnamAirline"
                    };

                    db.Ve.Add(ve);
                    db.SaveChanges();
                    int sokhach = (int)Session["SoNguoi"];
                    // Chuyển sang trang InfoTraveler cho vé một chiều
                    return RedirectToAction("InfoTraveler", new { maVe = maVe, songuoi = sokhach});
                }

                return View("Error"); // Nếu không tìm thấy chuyến bay, trả về trang lỗi
            }
            else
            {
                // Vé khứ hồi
                if (Session["ChuyenBayDi"] == null)
                {
                    // Chưa chọn chuyến bay về => lưu chuyến bay đi và hiển thị danh sách chuyến bay về
                    Session["ChuyenBayDi"] = maChuyenBay;

                    var diemDi = Session["DiemDi"] as string;
                    var diemDen = Session["DiemDen"] as string;

                    var returnFlights = db.ChuyenBay
                        .Where(f => f.DiemDi == diemDen && f.DiemDen == diemDi)
                        .ToList();

                    var viewModel = new ChooseFlightViewModel
                    {
                        OutboundFlights = null, // Không cần danh sách chiều đi nữa
                        ReturnFlights = returnFlights,
                        IsReturnTrip = true
                    };

                    return View("ChooseFlight", viewModel);
                }
                else
                {
                    // Đã chọn chuyến bay về => tạo vé
                    Session["ChuyenBayVe"] = maChuyenBay;

                    // Tạo mã vé cho chuyến bay về
                    string maVeVe = GenerateRandomMaVe();
                    Session["MaVeVe"] = maVeVe;

                    var flight = db.ChuyenBay.FirstOrDefault(f => f.MaChuyenBay == maChuyenBay);
                    if (flight != null)
                    {
                        var veVe = new Ve
                        {
                            MaVe = maVeVe,
                            MaChuyenBay = maChuyenBay,
                            MaNV = null,
                            MaDatCho = null,
                            ThoiGianDatVe = DateTime.Now,
                            ThanhTien = flight.GiaChuyenBay,
                            DaThanhToan = false,
                            TrangThai = "Chờ xác nhận",
                            Hang = "VietnamAirline"
                        };

                        db.Ve.Add(veVe);
                        db.SaveChanges();
                        int sokhach = (int)Session["SoNguoi"];
                        // Chuyển sang trang InfoTraveler với cả hai vé
                        return RedirectToAction("InfoTraveler", new { maVe = Session["MaVeDi"], maVeVe = maVeVe, songuoi = sokhach });
                    }

                    return View("Error"); // Nếu không tìm thấy chuyến bay, trả về trang lỗi
                }
            }
        }


        // Hàm tạo mã vé ngẫu nhiên
        private string GenerateRandomMaVe()
        {
            return Guid.NewGuid().ToString("N").Substring(0, 10);  // Mã vé dài 10 ký tự
        }


    }
}