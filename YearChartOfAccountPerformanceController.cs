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
    public class YearChartOfAccountPerformanceController : Controller
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
        public JsonResult LoadChartOfAccounts()
        {
            var chartOfAccount = db.ChartOfAccounts
                .Select(m => new { value = m.RevenueCode, text = m.ChartOfAccountName }).ToList();
            return new JsonResult { Data = chartOfAccount, JsonRequestBehavior = JsonRequestBehavior.AllowGet };

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
        public JsonResult GetChartOfAccountList(string RevenueCategoryId)
        {
            db.Configuration.ProxyCreationEnabled = false;
            List<ChartOfAccount> StateList = db.ChartOfAccounts.Where(x => x.RevenueCategoryId == RevenueCategoryId).ToList();
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
            IPagedList<YearChartOfAccountPerformance> PerformanceListList = db.YearChartOfAccountPerformances.OrderBy(c => c.PerformancenedDate).ToList().ToPagedList(page ?? 1, 2);
            return View(PerformanceListList);
        }

        [HttpPost]
        public ActionResult SaveOrder(string ParentId, string ChildId, string FisicalYearId,  YearChartOfAccountDetailPerformance[] YearChartOfAccountDetailPerformance)
        {
            string result = "እባክዎ የተሟል ምዝገባ ያድርጉ";

            if (ParentId != null && ChildId != null && FisicalYearId != null && YearChartOfAccountDetailPerformance != null)
            {
                var yearChartOfAccountPerformanceId = Guid.NewGuid();
                YearChartOfAccountPerformance model = new YearChartOfAccountPerformance
                {
                    YearChartOfAccountPerformanceId = yearChartOfAccountPerformanceId,
                    ParentId = ParentId,
                    ChildId = ChildId,
                    FisicalYearId = FisicalYearId,
                    PerformancenedDate = DateTime.Now
                };
                db.YearChartOfAccountPerformances.Add(model);
                foreach (var item in YearChartOfAccountDetailPerformance)
                {
                    var YearChartOfAccountDetailPerformanceId = Guid.NewGuid();
                    YearChartOfAccountDetailPerformance D = new YearChartOfAccountDetailPerformance
                    {
                        YearChartOfAccountDetailPerformanceId = YearChartOfAccountDetailPerformanceId,
                        YearChartOfAccountPerformanceId = yearChartOfAccountPerformanceId,
                        FisicalYearId = FisicalYearId,
                        YearChartOfAccountPerformanceAmount = item.YearChartOfAccountPerformanceAmount,
                        RevenueCategoryId = item.RevenueCategoryId,
                        RevenueCode = item.RevenueCode
                    };
                    db.YearChartOfAccountDetailPerformances.Add(D);
                }
                db.SaveChanges();
                result = "መረጃው በትክክል ተመዝግቧል";
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

    }
}