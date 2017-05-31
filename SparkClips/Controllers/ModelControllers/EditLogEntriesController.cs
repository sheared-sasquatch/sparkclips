using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using SparkClips.Data;
using SparkClips.Models.HairyDatabase;
using SparkClips.Models;

namespace SparkClips.Controllers.ModelControllers
{
    public class EditLogEntriesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public EditLogEntriesController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: LogEntries
        public async Task<IActionResult> Index()
        {
            ApplicationUser currentUser = await _userManager.GetUserAsync(User);
            var applicationDbContext = _context.LogEntries.Include(l => l.ApplicationUser);
                
            var list = (await applicationDbContext.ToListAsync()).Where(l => l.ApplicationUser == currentUser);
            return View(list);
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

            ApplicationUser currentUser = await _userManager.GetUserAsync(User);

            if (logEntry == null || currentUser == null || logEntry.ApplicationUserID != currentUser.Id)
            {
                return NotFound();
            }

            return View(logEntry);
        }

        //// GET: LogEntries/Create
        //public IActionResult Create()
        //{
        //    ViewData["ApplicationUserID"] = new SelectList(_context.ApplicationUsers, "Id", "Id");
        //    return View();
        //}

        // POST: LogEntries/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create([Bind("LogEntryID,Description,Cost,DateTimeCreated,Location,Barbers,ApplicationUserID")] LogEntry logEntry)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _context.Add(logEntry);
        //        await _context.SaveChangesAsync();
        //        return RedirectToAction("Index");
        //    }
        //    ViewData["ApplicationUserID"] = new SelectList(_context.ApplicationUsers, "Id", "Id", logEntry.ApplicationUserID);
        //    return View(logEntry);
        //}

        // GET: LogEntries/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var logEntry = await _context.LogEntries.SingleOrDefaultAsync(m => m.LogEntryID == id);
            ApplicationUser currentUser = await _userManager.GetUserAsync(User);

            if (logEntry == null || currentUser == null || logEntry.ApplicationUserID != currentUser.Id)
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
            ApplicationUser currentUser = await _userManager.GetUserAsync(User);
            logEntry.ApplicationUserID = currentUser.Id;


            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(logEntry);
                    //if (id != logEntry.LogEntryID || currentUser == null || currentUser.Id != logEntry.ApplicationUserID)
                    //{
                    //    return NotFound();
                    //}
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
                return Redirect(logEntry.AbsoluteUrl);
                //return RedirectToAction("Index");
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

            ApplicationUser currentUser = await _userManager.GetUserAsync(User);

            if (logEntry == null || currentUser == null || logEntry.ApplicationUserID != currentUser.Id)
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

            ApplicationUser currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null || logEntry.ApplicationUserID != currentUser.Id)
            {
                return NotFound();
            }


            _context.LogEntries.Remove(logEntry);
            await _context.SaveChangesAsync();
            return Redirect(logEntry.AbsoluteUrl);
            //return RedirectToAction("Index");
        }

        private bool LogEntryExists(int id)
        {
            return _context.LogEntries.Any(e => e.LogEntryID == id);
        }
    }
}
