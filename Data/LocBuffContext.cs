using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TraitBrowserWebApp.Models;

namespace TraitBrowserWebApp.Data
{
    public class LocBuffContext : DbContext
    {
        public LocBuffContext (DbContextOptions<LocBuffContext> options)
            : base(options)
        {
        }

        public DbSet<TraitBrowserWebApp.Models.LocBuff> LocBuff { get; set; } = default!;
    }
}
