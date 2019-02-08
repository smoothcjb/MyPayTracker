using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyPayTracker.Data;
using MyPayTracker.Models;

namespace MyPayTracker.Controllers
{
    public class TimeSheetsController : Controller
    {
        private readonly TimeSheetDbContext _context;

        public TimeSheetsController(TimeSheetDbContext context)
        {
            _context = context;
        }

        // GET: TimeSheets
        public async Task<IActionResult> Index()
        {
            ViewBag.date = DateTime.Now;
            var timeSheets = _context.TimeSheets.Include(t => t.Employee);
            return View(await timeSheets.ToListAsync());
        }

        // GET: TimeSheets/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var timeSheet = await _context.TimeSheets
                .Include(t => t.Employee)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (timeSheet == null)
            {
                return NotFound();
            }

            return View(timeSheet);
        }

        // GET: TimeSheets/Create
        public IActionResult Create()
        {
            ViewData["EmployeeID"] = new SelectList(_context.Employees, "ID", "DisplayName");
            return View();
        }

        // POST: TimeSheets/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int id, DateTime timeIn, DateTime timeOut, int employeeId )
        {
            TimeSheet timeSheet = new TimeSheet
            {
                ID = id,
                TimeIn = timeIn,
                TimeOut = timeOut,
                HoursWorked = timeOut - timeIn,
                EmployeeID = employeeId
            };
            if (ModelState.IsValid)
            {   
                _context.Add(timeSheet);
                await _context.SaveChangesAsync();
                return Redirect($"Details/{timeSheet.ID}");
            }
            ViewData["EmployeeID"] = new SelectList(_context.Employees, "ID", "DisplayName", timeSheet.EmployeeID);
            return View(timeSheet);
        }

        // GET: TimeSheets/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var timeSheet = await _context.TimeSheets.FindAsync(id);
            if (timeSheet == null)
            {
                return NotFound();
            }
            ViewData["EmployeeID"] = new SelectList(_context.Employees, "ID", "DisplayName", timeSheet.EmployeeID);
            return View(timeSheet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,TimeIn,TimeOut,HoursWorked,EmployeeID")] TimeSheet timeSheet)
        {
            if (id != timeSheet.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(timeSheet);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TimeSheetExists(timeSheet.ID))
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
            ViewData["EmployeeID"] = new SelectList(_context.Employees, "ID", "ID", timeSheet.EmployeeID);
            return View(timeSheet);
        }

        // GET: TimeSheets/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var timeSheet = await _context.TimeSheets
                .Include(t => t.Employee)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (timeSheet == null)
            {
                return NotFound();
            }

            return View(timeSheet);
        }

        // POST: TimeSheets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var timeSheet = await _context.TimeSheets.FindAsync(id);
            _context.TimeSheets.Remove(timeSheet);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TimeSheetExists(int id)
        {
            return _context.TimeSheets.Any(e => e.ID == id);
        }

        public async Task<IActionResult> Search(DateTime From, DateTime To, int id)
        {

            var searchResults = _context.TimeSheets.Include(t => t.Employee)
            .Where(i => i.TimeIn.Date >= From && i.TimeOut.Date <= To)
            .Where(e => e.EmployeeID == id);

            ViewBag.employees = _context.Employees.ToList();
            return View(await searchResults.ToListAsync());
        }
    }
}
