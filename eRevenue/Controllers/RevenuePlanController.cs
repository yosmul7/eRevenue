using eRevenue.Data;
using eRevenue.Models;
using eRevenue.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eRevenue.Controllers
{
    public class RevenuePlanController : Controller
    {
        private readonly ApplicationDbContext _db;

        public int OrganizationLevelId { get; private set; }
        public int RevenueTypeId { get; private set; }

        public RevenuePlanController(ApplicationDbContext db)
        {
            _db = db;
        }


        [HttpGet]
        public JsonResult LoadCenter(int OrganizationLevelId)
        {

            var data = _db.Centers
               .Where(m => m.OrganizationLevelId == OrganizationLevelId)
               .Select(m => new { value = m.CenterId, text = m.CenterNameAmh });
            return Json(data);
        }
       
        public IActionResult Index()
        {
            IEnumerable<RevenuePlan> objList = _db.RevenuePlans;

            foreach (var obj in objList)
            {
                obj.Year = _db.Years.FirstOrDefault(u => u.YearId == obj.YearId);                
                obj.OrganizationLevel = _db.OrganizationLevels.FirstOrDefault(u => u.OrganizationLevelId == obj.OrganizationLevelId);
                obj.Center = _db.Centers.FirstOrDefault(u => u.CenterId == obj.CenterId);
            }

            return View(objList);

        }
        // GET/Create
        // GET-Create
        public IActionResult Create()
        {
            RevenuePlanVM expenseVM = new()
            {
                RevenuePlan = new RevenuePlan(),
                TypeDropDownOrginizationLevel = _db.OrganizationLevels.Select(i => new SelectListItem
                {
                    Text = i.OrganizationLevelNameAmh,
                    Value = i.OrganizationLevelId.ToString()
                }),
                 TypeDropDownYear = _db.Years.Select(i => new SelectListItem
                 {
                    Text = i.YearName,
                    Value = i.YearId.ToString()
                 }),
                TypeDropDownCenter = _db.Centers.Select(i => new SelectListItem
                {
                    Text = i.CenterNameAmh,
                    Value = i.CenterId.ToString()
                }),

            };
            return View(expenseVM);
        }

        // POST/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(RevenuePlanVM obj)
        {
            _db.RevenuePlans.Add(obj.RevenuePlan);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int? id)
        {
            RevenuePlanVM expenseVM = new()
            {
                RevenuePlan = new RevenuePlan(),
                TypeDropDownOrginizationLevel = _db.OrganizationLevels.Select(i => new SelectListItem
                {
                    Text = i.OrganizationLevelNameAmh,
                    Value = i.OrganizationLevelId.ToString()
                })
            };

            if (id == null || id == 0)
            {
                return NotFound();
            }
            expenseVM.RevenuePlan = _db.RevenuePlans.Find(id);
            if (expenseVM.RevenuePlan == null)
            {
                return NotFound();
            }
            return View(expenseVM);
        }

        //POST: Ols/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(RevenuePlanVM obj)
        {

            _db.RevenuePlans.Remove(obj.RevenuePlan);
            _db.SaveChanges();

            return RedirectToAction("Index");
        }

        // GET Update
        public IActionResult Update(int? id)
        {
            RevenuePlanVM centerVM = new()
            {
                RevenuePlan = new RevenuePlan(),
                TypeDropDownOrginizationLevel = _db.OrganizationLevels.Select(i => new SelectListItem
                {
                    Text = i.OrganizationLevelNameAmh,
                    Value = i.OrganizationLevelId.ToString()
                }),
                TypeDropDownYear = _db.Years.Select(i => new SelectListItem
                {
                    Text = i.YearName,
                    Value = i.YearId.ToString()
                }),
                TypeDropDownCenter = _db.Centers.Select(i => new SelectListItem
                {
                    Text = i.CenterNameAmh,
                    Value = i.CenterId.ToString()
                }),
            };

            if (id == null || id == 0)
            {
                return NotFound();
            }
            centerVM.RevenuePlan = _db.RevenuePlans.Find(id);
            if (centerVM.RevenuePlan == null)
            {
                return NotFound();
            }
            return View(centerVM);

        }

        // POST UPDATE
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(RevenuePlanVM obj)
        {
                _db.RevenuePlans.Update(obj.RevenuePlan);
                _db.SaveChanges();
                return RedirectToAction("Index");

        }
    }
}
