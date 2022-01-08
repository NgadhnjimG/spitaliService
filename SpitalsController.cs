using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Spitali.Models;

namespace Spitali
{
    public class SpitalsController : Controller
    {
        private readonly SpitalsContext _context;

        public SpitalsController(SpitalsContext context)
        {
            _context = context;
        }

        // GET: Spitals
        public async Task<IActionResult> Index()
        {
            var spitalsContext = _context.Spitals.Include(s => s.Department).Include(s => s.HospitalNames);
            return View(await spitalsContext.ToListAsync());
        }

        // GET: Spitals/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var spital = await _context.Spitals
                .Include(s => s.Department)
                .Include(s => s.HospitalNames)
                .FirstOrDefaultAsync(m => m.SpitalId == id);
            if (spital == null)
            {
                return NotFound();
            }

            return View(spital);
        }

        // GET: Spitals/Create
        public IActionResult Create()
        {
            ViewData["Departments"] = new SelectList(_context.Departments, "DepartmentId", "Departments");
            ViewData["HospitalName"] = new SelectList(_context.HospitalNames, "HospitalNameId", "HospitalName1");
            return View();
        }

        // POST: Spitals/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SpitalId,HospitalName,Address,Departments,EmployeeNumber")] Spital spital)
        {
            if (ModelState.IsValid)
            {
                _context.Add(spital);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Departments"] = new SelectList(_context.Departments, "DepartmentId", "Departments", spital.Departments);
            ViewData["HospitalName"] = new SelectList(_context.HospitalNames, "HospitalNameId", "HospitalName1", spital.HospitalName);
            return View(spital);
        }

        // GET: Spitals/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var spital = await _context.Spitals.FindAsync(id);
            if (spital == null)
            {
                return NotFound();
            }
            ViewData["Departments"] = new SelectList(_context.Departments, "DepartmentId", "Departments", spital.Departments);
            ViewData["HospitalName"] = new SelectList(_context.HospitalNames, "HospitalNameId", "HospitalName1", spital.HospitalName);
            return View(spital);
        }

        // POST: Spitals/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SpitalId,HospitalName,Address,Departments,EmployeeNumber")] Spital spital)
        {
            if (id != spital.SpitalId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(spital);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SpitalExists(spital.SpitalId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["Departments"] = new SelectList(_context.Departments, "DepartmentId", "Departments", spital.Departments);
            ViewData["HospitalName"] = new SelectList(_context.HospitalNames, "HospitalNameId", "HospitalName1", spital.HospitalName);
            return View(spital);
        }

        // GET: Spitals/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var spital = await _context.Spitals
                .Include(s => s.Department)
                .Include(s => s.HospitalNames)
                .FirstOrDefaultAsync(m => m.SpitalId == id);
            if (spital == null)
            {
                return NotFound();
            }

            return View(spital);
        }

        // POST: Spitals/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var spital = await _context.Spitals.FindAsync(id);
            _context.Spitals.Remove(spital);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SpitalExists(int id)
        {
            return _context.Spitals.Any(e => e.SpitalId == id);
        }
    }
}
