using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SparkClips.Data;
using SparkClips.Models.HairyDatabase;

namespace SparkClips.Controllers.ModelControllers
{
    public class LogEntriesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public LogEntriesController(ApplicationDbContext context)
        {
            _context = context;    
        }

        // GET: LogEntries
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.LogEntries.Include(l => l.ApplicationUser);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: LogEntries/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var logEntry = await _context.LogEntries
                .Include(l => l.ApplicationUser)
                .SingleOrDefaultAsync(m => m.LogEntryID == id);
            if (logEntry == null)
            {
                return NotFound();
            }

            return View(logEntry);
        }

        // GET: LogEntries/Create
        public IActionResult Create()
        {
            ViewData["ApplicationUserID"] = new SelectList(_context.ApplicationUsers, "Id", "Id");
            return View();
        }

        // POST: LogEntries/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("LogEntryID,Description,Cost,DateTimeCreated,Location,Barbers,ApplicationUserID")] LogEntry logEntry)
        {
            if (ModelState.IsValid)
            {
                _context.Add(logEntry);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewData["ApplicationUserID"] = new SelectList(_context.ApplicationUsers, "Id", "Id", logEntry.ApplicationUserID);
            return View(logEntry);
        }

        // GET: LogEntries/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var logEntry = await _context.LogEntries.SingleOrDefaultAsync(m => m.LogEntryID == id);
            if (logEntry == null)
            {
                return NotFound();
            }
            ViewData["ApplicationUserID"] = new SelectList(_context.ApplicationUsers, "Id", "Id", logEntry.ApplicationUserID);
            return View(logEntry);
        }

        // POST: LogEntries/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("LogEntryID,Description,Cost,DateTimeCreated,Location,Barbers,ApplicationUserID")] LogEntry logEntry)
        {
            if (id != logEntry.LogEntryID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(logEntry);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LogEntryExists(logEntry.LogEntryID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index");
            }
            ViewData["ApplicationUserID"] = new SelectList(_context.ApplicationUsers, "Id", "Id", logEntry.ApplicationUserID);
            return View(logEntry);
        }

        // GET: LogEntries/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var logEntry = await _context.LogEntries
                .Include(l => l.ApplicationUser)
                .SingleOrDefaultAsync(m => m.LogEntryID == id);
            if (logEntry == null)
            {
                return NotFound();
            }

            return View(logEntry);
        }

        // POST: LogEntries/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var logEntry = await _context.LogEntries.SingleOrDefaultAsync(m => m.LogEntryID == id);
            _context.LogEntries.Remove(logEntry);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool LogEntryExists(int id)
        {
            return _context.LogEntries.Any(e => e.LogEntryID == id);
        }
    }
}
