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
    public class GalleryEntry_TagController : Controller
    {
        private readonly ApplicationDbContext _context;

        public GalleryEntry_TagController(ApplicationDbContext context)
        {
            _context = context;    
        }

        // GET: GalleryEntry_Tag
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.GalleryEntry_Tag.Include(g => g.GalleryEntry).Include(g => g.Tag);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: GalleryEntry_Tag/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var galleryEntry_Tag = await _context.GalleryEntry_Tag
                .Include(g => g.GalleryEntry)
                .Include(g => g.Tag)
                .SingleOrDefaultAsync(m => m.TagID == id);
            if (galleryEntry_Tag == null)
            {
                return NotFound();
            }

            return View(galleryEntry_Tag);
        }

        // GET: GalleryEntry_Tag/Create
        public IActionResult Create()
        {
            ViewData["GalleryEntryID"] = new SelectList(_context.GalleryEntries, "GalleryEntryID", "Description");
            ViewData["TagID"] = new SelectList(_context.Tags, "TagID", "Name");
            return View();
        }

        // POST: GalleryEntry_Tag/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("GalleryEntryID,TagID")] GalleryEntry_Tag galleryEntry_Tag)
        {
            if (ModelState.IsValid)
            {
                _context.Add(galleryEntry_Tag);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewData["GalleryEntryID"] = new SelectList(_context.GalleryEntries, "GalleryEntryID", "Description", galleryEntry_Tag.GalleryEntryID);
            ViewData["TagID"] = new SelectList(_context.Tags, "TagID", "Name", galleryEntry_Tag.TagID);
            return View(galleryEntry_Tag);
        }

        // GET: GalleryEntry_Tag/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var galleryEntry_Tag = await _context.GalleryEntry_Tag.SingleOrDefaultAsync(m => m.TagID == id);
            if (galleryEntry_Tag == null)
            {
                return NotFound();
            }
            ViewData["GalleryEntryID"] = new SelectList(_context.GalleryEntries, "GalleryEntryID", "Description", galleryEntry_Tag.GalleryEntryID);
            ViewData["TagID"] = new SelectList(_context.Tags, "TagID", "Name", galleryEntry_Tag.TagID);
            return View(galleryEntry_Tag);
        }

        // POST: GalleryEntry_Tag/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("GalleryEntryID,TagID")] GalleryEntry_Tag galleryEntry_Tag)
        {
            if (id != galleryEntry_Tag.TagID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(galleryEntry_Tag);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GalleryEntry_TagExists(galleryEntry_Tag.TagID))
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
            ViewData["GalleryEntryID"] = new SelectList(_context.GalleryEntries, "GalleryEntryID", "Description", galleryEntry_Tag.GalleryEntryID);
            ViewData["TagID"] = new SelectList(_context.Tags, "TagID", "Name", galleryEntry_Tag.TagID);
            return View(galleryEntry_Tag);
        }

        // GET: GalleryEntry_Tag/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var galleryEntry_Tag = await _context.GalleryEntry_Tag
                .Include(g => g.GalleryEntry)
                .Include(g => g.Tag)
                .SingleOrDefaultAsync(m => m.TagID == id);
            if (galleryEntry_Tag == null)
            {
                return NotFound();
            }

            return View(galleryEntry_Tag);
        }

        // POST: GalleryEntry_Tag/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var galleryEntry_Tag = await _context.GalleryEntry_Tag.SingleOrDefaultAsync(m => m.TagID == id);
            _context.GalleryEntry_Tag.Remove(galleryEntry_Tag);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool GalleryEntry_TagExists(int id)
        {
            return _context.GalleryEntry_Tag.Any(e => e.TagID == id);
        }
    }
}
