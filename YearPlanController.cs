using eRevenuesNew.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using PagedList.Mvc;
using eRevenuesNew.ViewModels;

namespace eRevenuesNew.Controllers
{
    public class YearPlanController : Controller
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
        public ActionResult Index(int? page)
        {
            IPagedList<YearPlan> PlanListList = db.YearPlans.OrderBy(c => c.ParentId).ToList().ToPagedList(page ?? 1, 3);
            return View(PlanListList);
        }

        [HttpPost]
        public ActionResult SaveOrder(string ParentId, string FisicalYearId, string ChildId,  YearDetail[] detail)
        {
            string result = "Error: Order is not complete";      
                if (ParentId != null && ChildId != null && FisicalYearId != null && detail != null)
            {
                var planId = Guid.NewGuid();
                YearPlan model = new YearPlan
                {
                    PlanId = planId,
                    ParentId = ParentId,
                    ChildId = ChildId,
                    FisicalYearId = FisicalYearId,
                    PlannedDate = DateTime.Now
                };
                db.YearPlans.Add(model);

                foreach (var item in detail)
                {
                    var detailId = Guid.NewGuid();
                    YearDetail D = new YearDetail
                    {
                        DetailId = detailId,
                        PlanId = planId,
                        FisicalYearId = FisicalYearId,
                        YearPlanAmount = item.YearPlanAmount
                    };
                    db.YearDetails.Add(D);
                }
                db.SaveChanges();
                result = "መረጃው በትክክል ተመዝግቧል";
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }
       
    }
}