using eRevenuesNew.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using PagedList.Mvc;

namespace eRevenuesNew.Controllers
{
    public class YearCategoryPerformanceController : Controller
    {
        //Get: Year Performance
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
        public JsonResult LoadRevenueCategories()
        {
            var revenueCategories = db.RevenueCategories
                .Select(m => new { value = m.RevenueCategoryId, text = m.RevenueCategoryName }).ToList();
            return new JsonResult { Data = revenueCategories, JsonRequestBehavior = JsonRequestBehavior.AllowGet };

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
        public ActionResult Index(int? page)
        {
            IPagedList<YearCategoryPerformance> PerformanceListList = db.YearCategoryPerformances.OrderBy(c=> c.PerformancenedDate).ToList().ToPagedList(page ?? 1, 2);
            return View(PerformanceListList);
        }

        [HttpPost]
        public ActionResult SaveOrder(string ParentId, string ChildId, string FisicalYearId,  YearCategoryDetailPerformance[] YearCategoryDetailPerformance)
        {
            string result = "እባክዎ የተሟል ምዝገባ ያድርጉ";

            if (ParentId != null && ChildId != null && FisicalYearId != null && YearCategoryDetailPerformance != null)
            {
                var YearCategoryPerformanceId = Guid.NewGuid();
                YearCategoryPerformance model = new YearCategoryPerformance
                {
                    YearCategoryPerformanceId = YearCategoryPerformanceId,
                    ParentId = ParentId,
                    ChildId = ChildId,
                    FisicalYearId = FisicalYearId,
                    PerformancenedDate = DateTime.Now
                };
                db.YearCategoryPerformances.Add(model);
                foreach (var item in YearCategoryDetailPerformance)
                {
                    var YearCategoryDetailPerformanceId = Guid.NewGuid();
                    YearCategoryDetailPerformance D = new YearCategoryDetailPerformance
                    {
                        YearCategoryDetailPerformanceId = YearCategoryDetailPerformanceId,
                        YearCategoryPerformanceId = YearCategoryPerformanceId,
                        FisicalYearId = FisicalYearId,
                        RevenueCategoryId = item.RevenueCategoryId,
                        YearCategoryPerformanceAmount = item.YearCategoryPerformanceAmount
                    };
                    db.YearCategoryDetailPerformances.Add(D);
                }
                db.SaveChanges();
                result = "መረጃው በትክክል ተመዝግቧል";
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

    }
}