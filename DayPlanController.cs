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
    public class DayPlanController : Controller
    {
        //Get: Day Plan
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
            IPagedList<DayPlan> PlanListList = db.DayPlans.OrderBy(c => c.ParentId).ToList().ToPagedList(page ?? 1, 2);
            return View(PlanListList);
        }

        [HttpPost]
        public ActionResult SaveOrder(string ParentId, string ChildId, string FisicalYearId, string MonthId , DayDetail[] detail)
        {
            string result = "Error: Order is not complete";
            if (ParentId != null && ChildId != null && FisicalYearId != null && MonthId !=null && detail != null)
            {
                var planId = Guid.NewGuid();
                DayPlan model = new DayPlan
                {
                    DayPlanId = planId,
                    ParentId = ParentId,
                    ChildId = ChildId,
                    MonthId = MonthId,
                    FisicalYearId = FisicalYearId,
                    PlannedDate = DateTime.Now
                };
                db.DayPlans.Add(model);

                foreach (var item in detail)
                {
                    var detailId = Guid.NewGuid();
                    DayDetail D = new DayDetail
                    {
                        DayDetailId = detailId,
                        DayPlanId = planId,
                        FisicalYearId = FisicalYearId,
                        MonthId = MonthId,
                        DayPlanAmount = item.DayPlanAmount,
                        PlannedDate = item.PlannedDate
                    };
                    db.DayDetails.Add(D);
                }
                db.SaveChanges();
                result = "መረጃው በትክክል ተመዝግቧል";
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        // Edit Day Details


        // Delete DayDetail
        [HttpPost]
        public ActionResult DeleteDayDetail(string detailId)
        {
            var detailGuid = Guid.Parse(detailId);
            var detail = db.DayDetails.Find(detailGuid);
            db.DayDetails.Remove(detail);
            db.SaveChanges();
            return Json("መረጃው ተሰርዟል.!!", JsonRequestBehavior.AllowGet);
        }

        // Delete DayPlan

        [HttpPost]
        public ActionResult DeleteDayPlan(string planId)
        {
            string result = "";
            var planGuid = Guid.Parse(planId);
            var plan = db.DayPlans.FirstOrDefault(c => c.DayPlanId == planGuid);
            if (plan != null)
            {
                var DayDetail = db.DayDetails.Where(o => o.DayPlanId == planGuid).ToList();
                foreach (DayDetail o in DayDetail)
                {
                    db.DayDetails.Remove(o);
                }
                db.DayPlans.Remove(plan);
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