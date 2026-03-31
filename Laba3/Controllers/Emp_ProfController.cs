using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Laba3.Data;
using Laba3.Models;

namespace Laba3.Controllers
{
    public class Emp_ProfController : Controller
    {
        private readonly AppDbContext _context;

        public Emp_ProfController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Emp_Prof
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.Emp_Prof.Include(e => e.Employee).Include(e => e.Profession);
            return View(await appDbContext.ToListAsync());
        }

        // GET: Emp_Prof/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var emp_Prof = await _context.Emp_Prof
                .Include(e => e.Employee)
                .Include(e => e.Profession)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (emp_Prof == null)
            {
                return NotFound();
            }

            return View(emp_Prof);
        }

        // GET: Emp_Prof/Create
        public IActionResult Create()
        {
            ViewData["EmployeeID"] = new SelectList(_context.Employee, "Id", "Name");
            ViewData["ProfessionID"] = new SelectList(_context.Profession, "Id", "Name");
            return View();
        }

        // POST: Emp_Prof/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,EmployeeID,ProfessionID")] Emp_Prof emp_Prof)
        {
            _context.Add(emp_Prof);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: Emp_Prof/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var emp_Prof = await _context.Emp_Prof.FindAsync(id);
            if (emp_Prof == null)
            {
                return NotFound();
            }
            ViewData["EmployeeID"] = new SelectList(_context.Employee, "Id", "Id", emp_Prof.EmployeeID);
            ViewData["ProfessionID"] = new SelectList(_context.Profession, "Id", "Id", emp_Prof.ProfessionID);
            return View(emp_Prof);
        }

        // POST: Emp_Prof/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,EmployeeID,ProfessionID")] Emp_Prof emp_Prof)
        {
            if (id != emp_Prof.Id)
            {
                return NotFound();
            }

            try
            {
                _context.Update(emp_Prof);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Emp_ProfExists(emp_Prof.Id))
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

        // GET: Emp_Prof/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var emp_Prof = await _context.Emp_Prof
                .Include(e => e.Employee)
                .Include(e => e.Profession)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (emp_Prof == null)
            {
                return NotFound();
            }

            return View(emp_Prof);
        }

        // POST: Emp_Prof/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var emp_Prof = await _context.Emp_Prof.FindAsync(id);
            if (emp_Prof != null)
            {
                _context.Emp_Prof.Remove(emp_Prof);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool Emp_ProfExists(int id)
        {
            return _context.Emp_Prof.Any(e => e.Id == id);
        }
    }
}
