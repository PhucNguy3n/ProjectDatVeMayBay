using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebDatVeMayBay.Models;
using PagedList;
using System.Net;
using System.Data.Entity;

namespace WebDatVeMayBay.Areas.Admin.Controllers
{
    public class MayBayController : Controller
    {
        private readonly DBContext db = new DBContext();

        // GET: Admin/MayBay
        public ActionResult Index(string searchTerm, string sortOrder, int? page)
        {
            // Lấy danh sách máy bay
            var mayBays = db.MayBay.AsQueryable();

            // Tìm kiếm
            if (!string.IsNullOrEmpty(searchTerm))
            {
                mayBays = mayBays.Where(cb => cb.MaMayBay.Contains(searchTerm) ||
                                                     cb.TenMayBay.Contains(searchTerm) ||
                                                     cb.MaGhe.Contains(searchTerm));
            }

            // Sắp xếp
            ViewBag.CurrentSort = sortOrder;
            ViewBag.SortByMaMayBay = string.IsNullOrEmpty(sortOrder) ? "MaMayBay_desc" : "";
            ViewBag.SortByTenMayBay = sortOrder == "TenMayBay" ? "TenMayBay_desc" : "TenMayBay";

            switch (sortOrder)
            {
                case "MaMayBay_desc":
                    mayBays = mayBays.OrderByDescending(cb => cb.MaMayBay);
                    break;
                case "TenMayBay":
                    mayBays = mayBays.OrderBy(cb => cb.TenMayBay);
                    break;
                case "TenMayBay_desc":
                    mayBays = mayBays.OrderByDescending(cb => cb.TenMayBay);
                    break;
                default:
                    mayBays = mayBays.OrderBy(cb => cb.MaMayBay);
                    break;
            }

            // Phân trang
            int pageSize = 3; // Số item mỗi trang
            int pageNumber = page ?? 1; // Trang hiện tại
            return View(mayBays.ToPagedList(pageNumber, pageSize));
        }

        // GET: Admin/MayBay/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/MayBay/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(MayBay mayBay)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.MayBay.Add(mayBay);
                    db.SaveChanges();
                    TempData["Success"] = "Thêm mới máy bay thành công!";
                    return RedirectToAction("Index"); // Điều hướng về danh sách
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Lỗi: " + ex.Message);
                }
            }
            return View(mayBay); // Trả lại View cùng dữ liệu để sửa lỗi
        }

        // GET: MayBay/Edit/{id}
        public ActionResult Edit(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var mayBay = db.MayBay.Find(id);
            if (mayBay == null)
            {
                return HttpNotFound("Không tìm thấy máy bay với mã này.");
            }

            return View(mayBay);
        }

        // POST: MayBay/Edit/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(MayBay mayBay)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.Entry(mayBay).State = EntityState.Modified;
                    db.SaveChanges();
                    TempData["Success"] = "Cập nhật thông tin máy bay thành công!";
                    return RedirectToAction("Index"); // Điều hướng về danh sách
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Lỗi: " + ex.Message);
                }
            }

            return View(mayBay); // Trả lại View cùng dữ liệu để sửa lỗi
        }

        // GET: MayBay/Details/{id}
        public ActionResult Details(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var mayBay = db.MayBay.Find(id);
            if (mayBay == null)
            {
                return HttpNotFound("Không tìm thấy thông tin máy bay với mã này.");
            }

            return View(mayBay);
        }

        // GET: MayBay/Delete/{id}
        public ActionResult Delete(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var mayBay = db.MayBay.Find(id);
            if (mayBay == null)
            {
                return HttpNotFound("Không tìm thấy thông tin máy bay với mã này.");
            }

            return View(mayBay);
        }

        // POST: MayBay/Delete/{id}
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            var mayBay = db.MayBay.Find(id);
            if (mayBay != null)
            {
                try
                {
                    db.MayBay.Remove(mayBay);
                    db.SaveChanges();
                    TempData["Success"] = "Xóa máy bay thành công!";
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