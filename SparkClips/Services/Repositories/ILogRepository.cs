using SparkClips.Models.HairyDatabase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SparkClips.Services.Repositories
{
    public interface ILogRepository
    {
        Task<IEnumerable<LogEntry>> GetLogEntries();
        Task<LogEntry> GetLogEntryByID(int logEntryID);
    }
}
