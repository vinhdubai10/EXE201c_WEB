using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using WEB_EXE201.Models;

namespace WEB_EXE201.Pages.Products
{
    public class CreateModel : PageModel
    {
        private readonly WEB_EXE201.Models.MyDbContext _context;
        private readonly IWebHostEnvironment _environment;

        public CreateModel(WEB_EXE201.Models.MyDbContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        public IActionResult OnGet()
        {
        ViewData["UserID"] = new SelectList(_context.Users, "UserID", "UserID");
            return Page();
        }
        [BindProperty]
        public IFormFile ImageUpload { get; set; }
        [BindProperty]
        public Product Product { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (_context.Products == null || Product == null)
            {
                return Page();
            }
            if (ImageUpload != null)
            {
                // Save image to wwwroot/uploads
                var uploadFolder = Path.Combine(_environment.WebRootPath, "images");
                if (!Directory.Exists(uploadFolder))
                {
                    Directory.CreateDirectory(uploadFolder);
                }

                var mime = Path.GetFileName(ImageUpload.FileName);
                var fileName = string.Concat(Path.GetRandomFileName(), ".", mime);
                var savePath = Path.Combine(uploadFolder, fileName);

                using (var fileStream = new FileStream(savePath, FileMode.Create, FileAccess.Write))
                {
                    await ImageUpload.CopyToAsync(fileStream);
                }

                // Save the file name in the Product.ImageUrl field
                Product.ImageUrl = fileName;
            }
            Product.DateCreated = DateTime.Now;
            _context.Products.Add(Product);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
