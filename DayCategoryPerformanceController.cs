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
    public class DayCategoryPerformanceController : Controller
    {
        //Get: Day Performance
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
        public JsonResult LoadMonths()
        {
            var months = db.Months
                .Select(m => new { value = m.MonthId, text = m.MonthName}).ToList();
                return new JsonResult { Data = months, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        public JsonResult GetMonthList(string FisicalYearId)
        {
            db.Configuration.ProxyCreationEnabled = false;
            List<Month> StateList = db.Months.Where(x => x.FisicalYearId == FisicalYearId).ToList();
            return Json(StateList, JsonRequestBehavior.AllowGet);
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
            IPagedList<DayCategoryPerformance> DayCategoryPerformanceList = db. DayCategoryPerformances.OrderBy(c => c.PerformancenedDate).ToList().ToPagedList(page ?? 1, 2);
            return View(DayCategoryPerformanceList);
        }

        [HttpPost]
        public ActionResult SaveOrder(string ParentId, string ChildId, string FisicalYearId, string MonthId, DayCategoryPerformanceDetail[] DayCategoryPerformanceDetail)
        {
            string result = "እባክዎ የተሟል ምዝገባ ያድርጉ";

            if (ParentId != null && ChildId != null && FisicalYearId != null && MonthId != null && DayCategoryPerformanceDetail != null)
            {
                var DayCategoryPerformanceId = Guid.NewGuid();
                DayCategoryPerformance model = new DayCategoryPerformance
                {
                    DayCategoryPerformanceId = DayCategoryPerformanceId,
                    ParentId = ParentId,
                    MonthId = MonthId,
                    ChildId = ChildId,
                    FisicalYearId = FisicalYearId,
                    PerformancenedDate = DateTime.Now
                };
                db. DayCategoryPerformances.Add(model);
                foreach (var item in DayCategoryPerformanceDetail)
                {
                    var DayCategoryPerformanceDetailId = Guid.NewGuid();
                    DayCategoryPerformanceDetail D = new DayCategoryPerformanceDetail
                    {
                        DayCategoryPerformanceDetailId = DayCategoryPerformanceDetailId,
                        DayCategoryPerformanceId = DayCategoryPerformanceId,
                        FisicalYearId = FisicalYearId,     
                        MonthId = MonthId,
                        RevenueCategoryId = item.RevenueCategoryId,
                        PerformancedDate = item.PerformancedDate,
                        DayCategoryPerformanceAmount = item.DayCategoryPerformanceAmount
                    };
                    db.DayCategoryPerformanceDetails.Add(D);
                }
                db.SaveChanges();
                result = "መረጃው በትክክል ተመዝግቧል";
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

    }
}