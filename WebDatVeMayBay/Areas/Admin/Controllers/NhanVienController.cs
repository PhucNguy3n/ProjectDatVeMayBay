using PagedList;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebDatVeMayBay.Models;

namespace WebDatVeMayBay.Areas.Admin.Controllers
{
    public class NhanVienController : Controller
    {
        private readonly DBContext db = new DBContext();

        // GET: Admin/NhanVien
        public ActionResult Index(string searchTerm, string sortOrder, int? page)
        {
            // Lấy danh sách nhân viên
            var nhanViens = db.NhanVien.AsQueryable();

            // Tìm kiếm
            if (!string.IsNullOrEmpty(searchTerm))
            {
                nhanViens = nhanViens.Where(cb => cb.MaNV.Contains(searchTerm) ||
                                                     cb.TenKH.Contains(searchTerm) ||
                                                     cb.SDT.Contains(searchTerm));
            }

            // Sắp xếp
            ViewBag.CurrentSort = sortOrder;
            ViewBag.SortByMaNhanVien = string.IsNullOrEmpty(sortOrder) ? "MaNhanVien_desc" : "";
            ViewBag.SortByTenNhanVien = sortOrder == "TenNhanVien" ? "TenNhanVien_desc" : "TenNhanVien";

            switch (sortOrder)
            {
                case "MaMayBay_descMaNhanVien_desc":
                    nhanViens = nhanViens.OrderByDescending(cb => cb.MaNV);
                    break;
                case "TenNhanVien":
                    nhanViens = nhanViens.OrderBy(cb => cb.TenKH);
                    break;
                case "TenNhanVien_desc":
                    nhanViens = nhanViens.OrderByDescending(cb => cb.TenKH);
                    break;
                default:
                    nhanViens = nhanViens.OrderBy(cb => cb.MaNV);
                    break;
            }

            // Phân trang
            int pageSize = 3; // Số item mỗi trang
            int pageNumber = page ?? 1; // Trang hiện tại
            return View(nhanViens.ToPagedList(pageNumber, pageSize));
        }

        // GET: Admin/NhanVien/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/NhanVien/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaNV,TenKH,GioiTinh,NgaySinh,DiaChi,SDT,TinhTrang")] NhanVien nhanVien)
        {
            if (ModelState.IsValid)
            {
                // Kiểm tra xem mã nhân viên đã tồn tại hay chưa
                var existingNV = db.NhanVien.Find(nhanVien.MaNV);
                if (existingNV != null)
                {
                    ModelState.AddModelError("MaNV", "Mã nhân viên đã tồn tại.");
                    return View(nhanVien);
                }

                // Thêm nhân viên mới vào cơ sở dữ liệu
                db.NhanVien.Add(nhanVien);
                db.SaveChanges();
                TempData["Success"] = "Tạo mới nhân viên thành công!";
                return RedirectToAction("Index");
            }

            // Nếu dữ liệu không hợp lệ, trả về view
            return View(nhanVien);
        }

        public ActionResult Edit(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var nhanVien = db.NhanVien.Find(id);

            if (nhanVien == null)
            {
                return HttpNotFound("Không tìm thấy nhân viên với mã: " + id);
            }

            return View(nhanVien);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(NhanVien nhanVien)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.Entry(nhanVien).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Lỗi: " + ex.Message);
                }
            }

            return View(nhanVien);
        }

        public ActionResult Details(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var nhanVien = db.NhanVien.Find(id);
            if (nhanVien == null)
            {
                return HttpNotFound("Không tìm thấy thông tin nhân viên với mã này.");
            }

            return View(nhanVien);
        }

        public ActionResult Delete(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var nhanVien = db.NhanVien.Find(id);

            if (nhanVien == null)
            {
                return HttpNotFound("Không tìm thấy nhân viên với mã: " + id);
            }

            return View(nhanVien);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            var nhanVien = db.NhanVien.Find(id);

            if (nhanVien != null)
            {
                try
                {
                    db.NhanVien.Remove(nhanVien);
                    db.SaveChanges();
                    TempData["Success"] = "Xóa nhân viên thành công!";
                }
                catch (Exception ex)
                {
                    TempData["Error"] = "Lỗi: " + ex.Message;
                }
            }

            return RedirectToAction("Index");
        }
    }
}