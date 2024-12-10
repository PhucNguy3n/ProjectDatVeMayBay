using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebDatVeMayBay.Models;
using PagedList;

namespace WebDatVeMayBay.Areas.Admin.Controllers
{
    public class KhachHangController : Controller
    {
        private readonly DBContext db = new DBContext();

        // GET: Admin/KhachHang
        public ActionResult Index(string searchTerm, string sortOrder, int? page)
        {
            // Lấy danh sách khách hàng
            var khachHangs = db.KhachHang.AsQueryable();

            // Tìm kiếm
            if (!string.IsNullOrEmpty(searchTerm))
            {
                khachHangs = khachHangs.Where(cb => cb.EmailKH.Contains(searchTerm) ||
                                                     cb.TenKH.Contains(searchTerm) ||
                                                     cb.SDT.Contains(searchTerm) ||
                                                     cb.CCCD.Contains(searchTerm));
            }

            // Sắp xếp
            ViewBag.CurrentSort = sortOrder;
            ViewBag.SortByTenKH = string.IsNullOrEmpty(sortOrder) ? "TenKH_desc" : "";
            ViewBag.SortByNgaySinh = sortOrder == "NgaySinh" ? "NgaySinh_desc" : "NgaySinh";

            switch (sortOrder)
            {
                case "TenKH_desc":
                    khachHangs = khachHangs.OrderByDescending(cb => cb.TenKH);
                    break;
                case "NgaySinh":
                    khachHangs = khachHangs.OrderBy(cb => cb.NgaySinh);
                    break;
                case "NgaySinh_desc":
                    khachHangs = khachHangs.OrderByDescending(cb => cb.NgaySinh);
                    break;
                default:
                    khachHangs = khachHangs.OrderBy(cb => cb.TenKH);
                    break;
            }

            // Phân trang
            int pageSize = 10; // Số item mỗi trang
            int pageNumber = page ?? 1; // Trang hiện tại
            return View(khachHangs.ToPagedList(pageNumber, pageSize));
        }

        // GET: Admin/KhachHang/LichSuDatVe
        public ActionResult LichSuDatVe(string email, DateTime? fromDate, DateTime? toDate, int page = 1, int pageSize = 5)
        {
            if (string.IsNullOrEmpty(email))
            {
                return HttpNotFound("Email không hợp lệ.");
            }

            var khachHang = db.KhachHang
                .Include("DatChos.Ves.ChuyenBay")
                .Include("DatChos.Ves.ChiTietVes")
                .FirstOrDefault(kh => kh.EmailKH == email);

            if (khachHang == null)
            {
                return HttpNotFound("Khách hàng không tồn tại.");
            }

            // Lọc vé theo ngày
            var lichSuDatVe = khachHang.DatChos.SelectMany(dc => dc.Ves)
                .Where(v => (!fromDate.HasValue || v.ThoiGianDatVe >= fromDate) &&
                            (!toDate.HasValue || v.ThoiGianDatVe <= toDate))
                .OrderByDescending(v => v.ThoiGianDatVe);

            // Phân trang
            var lichSuDatVePaged = lichSuDatVe
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            ViewBag.CurrentPage = page;
            ViewBag.PageSize = pageSize;
            ViewBag.TotalRecords = lichSuDatVe.Count();
            ViewBag.FromDate = fromDate;
            ViewBag.ToDate = toDate;

            return View("LichSuDatVe", khachHang);
        }
    }
}