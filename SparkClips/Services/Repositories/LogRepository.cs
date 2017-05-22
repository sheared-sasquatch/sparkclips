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

        /// <summary>
        /// Get a list of all the log entries
        /// </summary>
        /// <returns>List of log entry objects</returns>
        public async Task<IEnumerable<LogEntry>> GetLogEntries()
        {
            IEnumerable<LogEntry> logEntries = await _sparkClipsContext.LogEntries
                     .Include(logEntry => logEntry.ApplicationUser)
                     .ToListAsync();

            return logEntries;
        }

        /// <summary>
        /// Get a specific log entry by pk
        /// </summary>
        /// <param name="logEntryID">The pk of the log entry you are trying to fetch</param>
        /// <returns>A single log entry object OR null if nothing is found</returns>
        public async Task<LogEntry> GetLogEntryByID(int logEntryID)
        {
            // untested, but probably will work
            var result = await _sparkClipsContext.LogEntries
                .Include(logEntry => logEntry.Images)
                    .ThenInclude(image => image.Image)
                .Include(logEntry => logEntry.ApplicationUser)
                .SingleOrDefaultAsync(logEntry => logEntry.LogEntryID == logEntryID);

            return result;
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // dispose managed state (managed objects).
                    _sparkClipsContext.Dispose();
                }
                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
