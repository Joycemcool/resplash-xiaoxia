﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ReSplash.Data;
using ReSplash.Models;

namespace ReSplash.Pages.Photos
{
    public class CreateModel : PageModel
    {
        private readonly ReSplashContext _context;
        
        [BindProperty]
        public Photo Photo { get; set; } = default!;

        [BindProperty]
        public IFormFile ImageUpload { get; set; }

        IWebHostEnvironment _env;

        public CreateModel(ReSplashContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        public IActionResult OnGet()
        {
            return Page();
        }   

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            // Set default values

            User? user = _context.User.Where(u => u.UserId == 1).SingleOrDefault();

            if (user != null)
            {
                Photo.User = user;
            }

            // Make a unique image name
            string imageName = DateTime.Now.ToString("yyyy-MM-dd-hh-mm-ss-") + ImageUpload.FileName;
                        
            Photo.FileName = imageName;
            Photo.PublishDate = DateTime.Now;
            Photo.ImageViews = 0;
            Photo.ImageDownloads = 0;

            // Validate model
            if (!ModelState.IsValid || _context.Photo == null || Photo == null)
            {
                return Page();
            }

            // Save to database
            _context.Photo.Add(Photo);
            await _context.SaveChangesAsync();

            //
            // Upload the Image to the www/photo folder
            //
            
            string file = _env.ContentRootPath + "\\wwwroot\\photos\\" + imageName;

            using(FileStream fileStream = new FileStream(file, FileMode.Create))
            {
                ImageUpload.CopyTo(fileStream);
            }

            return RedirectToPage("./Index");
        }
    }
}
