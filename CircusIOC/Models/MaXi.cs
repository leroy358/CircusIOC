using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace CircusIOC.Models
{
    public class MaXiDbContext : DbContext
    {
         public DbSet<Admins> Admins { get; set; }
        public DbSet<Users> Users { get; set; }
        public DbSet<Pictures> Pictures { get; set; }
        public MaXiDbContext()
            : base("DefaultConnection")
        {
            Database.SetInitializer<MaXiDbContext>(null);
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<System.Data.Entity.ModelConfiguration.Conventions.PluralizingTableNameConvention>();
        }
    }
}