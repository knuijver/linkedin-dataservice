using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SAWebHost.Data;
using SAWebHost.Data.Dto;

namespace SAWebHost.Pages.Tokens
{
    public class IndexModel : PageModel
    {
        private readonly SAWebHost.Data.SAWebHostContext _context;

        public IndexModel(SAWebHost.Data.SAWebHostContext context)
        {
            _context = context;
        }

        public IList<AccessTokenEntry> AccessTokenEntry { get;set; }

        public async Task OnGetAsync()
        {
            AccessTokenEntry = await _context.AccessTokenEntry.ToListAsync();
        }
    }
}
