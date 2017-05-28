using Microsoft.AspNetCore.Mvc;
using SparkClips.Models.HairyDatabase;
using SparkClips.Services.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
// Below two dependencies required for UserManager
using Microsoft.AspNetCore.Identity;
using SparkClips.Models;



namespace SparkClips.Controllers
{
    public class GalleryController : Controller
    {
        private IGalleryRepository _galleryRepository;
        private ITagRepository _tagRepository;
        private readonly UserManager<ApplicationUser> _userManager;

        public GalleryController(IGalleryRepository galleryRepository, UserManager<ApplicationUser> userManager, ITagRepository tagRepository)
        {
            _galleryRepository = galleryRepository;
            _userManager = userManager;
            _tagRepository = tagRepository;
        }

        // GET: /Gallery/
        public async Task<IActionResult> Index(List<int> tags)
        {
            IEnumerable<GalleryEntry> galleryEntries = await _galleryRepository.GetGalleryEntries();
            galleryEntries = galleryEntries.Where(galleryEntry => 
                tags.All(tag => galleryEntry.Tags.Select(t => t.TagID).Contains(tag)));

            // loop over each gallery entry and add any computed fields
            foreach (GalleryEntry galleryEntry in galleryEntries)
            {
                // setting the thumbnail image
                galleryEntry.Thumbnail = _galleryRepository.ComputeThumbnail(galleryEntry);
                // seting the gallery entry number of likes
                galleryEntry.Likes = await _galleryRepository.ComputeNLikes(galleryEntry);
                // Boolean has the picture been favorited by this user already
                var user = await _userManager.GetUserAsync(User);
                if(user != null) {
                    galleryEntry.Faved = _galleryRepository.isFavorited(galleryEntry.GalleryEntryID, user.Id);
                }
                
            }
            galleryEntries = galleryEntries.OrderByDescending(galleryEntry => galleryEntry.Likes);

            // fetch tags from the database
            IEnumerable<Tag> database_tags = await _tagRepository.GetTags();
            // Transform list of tags into a list of tuples with the id, name, and bool to see if it should be checked
            IEnumerable<Tuple<int, string, bool>> tupled_database_tags = database_tags
                .Select(tag => Tuple.Create(tag.TagID, tag.Name, tags.Contains(tag.TagID)));

            // pass list of tuples to the ViewData for the View to use
            ViewData["tags"] = tupled_database_tags;

            return View(galleryEntries);
        }

        // GET: /Gallery/Detail/1
        public async Task<IActionResult> Detail(int ID)
        {
            GalleryEntry galleryEntry = await _galleryRepository.GetGalleryEntryByID(ID);
            if (galleryEntry == null)
            {
                return NotFound();
            }
            return View(galleryEntry);
        }
    }
}
