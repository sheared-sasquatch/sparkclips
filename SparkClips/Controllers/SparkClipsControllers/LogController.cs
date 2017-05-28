﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SparkClips.Data;
using SparkClips.Models.HairyDatabase;
using SparkClips.Services.BlobBob;
using SparkClips.Services.Repositories;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using SparkClips.Models;

namespace SparkClips.Controllers
{
    public class LogController : Controller
    {
        private IFileStorage _fileStorage;
        private readonly UserManager<ApplicationUser> _userManager;
        private ILogRepository _logRepository;
        private ApplicationDbContext _sparkClipsContext;

        public LogController(ApplicationDbContext sparkClipsContext, IFileStorage fileStorage, ILogRepository logRepository, UserManager<ApplicationUser> userManager)
        {
            _sparkClipsContext = sparkClipsContext;
            _fileStorage = fileStorage; // store a reference to the file storage object from ASP .NET's dependency injection container
            _logRepository = logRepository;
            _userManager = userManager;
        }

        // GET: /Log/
        public async Task<IActionResult> Index()
        {
            IEnumerable<LogEntry> logEntries = await _logRepository.GetLogEntries();

            foreach (LogEntry logEntry in logEntries)
            {
            // setting the thumbnail image
               logEntry.Thumbnail = _logRepository.ComputeThumbnail(logEntry);
            }
            return View(logEntries);
        }

        // GET: /Log/Detail
        public async Task<IActionResult> Detail(int ID)
        {
            LogEntry logEntry = await _logRepository.GetLogEntryByID(ID);
            if (logEntry == null)
            {
                return NotFound();
            }
            return View(logEntry);
        }

        public IActionResult Entry()
        {
            return View();
        }


        /// <summary>
        /// Receive one or more files from the user's POST request
        /// and upload them to blob storage
        /// </summary>
        /// <param name="files">List of FormFile objects that will
        /// get passed to the FileStorage.UploadImage method</param>
        /// <returns>A JSON object which gives some useless data about the files uploaded.
        /// This should probably become a redirect at some point.</returns>
        [HttpPost]
        public async Task<IActionResult> ProcessEntry(List<IFormFile> files, LogEntry logEntry)
        {
            long size = files.Sum(f => f.Length);
            logEntry.ApplicationUser = await _userManager.GetUserAsync(User);
            _sparkClipsContext.LogEntries.Add(logEntry);
            _sparkClipsContext.SaveChanges();

            foreach (FormFile formFile in files)
            {
                if (formFile.Length > 0)
                {
                    Image image = await _fileStorage.UploadImage(ContainerName.Gallery, formFile);
                    _sparkClipsContext.Images.Add(image);
                    _sparkClipsContext.SaveChanges();

                    LogEntry_Image associative_entity = new LogEntry_Image {
                      LogEntryID = logEntry.LogEntryID,
                      ImageID = image.ImageID
                    };
                    _sparkClipsContext.LogEntry_Image.Add(associative_entity);
                    _sparkClipsContext.SaveChanges();
                }
            }

            // process uploaded files
            // Don't rely on or trust the FileName property without validation.

            return RedirectToAction("Index");
            return Ok(new { count = files.Count, size });
        }
    }
}
