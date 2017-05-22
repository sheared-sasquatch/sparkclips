using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SparkClips.Models.HairyDatabase;
using SparkClips.Data;
using Microsoft.EntityFrameworkCore;

namespace SparkClips.Services.Repositories
{
    public class LogRepository : ILogRepository
    {
        private ApplicationDbContext _sparkClipsContext;

        public LogRepository(ApplicationDbContext context)
        {
            _sparkClipsContext = context;
        }

        public async Task<IEnumerable<LogEntry>> GetLogEntries()
        {
            IEnumerable<LogEntry> logEntries = await _sparkClipsContext.LogEntries
                     .Include(logEntry => logEntry.ApplicationUser)
                     .ToListAsync();

            return logEntries;
        }

        public async Task<LogEntry> GetLogEntryByID(int logEntryID)
        {
            throw new NotImplementedException();
        }
    }
}
