using eRevenue.Data;
using eRevenue.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eRevenue.Controllers
{
    public class OrganizationLevelController : Controller
    {
        private readonly ApplicationDbContext _db;
        public OrganizationLevelController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            IEnumerable<OrganizationLevel> objList = _db.OrganizationLevels;
            return View(objList);
        }
        // GET/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(OrganizationLevel obj)
        {
            _db.OrganizationLevels.Add(obj);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var obj = _db.OrganizationLevels.Find(id);
            if (obj == null)
            {
                return NotFound();
            }
            return View(obj);
        }

        // POST: Ols/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ol = await _db.OrganizationLevels.FindAsync(id);
            _db.OrganizationLevels.Remove(ol);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OlExists(int id)
        {
            return _db.OrganizationLevels.Any(e => e.OrganizationLevelId == id);
        }

        public IActionResult Update(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var obj = _db.OrganizationLevels.Find(id);
            if (obj == null)
            {
                return NotFound();
            }
            return View(obj);
        }
        // POST-Update
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(OrganizationLevel obj)
        {
            if (ModelState.IsValid)
            {
                _db.OrganizationLevels.Update(obj);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(obj);
        }
    }
}
