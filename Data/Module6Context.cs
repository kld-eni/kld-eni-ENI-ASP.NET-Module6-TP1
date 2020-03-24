using Module6.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Module6.Data
{
    public class Module6Context : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, please use data migrations.
        // For more information refer to the documentation:
        // http://msdn.microsoft.com/en-us/data/jj591621.aspx
    
        public Module6Context() : base("name=Module6Context")
        {
            this.Configuration.LazyLoadingEnabled = false;
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Samourai>().HasOptional(s => s.Arme).WithOptionalDependent();
            modelBuilder.Entity<Samourai>().HasMany(s => s.ArtsMartiaux).WithMany();
            base.OnModelCreating(modelBuilder);
        }

        public System.Data.Entity.DbSet<Module6.Models.Arme> Armes { get; set; }

        public System.Data.Entity.DbSet<Module6.Models.Samourai> Samourais { get; set; }

        public System.Data.Entity.DbSet<Module6.Models.ArtMartial> ArtMartiaux { get; set; }
    }
}
