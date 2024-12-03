using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebDatVeMayBay.Models;
using PagedList;

namespace WebDatVeMayBay.Areas.Admin.Controllers
{
    public class ChuyenBayController : Controller
    {
        private readonly DBContext db = new DBContext();

        // GET: Admin/ChuyenBay/Index
        public ActionResult Index(string searchTerm, string sortOrder, int? page)
        {
            // Lấy danh sách chuyến bay
            var chuyenBays = db.ChuyenBay.AsQueryable();

            // Tìm kiếm
            if (!string.IsNullOrEmpty(searchTerm))
            {
                chuyenBays = chuyenBays.Where(cb => cb.MaChuyenBay.Contains(searchTerm) ||
                                                     cb.SanBayDi.Contains(searchTerm) ||
                                                     cb.SanBayDen.Contains(searchTerm));
            }

            // Sắp xếp
            ViewBag.CurrentSort = sortOrder;
            ViewBag.SortByMaChuyenBay = string.IsNullOrEmpty(sortOrder) ? "MaChuyenBay_desc" : "";
            ViewBag.SortByThoiGianKhoiHanh = sortOrder == "ThoiGianKhoiHanh" ? "ThoiGianKhoiHanh_desc" : "ThoiGianKhoiHanh";

            switch (sortOrder)
            {
                case "MaChuyenBay_desc":
                    chuyenBays = chuyenBays.OrderByDescending(cb => cb.MaChuyenBay);
                    break;
                case "ThoiGianKhoiHanh":
                    chuyenBays = chuyenBays.OrderBy(cb => cb.ThoiGianKhoiHanh);
                    break;
                case "ThoiGianKhoiHanh_desc":
                    chuyenBays = chuyenBays.OrderByDescending(cb => cb.ThoiGianKhoiHanh);
                    break;
                default:
                    chuyenBays = chuyenBays.OrderBy(cb => cb.MaChuyenBay);
                    break;
            }

            // Phân trang
            int pageSize = 5; // Số item mỗi trang
            int pageNumber = page ?? 1; // Trang hiện tại
            return View(chuyenBays.ToPagedList(pageNumber, pageSize));
        }

        // GET: Admin/ChuyenBay/Create
        public ActionResult Create()
        {
            ViewBag.MaMayBay = new SelectList(db.MayBay, "MaMayBay", "TenMayBay");
            return View();
        }

        // POST: Admin/ChuyenBay/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaChuyenBay,MaMayBay,SanBayDi,SanBayDen,DiemDi,DiemDen,ThoiGianKhoiHanh,ThoiGianDen,ThoiGianBayDuTinh,GiaChuyenBay")] ChuyenBay chuyenBay)
        {
            // Kiểm tra tính hợp lệ của dữ liệu
            if (ModelState.IsValid)
            {
                // Kiểm tra logic Thời gian khởi hành và Thời gian đến
                if (chuyenBay.ThoiGianKhoiHanh >= chuyenBay.ThoiGianDen)
                {
                    ModelState.AddModelError("ThoiGianKhoiHanh", "Thời gian khởi hành phải nhỏ hơn thời gian đến.");
                    ViewBag.MaMayBay = new SelectList(db.MayBay, "MaMayBay", "TenMayBay", chuyenBay.MaMayBay);
                    return View(chuyenBay);
                }

                // Kiểm tra mã chuyến bay đã tồn tại chưa
                if (db.ChuyenBay.Any(cb => cb.MaChuyenBay == chuyenBay.MaChuyenBay))
                {
                    ModelState.AddModelError("MaChuyenBay", "Mã chuyến bay đã tồn tại.");
                    ViewBag.MaMayBay = new SelectList(db.MayBay, "MaMayBay", "TenMayBay", chuyenBay.MaMayBay);
                    return View(chuyenBay);
                }

                db.ChuyenBay.Add(chuyenBay);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            // Hiển thị lại form nếu có lỗi
            ViewBag.MaMayBay = new SelectList(db.MayBay, "MaMayBay", "TenMayBay", chuyenBay.MaMayBay);
            return View(chuyenBay);
        }

        // GET: ChuyenBay/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            ChuyenBay chuyenBay = db.ChuyenBay.Find(id);
            if (chuyenBay == null)
            {
                return HttpNotFound();
            }

            // Load dropdown list for MaMayBay
            ViewBag.MaMayBay = new SelectList(db.MayBay, "MaMayBay", "TenMayBay", chuyenBay.MaMayBay);

            return View(chuyenBay);
        }

        // POST: ChuyenBay/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaChuyenBay,MaMayBay,SanBayDen,SanBayDi,DiemDi,DiemDen,ThoiGianKhoiHanh,ThoiGianDen,ThoiGianBayDuTinh,GiaChuyenBay")] ChuyenBay chuyenBay)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.Entry(chuyenBay).State = EntityState.Modified;
                    db.SaveChanges();
                    TempData["Success"] = "Thông tin chuyến bay đã được cập nhật thành công.";
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", $"Có lỗi xảy ra: {ex.Message}");
                }
            }

            // Reload dropdown list for MaMayBay in case of validation error
            ViewBag.MaMayBay = new SelectList(db.MayBay, "MaMayBay", "TenMayBay", chuyenBay.MaMayBay);

            return View(chuyenBay);
        }

        // GET: ChuyenBay/Details/{id}
        public ActionResult Details(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            // Tìm chuyến bay theo MaChuyenBay
            var chuyenBay = db.ChuyenBay.Include("MayBay").FirstOrDefault(cb => cb.MaChuyenBay == id);

            if (chuyenBay == null)
            {
                return HttpNotFound();
            }

            return View(chuyenBay);
        }

        // GET: ChuyenBay/Delete/{id}
        public ActionResult Delete(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            // Tìm chuyến bay theo MaChuyenBay
            var chuyenBay = db.ChuyenBay.FirstOrDefault(cb => cb.MaChuyenBay == id);

            if (chuyenBay == null)
            {
                return HttpNotFound();
            }

            return View(chuyenBay);
        }

        // POST: ChuyenBay/Delete/{id}
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            var chuyenBay = db.ChuyenBay.FirstOrDefault(cb => cb.MaChuyenBay == id);

            if (chuyenBay == null)
            {
                return HttpNotFound();
            }

            // Xóa chuyến bay
            db.ChuyenBay.Remove(chuyenBay);
            db.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}