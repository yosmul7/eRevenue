using eRevenuesNew.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using PagedList.Mvc;
using eRevenuesNew.ViewModels;
using System.IO;

namespace eRevenuesNew.Controllers
{
    public class LetterController : Controller
    {
        //Get: Year Plan
        readonly eRevenuesDbEntities db = new eRevenuesDbEntities();
        [HttpGet]
        public JsonResult LoadFisicalYears()
        {
            var fisicalYears = db.FisicalYears
                .Select(m => new { value = m.FisicalYearId, text = m.FisicalYearName }).ToList();
            return new JsonResult { Data = fisicalYears, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        public JsonResult LoadParents()
        {
            var parents = db.Parents
                .Select(m => new { value = m.ParentId, text = m.ParentName }).ToList();
            return new JsonResult { Data = parents, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        public JsonResult GetChildList(string ParentId)
        {
            db.Configuration.ProxyCreationEnabled = false;
            List<Child> StateList = db.Children.Where(x => x.ParentId == ParentId).ToList();
            return Json(StateList, JsonRequestBehavior.AllowGet);
        }
        public JsonResult LoadChildren()
        {
            var children = db.Children
                .Select(m => new { value = m.ChildId, text = m.ChildName }).ToList();
            return new JsonResult { Data = children, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        public ActionResult AddNewLetter()
        {
            return View(db.Letters.ToList());
        }
        [HttpPost]
        public ActionResult SaveData(Letter item)
        {
            if (item.ParentId != null && item.LetterNumber != null && item.ChildId != null && item.FisicalYearId != null && item.UploadImage != null)
            {
                string fileName = Path.GetFileNameWithoutExtension(item.UploadImage.FileName);
                string extension = Path.GetExtension(item.UploadImage.FileName);
                fileName = fileName + DateTime.Now.ToString("yymmssff") + extension;
                item.PicUrl = fileName;
                item.UploadImage.SaveAs(Path.Combine(Server.MapPath("~/AppFile/Images"), fileName));
                db.Letters.Add(item);
                db.SaveChanges();
            }
            var result = "Successfully Added";
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}