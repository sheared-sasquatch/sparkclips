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
    public class GalleryEntry_ImageController : Controller
    {
        private readonly SparkClipsContext _context;

        public GalleryEntry_ImageController(SparkClipsContext context)
        {
            _context = context;    
        }

        // GET: GalleryEntry_Image
        public async Task<IActionResult> Index()
        {
            var sparkClipsContext = _context.GalleryEntry_Image.Include(g => g.GalleryEntry).Include(g => g.Image);
            return View(await sparkClipsContext.ToListAsync());
        }

        // GET: GalleryEntry_Image/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var galleryEntry_Image = await _context.GalleryEntry_Image
                .Include(g => g.GalleryEntry)
                .Include(g => g.Image)
                .SingleOrDefaultAsync(m => m.ImageID == id);
            if (galleryEntry_Image == null)
            {
                return NotFound();
            }

            return View(galleryEntry_Image);
        }

        // GET: GalleryEntry_Image/Create
        public IActionResult Create()
        {
            ViewData["GalleryEntryID"] = new SelectList(_context.GalleryEntries, "GalleryEntryID", "GalleryEntryID");
            ViewData["ImageID"] = new SelectList(_context.Images, "ImageID", "ImageID");
            return View();
        }

        // POST: GalleryEntry_Image/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ImageID,GalleryEntryID")] GalleryEntry_Image galleryEntry_Image)
        {
            if (ModelState.IsValid)
            {
                _context.Add(galleryEntry_Image);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewData["GalleryEntryID"] = new SelectList(_context.GalleryEntries, "GalleryEntryID", "GalleryEntryID", galleryEntry_Image.GalleryEntryID);
            ViewData["ImageID"] = new SelectList(_context.Images, "ImageID", "ImageID", galleryEntry_Image.ImageID);
            return View(galleryEntry_Image);
        }

        // GET: GalleryEntry_Image/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var galleryEntry_Image = await _context.GalleryEntry_Image.SingleOrDefaultAsync(m => m.ImageID == id);
            if (galleryEntry_Image == null)
            {
                return NotFound();
            }
            ViewData["GalleryEntryID"] = new SelectList(_context.GalleryEntries, "GalleryEntryID", "GalleryEntryID", galleryEntry_Image.GalleryEntryID);
            ViewData["ImageID"] = new SelectList(_context.Images, "ImageID", "ImageID", galleryEntry_Image.ImageID);
            return View(galleryEntry_Image);
        }

        // POST: GalleryEntry_Image/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ImageID,GalleryEntryID")] GalleryEntry_Image galleryEntry_Image)
        {
            if (id != galleryEntry_Image.ImageID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(galleryEntry_Image);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GalleryEntry_ImageExists(galleryEntry_Image.ImageID))
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
            ViewData["GalleryEntryID"] = new SelectList(_context.GalleryEntries, "GalleryEntryID", "GalleryEntryID", galleryEntry_Image.GalleryEntryID);
            ViewData["ImageID"] = new SelectList(_context.Images, "ImageID", "ImageID", galleryEntry_Image.ImageID);
            return View(galleryEntry_Image);
        }

        // GET: GalleryEntry_Image/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var galleryEntry_Image = await _context.GalleryEntry_Image
                .Include(g => g.GalleryEntry)
                .Include(g => g.Image)
                .SingleOrDefaultAsync(m => m.ImageID == id);
            if (galleryEntry_Image == null)
            {
                return NotFound();
            }

            return View(galleryEntry_Image);
        }

        // POST: GalleryEntry_Image/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var galleryEntry_Image = await _context.GalleryEntry_Image.SingleOrDefaultAsync(m => m.ImageID == id);
            _context.GalleryEntry_Image.Remove(galleryEntry_Image);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool GalleryEntry_ImageExists(int id)
        {
            return _context.GalleryEntry_Image.Any(e => e.ImageID == id);
        }
    }
}
