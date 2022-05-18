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
    public class YearPerformanceController : Controller
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
            IPagedList<YearPerformance> PerformanceListList = db.YearPerformances.OrderBy(c => c.ParentId).ToList().ToPagedList(page ?? 1, 3);
            return View(PerformanceListList);
        }

        [HttpPost]
        public ActionResult SaveOrder(string ParentId, string FisicalYearId, string ChildId,  YearDetailPerformance[] detail)
        {
            string result = "Error: Order is not complete";      
                if (ParentId != null && ChildId != null && FisicalYearId != null && detail != null)
            {
                var PerformanceId = Guid.NewGuid();
                YearPerformance model = new YearPerformance
                {
                    PerformanceId = PerformanceId,
                    ParentId = ParentId,
                    ChildId = ChildId,
                    FisicalYearId = FisicalYearId,
                    PerformancenedDate = DateTime.Now
                };
                db.YearPerformances.Add(model);

                foreach (var item in detail)
                {
                    var detailId = Guid.NewGuid();
                    YearDetailPerformance D = new YearDetailPerformance
                    {
                        DetailId = detailId,
                        PerformanceId = PerformanceId,
                        FisicalYearId = FisicalYearId,
                        YearPerformanceAmount = item.YearPerformanceAmount
                    };
                    db.YearDetailPerformances.Add(D);
                }
                db.SaveChanges();
                result = "መረጃው በትክክል ተመዝግቧል";
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }        


    }
}