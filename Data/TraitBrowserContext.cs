using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TraitBrowser.Models;

namespace TraitBrowserWebApp.Data
{
    public class TraitBrowserContext : DbContext
    {
        public TraitBrowserContext (DbContextOptions<TraitBrowserContext> options)
            : base(options)
        {
        }

        public DbSet<TraitBrowser.Models.Trait> Trait { get; set; } = default!;
    }
}
