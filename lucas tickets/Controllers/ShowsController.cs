using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using lucas_tickets.Data;
using lucas_tickets.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lucas_tickets.Controllers
{
    [Authorize]
    public class ShowsController : Controller
    {   private readonly IConfiguration _configuration;
        private readonly lucas_ticketsContext _context;
        private readonly BlobContainerClient _containerClient;

        public ShowsController(IConfiguration configuration, lucas_ticketsContext context)
        {
            _context = context;
            
            _configuration = configuration;

            var connectionString = _configuration["AzureStorage"];
            var containerName = "lucas-tickets-uploads";
            _containerClient = new BlobContainerClient(connectionString, containerName);
        }

        // GET: Shows
       

        // GET: Shows/Create
        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(_context.Set<Category>(), "CategoryId", "Title");
            return View();
        }

        // POST: Shows/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ShowId,Title,Description,Location,Owner,CategoryId,FormFile")] Shows shows)
        {

            shows.Createdate = DateTime.Now;

            if (ModelState.IsValid)
            { 


                if (shows.FormFile != null)
            {

                IFormFile fileupload = shows.FormFile;

                string blobName = Guid.NewGuid().ToString() + "_" + fileupload.FileName;
                var blobClient = _containerClient.GetBlobClient(blobName);
                using (var stream = fileupload.OpenReadStream())
                {
                    await blobClient.UploadAsync(stream, new BlobHttpHeaders { ContentType = fileupload.ContentType });
                }

                string blobURL = blobClient.Uri.ToString();

                shows.Filename = blobURL;
            }

            _context.Add(shows);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index), "Home");
        }
            ViewData["CategoryId"] = new SelectList(_context.Set<Category>(), "CategoryId", "Title", shows.CategoryId);
            return View(shows);
        }
        
        
        // GET: Shows/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shows = await _context.Shows.FindAsync(id);
            if (shows == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(_context.Set<Category>(), "CategoryId", "Title", shows.CategoryId);
            return View(shows);
        }

        // POST: Shows/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ShowId,Title,Description,Createdate,Filename,Location,Owner,CategoryId")] Shows shows)
        {
            if (id != shows.ShowId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(shows);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ShowsExists(shows.ShowId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index),"home");
            }
            ViewData["CategoryId"] = new SelectList(_context.Set<Category>(), "CategoryId", "CategoryId", shows.CategoryId);
            return View(shows);
        }

        // GET: Shows/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shows = await _context.Shows
                .Include(s => s.Category)
                .FirstOrDefaultAsync(m => m.ShowId == id);
            if (shows == null)
            {
                return NotFound();
            }

            return View(shows);
        }

        // POST: Shows/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var shows = await _context.Shows.FindAsync(id);
            if (shows != null)
            {
                _context.Shows.Remove(shows);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index), "home");
        }




        private bool ShowsExists(int id)
        {
            return _context.Shows.Any(e => e.ShowId == id);
        }
    }
}
