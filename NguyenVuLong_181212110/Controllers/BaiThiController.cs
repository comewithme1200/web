using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NguyenVuLong_181212110.Models.Entities;
namespace NguyenVuLong_181212110.Controllers
{
    public class BaiThiController : Controller
    {
        NguyenVuLong_Context db = new NguyenVuLong_Context();
        // GET: BaiThi
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult RenderNav()
        {
            List<LoaiHang> listLoaiHang = db.LoaiHangs.ToList();
            return PartialView("NguyenVuLong_Header",listLoaiHang);
        }
        public ActionResult RenderProduct()
        {
            List<HangHoa> listHangHoa = db.HangHoas.Where(h => h.Gia >= 100).ToList();
            return PartialView("NguyenVuLong_Main", listHangHoa);
        }
        public ActionResult LoadProductByCatID(int CatID)
        {
            List<HangHoa> listHangHoa = db.HangHoas.Where(h => h.MaLoai == CatID).ToList();
            return PartialView("NguyenVuLong_Main", listHangHoa);
        }
        public ActionResult LoadProductByName(String searchString)     
        {
            List<HangHoa> listHangHoa = db.HangHoas.ToList().FindAll(x => x.TenHang.ToLower().Contains(searchString.ToLower()));
            return PartialView("NguyenVuLong_Main", listHangHoa);
        } 
        public ActionResult Create()
        {
            ViewBag.MaLoai = new SelectList(db.LoaiHangs, "MaLoai", "TenLoai");
            return View();
        }
        [HttpPost]
        public ActionResult Create(HangHoa hanghoa)
        {
            db.HangHoas.Add(hanghoa);
            db.SaveChanges();
            return Redirect("Index");
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var hh = db.HangHoas.ToList().Find(x => x.MaHang ==id);
            ViewBag.MaLoai = new SelectList(db.LoaiHangs, "MaLoai", "TenLoai");
            return View(hh);
        }
        [HttpPost]
        public ActionResult Edit(HangHoa hanghoa)
        {
            //db.HangHoas.Add(hanghoa);
            var hh = db.HangHoas.ToList().Find(x => x.MaHang == hanghoa.MaHang);
            hh.TenHang = hanghoa.TenHang;
            hh.MaLoai = hanghoa.MaLoai;
            hh.Gia = hanghoa.Gia;
            hh.Anh = hanghoa.Anh;
            db.SaveChanges();
            return Redirect("Index");
        }

        [HttpGet]
        public ActionResult Delete(int id) {

            var hh = db.HangHoas.ToList().Find(x => x.MaHang == id);
            ViewBag.MaLoai = new SelectList(db.LoaiHangs, "MaLoai", "TenLoai");
            return View(hh);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Deletee(int id)
        {
            var hh = db.HangHoas.ToList().Find(x => x.MaHang == id);
            db.HangHoas.Remove(hh);
            db.SaveChanges();
            ViewBag.MaLoai = new SelectList(db.LoaiHangs, "MaLoai", "TenLoai");
            return Redirect("Index");
        }

    }
}