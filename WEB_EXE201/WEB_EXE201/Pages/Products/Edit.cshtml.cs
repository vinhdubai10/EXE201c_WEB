using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WEB_EXE201.Models;

namespace WEB_EXE201.Pages.Products
{
    public class EditModel : PageModel
    {
        private readonly WEB_EXE201.Models.MyDbContext _context;
        private readonly IWebHostEnvironment _environment;

        public EditModel(WEB_EXE201.Models.MyDbContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        [BindProperty]
        public Product Product { get; set; } = default!;
        [BindProperty]
        public IFormFile ImageUpload { get; set; }
        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Products == null)
            {
                return NotFound();
            }

            var product = await _context.Products.FirstOrDefaultAsync(m => m.ProductID == id);
            if (product == null)
            {
                return NotFound();
            }
            Product = product;
            ViewData["UserID"] = new SelectList(_context.Users, "UserID", "UserID");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            var productToUpdate = await _context.Products.FindAsync(Product.ProductID);

            if (productToUpdate == null)
            {
                return NotFound();
            }
            if (ImageUpload != null)
            {
                if (!string.IsNullOrEmpty(productToUpdate.ImageUrl))
                {
                    var oldImagePath = Path.Combine(_environment.WebRootPath, "images", productToUpdate.ImageUrl);
                    if (System.IO.File.Exists(oldImagePath))
                    {
                        System.IO.File.Delete(oldImagePath);
                    }
                }
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
                productToUpdate.ImageUrl = fileName;
            }
            productToUpdate.ProductName = Product.ProductName;
            productToUpdate.Description = Product.Description;
            productToUpdate.DateModified = DateTime.Now;
            _context.Update(productToUpdate);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
