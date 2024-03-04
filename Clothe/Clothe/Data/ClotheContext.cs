using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Clothe.Models;

namespace Clothe.Data
{
    public class ClotheContext : DbContext
    {
        public ClotheContext (DbContextOptions<ClotheContext> options)
            : base(options)
        {
        }

        public DbSet<Clothe.Models.User> User { get; set; } = default!;
        public DbSet<Clothe.Models.Product> Product { get; set; } = default!;
        public DbSet<Clothe.Models.Order> Order { get; set; } = default!;
        public DbSet<Clothe.Models.Feedback> Feedback { get; set; } = default!;
    }
}
