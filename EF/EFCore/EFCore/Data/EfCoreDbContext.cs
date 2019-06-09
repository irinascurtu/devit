using EFCore.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EFCore.Data
{
    public class EfCoreDbContext : DbContext
    {

        public EfCoreDbContext(DbContextOptions<EfCoreDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<Feedback> Feedbacks { get; set; }
    }

}
