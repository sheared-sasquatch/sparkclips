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
    public class GalleryEntry_ApplicationUserController : Controller
    {
        private readonly ApplicationDbContext _context;

        public GalleryEntry_ApplicationUserController(ApplicationDbContext context)
        {
            _context = context;    
        }

        // GET: GalleryEntry_ApplicationUser
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.GalleryEntry_ApplicationUser.Include(g => g.ApplicationUser).Include(g => g.GalleryEntry);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: GalleryEntry_ApplicationUser/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var galleryEntry_ApplicationUser = await _context.GalleryEntry_ApplicationUser
                .Include(g => g.ApplicationUser)
                .Include(g => g.GalleryEntry)
                .SingleOrDefaultAsync(m => m.GalleryEntryID == id);
            if (galleryEntry_ApplicationUser == null)
            {
                return NotFound();
            }

            return View(galleryEntry_ApplicationUser);
        }

        // GET: GalleryEntry_ApplicationUser/Create
        public IActionResult Create()
        {
            ViewData["ApplicationUserID"] = new SelectList(_context.ApplicationUsers, "Id", "Id");
            ViewData["GalleryEntryID"] = new SelectList(_context.GalleryEntries, "GalleryEntryID", "GalleryEntryID");
            return View();
        }

        // POST: GalleryEntry_ApplicationUser/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("GalleryEntryID,ApplicationUserID")] GalleryEntry_ApplicationUser galleryEntry_ApplicationUser)
        {
            if (ModelState.IsValid)
            {
                _context.Add(galleryEntry_ApplicationUser);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewData["ApplicationUserID"] = new SelectList(_context.ApplicationUsers, "Id", "Id", galleryEntry_ApplicationUser.ApplicationUserID);
            ViewData["GalleryEntryID"] = new SelectList(_context.GalleryEntries, "GalleryEntryID", "GalleryEntryID", galleryEntry_ApplicationUser.GalleryEntryID);
            return View(galleryEntry_ApplicationUser);
        }

        // GET: GalleryEntry_ApplicationUser/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var galleryEntry_ApplicationUser = await _context.GalleryEntry_ApplicationUser.SingleOrDefaultAsync(m => m.GalleryEntryID == id);
            if (galleryEntry_ApplicationUser == null)
            {
                return NotFound();
            }
            ViewData["ApplicationUserID"] = new SelectList(_context.ApplicationUsers, "Id", "Id", galleryEntry_ApplicationUser.ApplicationUserID);
            ViewData["GalleryEntryID"] = new SelectList(_context.GalleryEntries, "GalleryEntryID", "GalleryEntryID", galleryEntry_ApplicationUser.GalleryEntryID);
            return View(galleryEntry_ApplicationUser);
        }

        // POST: GalleryEntry_ApplicationUser/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("GalleryEntryID,ApplicationUserID")] GalleryEntry_ApplicationUser galleryEntry_ApplicationUser)
        {
            if (id != galleryEntry_ApplicationUser.GalleryEntryID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(galleryEntry_ApplicationUser);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GalleryEntry_ApplicationUserExists(galleryEntry_ApplicationUser.GalleryEntryID))
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
            ViewData["ApplicationUserID"] = new SelectList(_context.ApplicationUsers, "Id", "Id", galleryEntry_ApplicationUser.ApplicationUserID);
            ViewData["GalleryEntryID"] = new SelectList(_context.GalleryEntries, "GalleryEntryID", "GalleryEntryID", galleryEntry_ApplicationUser.GalleryEntryID);
            return View(galleryEntry_ApplicationUser);
        }

        // GET: GalleryEntry_ApplicationUser/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var galleryEntry_ApplicationUser = await _context.GalleryEntry_ApplicationUser
                .Include(g => g.ApplicationUser)
                .Include(g => g.GalleryEntry)
                .SingleOrDefaultAsync(m => m.GalleryEntryID == id);
            if (galleryEntry_ApplicationUser == null)
            {
                return NotFound();
            }

            return View(galleryEntry_ApplicationUser);
        }

        // POST: GalleryEntry_ApplicationUser/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var galleryEntry_ApplicationUser = await _context.GalleryEntry_ApplicationUser.SingleOrDefaultAsync(m => m.GalleryEntryID == id);
            _context.GalleryEntry_ApplicationUser.Remove(galleryEntry_ApplicationUser);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool GalleryEntry_ApplicationUserExists(int id)
        {
            return _context.GalleryEntry_ApplicationUser.Any(e => e.GalleryEntryID == id);
        }
    }
}
