using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Collections.Generic;
using WEB_EXE201.Models;

namespace WEB_EXE201.Pages.Home
{
    public class IndexModel : PageModel
    {
        private readonly MyDbContext _dbContext;

        public IndexModel(MyDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [BindProperty(SupportsGet = true)]
        public string SearchString { get; set; }

        public List<Product> Products { get; set; } = new List<Product>();

        public async Task OnGetAsync()
        {
            Products = await GetProducts(SearchString);
        }

        private async Task<List<Product>> GetProducts(string searchString)
        {
            if (string.IsNullOrWhiteSpace(searchString))
            {
                return await _dbContext.Products
                    .Include(p => p.User)
                    .ToListAsync();
            }

            return await _dbContext.Products
                .Include(p => p.User)
                .Where(p => p.ProductName.Contains(searchString))
                .ToListAsync();
        }
    }
}
