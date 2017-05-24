using SparkClips.Models.HairyDatabase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SparkClips.Services.Repositories
{
    public interface ITagRepository
    {
        Task<IEnumerable<Tag>> GetTags();
    }
}
