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
    public class PhoneNumbersController : Controller
    {
        private readonly AppDbContext _context;

        public PhoneNumbersController(AppDbContext context)
        {
            _context = context;
        }

        // GET: PhoneNumbers
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.PhoneNumber.Include(p => p.Employee);
            return View(await appDbContext.ToListAsync());
        }

        // GET: PhoneNumbers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var phoneNumber = await _context.PhoneNumber
                .Include(p => p.Employee)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (phoneNumber == null)
            {
                return NotFound();
            }

            return View(phoneNumber);
        }

        // GET: PhoneNumbers/Create
        public IActionResult Create()
        {
            ViewData["EmployeeID"] = new SelectList(_context.Employee, "Id", "Name");
            return View();
        }

        // POST: PhoneNumbers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Number,EmployeeID")] PhoneNumber phoneNumber)
        {
            _context.Add(phoneNumber);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: PhoneNumbers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var phoneNumber = await _context.PhoneNumber.FindAsync(id);
            if (phoneNumber == null)
            {
                return NotFound();
            }
            ViewData["EmployeeID"] = new SelectList(_context.Employee, "Id", "Id", phoneNumber.EmployeeID);
            return View(phoneNumber);
        }

        // POST: PhoneNumbers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Number,EmployeeID")] PhoneNumber phoneNumber)
        {
            if (id != phoneNumber.Id)
            {
                return NotFound();
            }

            try
            {
                _context.Update(phoneNumber);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PhoneNumberExists(phoneNumber.Id))
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

        // GET: PhoneNumbers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var phoneNumber = await _context.PhoneNumber
                .Include(p => p.Employee)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (phoneNumber == null)
            {
                return NotFound();
            }

            return View(phoneNumber);
        }

        // POST: PhoneNumbers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var phoneNumber = await _context.PhoneNumber.FindAsync(id);
            if (phoneNumber != null)
            {
                _context.PhoneNumber.Remove(phoneNumber);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PhoneNumberExists(int id)
        {
            return _context.PhoneNumber.Any(e => e.Id == id);
        }
    }
}
