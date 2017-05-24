using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SparkClips.Models.HairyDatabase;
using SparkClips.Data;
using Microsoft.EntityFrameworkCore;

namespace SparkClips.Services.Repositories
{
    public class TagRepository : ITagRepository
    {
        private ApplicationDbContext _sparkClipsContext;

        public TagRepository(ApplicationDbContext context)
        {
            _sparkClipsContext = context;
        }

        /// <summary>
        /// Get a list of all the tags
        /// </summary>
        /// <returns>List of tag objects</returns>
        public async Task<IEnumerable<Tag>> GetTags()
        {
            IEnumerable<Tag> tags = await _sparkClipsContext.Tags
                .ToListAsync();
            return tags;
        }
    }
}
