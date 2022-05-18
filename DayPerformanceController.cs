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
    public class DayPerformanceController : Controller
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
        public JsonResult LoadMonths()
        {
            var months = db.Months
                .Select(m => new { value = m.MonthId, text = m.MonthName }).ToList();
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
            IPagedList<DayPerformance> PerformanceListList = db.DayPerformances.OrderBy(c => c.ParentId).ToList().ToPagedList(page ?? 1, 2);
            return View(PerformanceListList);
        }

        [HttpPost]
        public ActionResult SaveOrder(string ParentId, string ChildId, string FisicalYearId, string MonthId, DayPerformanceDetail[] detail)
        {
            string result = "Error: Order is not complete";
            if (ParentId != null && ChildId != null && FisicalYearId != null && MonthId != null && detail != null)
            {
                var PerformanceId = Guid.NewGuid();
                DayPerformance model = new DayPerformance
                {
                    DayPerformanceId = PerformanceId,
                    ParentId = ParentId,
                    ChildId = ChildId,
                    MonthId = MonthId,
                    FisicalYearId = FisicalYearId,
                    PerformancenedDate = DateTime.Now
                };
                db.DayPerformances.Add(model);

                foreach (var item in detail)
                {
                    var detailId = Guid.NewGuid();
                    DayPerformanceDetail D = new DayPerformanceDetail
                    {
                        DayPerformanceDetailId = detailId,
                        DayPerformanceId = PerformanceId,
                        FisicalYearId = FisicalYearId,
                        MonthId = MonthId,
                        DayPerformanceAmount = item.DayPerformanceAmount,
                        PerformancenedDate = item.PerformancenedDate                        
                    };
                    db.DayPerformanceDetails.Add(D);
                }
                db.SaveChanges();
                result = "መረጃው በትክክል ተመዝግቧል";
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        

    }
}