using eRevenue.Data;
using eRevenue.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eRevenue.Controllers
{
    public class RevenueTypeDetailController : Controller
    {
        private readonly ApplicationDbContext _db;
        public RevenueTypeDetailController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            IEnumerable<RevenueTypeDetail> objList = _db.RevenueTypeDetails;
            return View(objList);
        }
        // GET/Create
        // GET-Create
        //public IActionResult Create()
        //{
        //    CenterVM expenseVM = new()
        //    {
        //        Center = new Center(),
        //        TypeDropDownOrginizationLevel = _db.OrganizationLevels.Select(i => new SelectListItem
        //        {
        //            Text = i.OrganizationLevelNameAmh,
        //            Value = i.OrganizationLevelId.ToString()
        //        })
        //    };
        //    return View(expenseVM);
        //}

        //// POST/Create
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public IActionResult Create(CenterVM obj)
        //{
        //    _db.Centers.Add(obj.Center);
        //    _db.SaveChanges();
        //    return RedirectToAction("Index");
        //}

        //public IActionResult Delete(int? id)
        //{
        //    CenterVM expenseVM = new()
        //    {
        //        Center = new Center(),
        //        TypeDropDownOrginizationLevel = _db.OrganizationLevels.Select(i => new SelectListItem
        //        {
        //            Text = i.OrganizationLevelNameAmh,
        //            Value = i.OrganizationLevelId.ToString()
        //        })
        //    };

        //    if (id == null || id == 0)
        //    {
        //        return NotFound();
        //    }
        //    expenseVM.Center = _db.Centers.Find(id);
        //    if (expenseVM.Center == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(expenseVM);
        //}

        ////POST: Ols/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public IActionResult DeleteConfirmed(CenterVM obj)
        //{

        //    _db.Centers.Remove(obj.Center);
        //    _db.SaveChanges();

        //    return RedirectToAction("Index");
        //}

        //// GET Update
        //public IActionResult Update(int? id)
        //{
        //    CenterVM centerVM = new()
        //    {
        //        Center = new Center(),
        //        TypeDropDownOrginizationLevel = _db.OrganizationLevels.Select(i => new SelectListItem
        //        {
        //            Text = i.OrganizationLevelNameAmh,
        //            Value = i.OrganizationLevelId.ToString()
        //        })
        //    };

        //    if (id == null || id == 0)
        //    {
        //        return NotFound();
        //    }
        //    centerVM.Center = _db.Centers.Find(id);
        //    if (centerVM.Center == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(centerVM);

        //}

        //// POST UPDATE
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public IActionResult Update(CenterVM obj)
        //{
        //    _db.Centers.Update(obj.Center);
        //    _db.SaveChanges();
        //    return RedirectToAction("Index");

        //}
    }
}

