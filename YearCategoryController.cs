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
    public class YearCategoryController : Controller
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
            IPagedList<YearCategoryPlan> PlanListList = db.YearCategoryPlans.OrderBy(c=> c.PlannedDate).ToList().ToPagedList(page ?? 1, 2);
            return View(PlanListList);
        }

        [HttpPost]
        public ActionResult SaveOrder(string ParentId, string ChildId, string FisicalYearId,  YearCategoryDetail[] yearCategoryDetail)
        {
            string result = "እባክዎ የተሟል ምዝገባ ያድርጉ";

            if (ParentId != null && ChildId != null && FisicalYearId != null && yearCategoryDetail != null)
            {
                var yearCategoryPlanId = Guid.NewGuid();
                YearCategoryPlan model = new YearCategoryPlan
                {
                    YearCategoryPlanId = yearCategoryPlanId,
                    ParentId = ParentId,
                    ChildId = ChildId,
                    FisicalYearId = FisicalYearId,
                    PlannedDate = DateTime.Now
                };
                db.YearCategoryPlans.Add(model);
                foreach (var item in yearCategoryDetail)
                {
                    var yearCategoryDetailId = Guid.NewGuid();
                    YearCategoryDetail D = new YearCategoryDetail
                    {
                        YearCategoryDetailId = yearCategoryDetailId,
                        YearCategoryPlanId = yearCategoryPlanId,
                        FisicalYearId = FisicalYearId,
                        RevenueCategoryId = item.RevenueCategoryId,
                        YearCategoryPlanAmount = item.YearCategoryPlanAmount
                    };
                    db.YearCategoryDetails.Add(D);
                }
                db.SaveChanges();
                result = "መረጃው በትክክል ተመዝግቧል";
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

    }
}