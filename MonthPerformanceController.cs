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
    public class MonthPerformanceController : Controller
    {
        //Get: Month Performance
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
        public ActionResult Index(int? page)
        {
            IPagedList<MonthPerformance> PerformanceListList = db.MonthPerformances.OrderBy(c => c.ParentId).ToList().ToPagedList(page ?? 1, 1);
            return View(PerformanceListList);
        }

        [HttpPost]
        public ActionResult SaveOrder(string ParentId, string ChildId, string FisicalYearId, string MonthId, MonthDetailPerformance[] MonthDetailPerformance)
        {
            string result = "Error: Order is not complete";      
                if (ParentId != null && ChildId != null && FisicalYearId != null && MonthId != null && MonthDetailPerformance != null)
            {
                var PerformanceId = Guid.NewGuid();
                MonthPerformance model = new MonthPerformance
                {
                    MonthPerformanceId = PerformanceId,
                    ParentId = ParentId,
                    ChildId = ChildId,
                    FisicalYearId = FisicalYearId,
                    PerformancenedDate = DateTime.Now
                };
                db.MonthPerformances.Add(model);

                foreach (var item in MonthDetailPerformance)
                {
                    var detailId = Guid.NewGuid();
                    MonthDetailPerformance D = new MonthDetailPerformance
                    {
                        MonthDetailPerformanceId = detailId,
                        MonthPerformanceId = PerformanceId,
                        MonthId = item.MonthId,
                        FisicalYearId = FisicalYearId,
                        MonthPerformanceAmount = item.MonthPerformanceAmount
                    };
                    db.MonthDetailPerformances.Add(D);
                }
                db.SaveChanges();
                result = "መረጃው በትክክል ተመዝግቧል";
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        
        // Edit Month Details


        // Delete MonthDetail
        [HttpPost]
        public ActionResult DeleteMonthDetail(string detailId)
        {
            var detailGuid = Guid.Parse(detailId);
            var detail = db.MonthDetails.Find(detailGuid);
            db.MonthDetails.Remove(detail);
            db.SaveChanges();
            return Json("መረጃው ተሰርዟል.!!", JsonRequestBehavior.AllowGet);
        }

        // Delete MonthPerformance

        [HttpPost]
        public ActionResult DeleteMonthPerformance(string PerformanceId)
        {
            string result = "";
            var PerformanceGuid = Guid.Parse(PerformanceId);
            var Performance = db.MonthPerformances.FirstOrDefault(c => c.MonthPerformanceId == PerformanceGuid);
            if (Performance != null)
            {
                var MonthDetailPerformance = db.MonthDetailPerformances.Where(o => o.MonthPerformanceId == PerformanceGuid).ToList();
                foreach (MonthDetailPerformance o in MonthDetailPerformance)
                {
                    db.MonthDetailPerformances.Remove(o);
                }
                db.MonthPerformances.Remove(Performance);
                db.SaveChanges();
                result = "መረጃው ተሰርዟል.!!";
            }
            else
            {
                result = "መረጃው አልተገኘም..!!";
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }

    }
}