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
    public class Year7PlanController : Controller
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

        [HttpPost] // Add New And Edit 
        public ActionResult SaveOrder(string id, string ParentId, string FisicalYearId, string ChildId, YearDetail[] detail)
        {
            string result = "Error: Order is not complete";
            // New Entry
            if (string.IsNullOrEmpty(id))
            {
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

                        //
                    }
                    db.SaveChanges();
                }
            }
            // Edit YearDetails 
            else
            {
                var planGuid = Guid.Parse(id);
                var PlanInDb = db.YearPlans.FirstOrDefault(c => c.PlanId == planGuid);
                PlanInDb.FisicalYearId = FisicalYearId;
                PlanInDb.ChildId = ChildId;
                PlanInDb.ParentId = ParentId;

                foreach (var item in detail)
                {
                    var dbYearDetail = db.YearDetails.FirstOrDefault(det => det.DetailId == det.DetailId);
                    if (dbYearDetail != null)
                    {
                        dbYearDetail.FisicalYearId = item.FisicalYearId;
                        dbYearDetail.YearPlanAmount = item.YearPlanAmount;

                    }
                    else
                    {
                        YearDetail yearD = new YearDetail();
                        yearD.DetailId = Guid.NewGuid();
                        yearD.FisicalYearId = item.FisicalYearId;
                        yearD.YearPlanAmount = item.YearPlanAmount;
                        yearD.PlanId = planGuid;
                        db.YearDetails.Add(yearD);
                    }
                }
                db.SaveChanges();
                result = "Success! Order edit is complete..";
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetPlanDetails(string planId)
        {
            var planGuid = Guid.Parse(planId);
            var PlanDetails = db.YearPlans.Include("YearDetails").Single(c => c.PlanId == planGuid);

            vmYearPlan YearPlan = new vmYearPlan();
            YearPlan.PlanId = PlanDetails.PlanId;
            YearPlan.FisicalYearId = PlanDetails.FisicalYearId;
            YearPlan.ParentId = PlanDetails.ParentId;
            YearPlan.ChildId = PlanDetails.ChildId;

            List<vmYearDetail> orderslList = new List<vmYearDetail>();

            foreach (var o in PlanDetails.YearDetails)
            {
                vmYearDetail order = new vmYearDetail();
                order.DetailId = o.DetailId;
                order.FisicalYearId = o.FisicalYearId;
                order.YearPlanAmount = o.YearPlanAmount;
                orderslList.Add(order);
            }

            vmYearPlanAndYearDetails PlanAndYearDetails = new vmYearPlanAndYearDetails()
            {
                YearPlan = YearPlan,
                YearDetails = orderslList
            };

            return Json(PlanAndYearDetails, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult DeleteYearDetail(string yearDetailId)
        {
            var detailGuid = Guid.Parse(yearDetailId);
            var yearPlan = db.YearDetails.Find(detailGuid);
            db.YearDetails.Remove(yearPlan);
            db.SaveChanges();
            return Json("Delete is completed..!!", JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult DeleteYearPlan(string planId)
        {
            string result = "";
            var planGuid = Guid.Parse(planId);
            var yearPlan = db.YearPlans.FirstOrDefault(c => c.PlanId == planGuid);
            if (yearPlan != null)
            {
                var orders = db.YearDetails.Where(o => o.PlanId == planGuid).ToList();
                foreach (YearDetail o in orders)
                {
                    db.YearDetails.Remove(o);
                }
                db.YearPlans.Remove(yearPlan);
                db.SaveChanges();
                result = "Customer is deleted successfully";
            }
            else
            {
                result = "Customer not found..!!";
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}