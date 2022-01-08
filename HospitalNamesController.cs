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
    public class HospitalNamesController : Controller
    {
        private readonly SpitalsContext _context;

        public HospitalNamesController(SpitalsContext context)
        {
            _context = context;
        }

        // GET: HospitalNames
        public async Task<IActionResult> Index()
        {
            return View(await _context.HospitalNames.ToListAsync());
        }

        // GET: HospitalNames/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hospitalName = await _context.HospitalNames
                .FirstOrDefaultAsync(m => m.HospitalNameId == id);
            if (hospitalName == null)
            {
                return NotFound();
            }

            return View(hospitalName);
        }

        // GET: HospitalNames/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: HospitalNames/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("HospitalNameId,HospitalName1")] HospitalName hospitalName)
        {
            if (ModelState.IsValid)
            {
                _context.Add(hospitalName);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(hospitalName);
        }

        // GET: HospitalNames/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hospitalName = await _context.HospitalNames.FindAsync(id);
            if (hospitalName == null)
            {
                return NotFound();
            }
            return View(hospitalName);
        }

        // POST: HospitalNames/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("HospitalNameId,HospitalName1")] HospitalName hospitalName)
        {
            if (id != hospitalName.HospitalNameId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(hospitalName);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HospitalNameExists(hospitalName.HospitalNameId))
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
            return View(hospitalName);
        }

        // GET: HospitalNames/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hospitalName = await _context.HospitalNames
                .FirstOrDefaultAsync(m => m.HospitalNameId == id);
            if (hospitalName == null)
            {
                return NotFound();
            }

            return View(hospitalName);
        }

        // POST: HospitalNames/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var hospitalName = await _context.HospitalNames.FindAsync(id);
            _context.HospitalNames.Remove(hospitalName);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HospitalNameExists(int id)
        {
            return _context.HospitalNames.Any(e => e.HospitalNameId == id);
        }
    }
}
