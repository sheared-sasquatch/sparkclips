using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SparkClips.Data;
using SparkClips.Models.HairyDatabase;

namespace SparkClips.Controllers
{
    public class GalleryEntriesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public GalleryEntriesController(ApplicationDbContext context)
        {
            _context = context;    
        }

        // GET: GalleryEntries
        public async Task<IActionResult> Index()
        {
            return View(await _context.GalleryEntries.ToListAsync());
        }

        // GET: GalleryEntries/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var galleryEntry = await _context.GalleryEntries
                .SingleOrDefaultAsync(m => m.GalleryEntryID == id);
            if (galleryEntry == null)
            {
                return NotFound();
            }

            return View(galleryEntry);
        }

        // GET: GalleryEntries/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: GalleryEntries/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("GalleryEntryID,Title,Description,Instructions")] GalleryEntry galleryEntry)
        {
            if (ModelState.IsValid)
            {
                _context.Add(galleryEntry);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(galleryEntry);
        }

        // GET: GalleryEntries/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var galleryEntry = await _context.GalleryEntries.SingleOrDefaultAsync(m => m.GalleryEntryID == id);
            if (galleryEntry == null)
            {
                return NotFound();
            }
            return View(galleryEntry);
        }

        // POST: GalleryEntries/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("GalleryEntryID,Title,Description,Instructions")] GalleryEntry galleryEntry)
        {
            if (id != galleryEntry.GalleryEntryID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(galleryEntry);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GalleryEntryExists(galleryEntry.GalleryEntryID))
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
            return View(galleryEntry);
        }

        // GET: GalleryEntries/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var galleryEntry = await _context.GalleryEntries
                .SingleOrDefaultAsync(m => m.GalleryEntryID == id);
            if (galleryEntry == null)
            {
                return NotFound();
            }

            return View(galleryEntry);
        }

        // POST: GalleryEntries/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var galleryEntry = await _context.GalleryEntries.SingleOrDefaultAsync(m => m.GalleryEntryID == id);
            _context.GalleryEntries.Remove(galleryEntry);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool GalleryEntryExists(int id)
        {
            return _context.GalleryEntries.Any(e => e.GalleryEntryID == id);
        }
    }
}
