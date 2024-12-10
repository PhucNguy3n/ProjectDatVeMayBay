using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebDatVeMayBay.Models;

namespace WebDatVeMayBay.Areas.Admin.Controllers
{
    public class HomeController : Controller
    {
        private readonly DBContext db = new DBContext();

        // GET: Admin/Home
        public ActionResult Index()
        {
            ViewBag.NumberOfEmployees = db.NhanVien.Count();
            ViewBag.NumberOfFlights = db.ChuyenBay.Count();
            ViewBag.NumberOfPlanes = db.MayBay.Count();
            ViewBag.NumberOfCustomers = db.KhachHang.Count();

            return View();
        }
    }
}