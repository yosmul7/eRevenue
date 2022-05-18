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
    public class MonthPlanController : Controller
    {
        //Get: Month Plan
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
            IPagedList<MonthPlan> PlanListList = db.MonthPlans.OrderBy(c => c.ParentId).ToList().ToPagedList(page ?? 1, 1);
            return View(PlanListList);
        }

        [HttpPost]
        public ActionResult SaveOrder(string ParentId, string ChildId, string FisicalYearId, string MonthId, MonthDetail[] detail)
        {
            string result = "Error: Order is not complete";      
                if (ParentId != null && ChildId != null && FisicalYearId != null && MonthId != null && detail != null)
            {
                var planId = Guid.NewGuid();
                MonthPlan model = new MonthPlan
                {
                    MonthPlanId = planId,
                    ParentId = ParentId,
                    ChildId = ChildId,
                    FisicalYearId = FisicalYearId,
                    PlannedDate = DateTime.Now
                };
                db.MonthPlans.Add(model);

                foreach (var item in detail)
                {
                    var detailId = Guid.NewGuid();
                    MonthDetail D = new MonthDetail
                    {
                        MonthDetailId = detailId,
                        MonthPlanId = planId,
                        MonthId = item.MonthId,
                        FisicalYearId = FisicalYearId,
                        MonthPlanAmount = item.MonthPlanAmount
                    };
                    db.MonthDetails.Add(D);
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

        // Delete MonthPlan

        [HttpPost]
        public ActionResult DeleteMonthPlan(string planId)
        {
            string result = "";
            var planGuid = Guid.Parse(planId);
            var plan = db.MonthPlans.FirstOrDefault(c => c.MonthPlanId == planGuid);
            if (plan != null)
            {
                var MonthDetail = db.MonthDetails.Where(o => o.MonthPlanId == planGuid).ToList();
                foreach (MonthDetail o in MonthDetail)
                {
                    db.MonthDetails.Remove(o);
                }
                db.MonthPlans.Remove(plan);
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