﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WEB_EXE201.Models;

namespace WEB_EXE201.Pages.Products
{
    public class IndexModel : PageModel
    {
        private readonly WEB_EXE201.Models.MyDbContext _context;

        public IndexModel(WEB_EXE201.Models.MyDbContext context)
        {
            _context = context;
        }

        public IList<Product> Product { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Products != null)
            {
                Product = await _context.Products
                .Include(p => p.User).ToListAsync();
            }
        }
    }
}
