using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SparkClips.Data;
using SparkClips.Models.HairyDatabase;

namespace SparkClips.Controllers.ModelControllers
{
    [Produces("application/json")]
    [Route("api/GalleryEntry_ApplicationUserApi")]
    public class GalleryEntry_ApplicationUserApiController : Controller
    {
        private readonly ApplicationDbContext _context;

        public GalleryEntry_ApplicationUserApiController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/GalleryEntry_ApplicationUserApi
        [HttpGet]
        public IEnumerable<GalleryEntry_ApplicationUser> GetGalleryEntry_ApplicationUser()
        {
            return _context.GalleryEntry_ApplicationUser;
        }

        // GET: api/GalleryEntry_ApplicationUserApi/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetGalleryEntry_ApplicationUser([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var galleryEntry_ApplicationUser = await _context.GalleryEntry_ApplicationUser.SingleOrDefaultAsync(m => m.GalleryEntryID == id);

            if (galleryEntry_ApplicationUser == null)
            {
                return NotFound();
            }

            return Ok(galleryEntry_ApplicationUser);
        }

        // PUT: api/GalleryEntry_ApplicationUserApi/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutGalleryEntry_ApplicationUser([FromRoute] int id, [FromBody] GalleryEntry_ApplicationUser galleryEntry_ApplicationUser)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != galleryEntry_ApplicationUser.GalleryEntryID)
            {
                return BadRequest();
            }

            _context.Entry(galleryEntry_ApplicationUser).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GalleryEntry_ApplicationUserExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/GalleryEntry_ApplicationUserApi
        [HttpPost]
        public async Task<IActionResult> PostGalleryEntry_ApplicationUser([FromBody] GalleryEntry_ApplicationUser galleryEntry_ApplicationUser)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.GalleryEntry_ApplicationUser.Add(galleryEntry_ApplicationUser);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (GalleryEntry_ApplicationUserExists(galleryEntry_ApplicationUser.GalleryEntryID))
                {
                    return new StatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetGalleryEntry_ApplicationUser", new { id = galleryEntry_ApplicationUser.GalleryEntryID }, galleryEntry_ApplicationUser);
        }

        // DELETE: api/GalleryEntry_ApplicationUserApi/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGalleryEntry_ApplicationUser([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var galleryEntry_ApplicationUser = await _context.GalleryEntry_ApplicationUser.SingleOrDefaultAsync(m => m.GalleryEntryID == id);
            if (galleryEntry_ApplicationUser == null)
            {
                return NotFound();
            }

            _context.GalleryEntry_ApplicationUser.Remove(galleryEntry_ApplicationUser);
            await _context.SaveChangesAsync();

            return Ok(galleryEntry_ApplicationUser);
        }

        private bool GalleryEntry_ApplicationUserExists(int id)
        {
            return _context.GalleryEntry_ApplicationUser.Any(e => e.GalleryEntryID == id);
        }
    }
}