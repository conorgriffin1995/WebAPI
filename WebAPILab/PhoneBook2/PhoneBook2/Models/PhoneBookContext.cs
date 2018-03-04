using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace PhoneBook2.Models
{
    public class PhoneBookContext : DbContext
    {
        public PhoneBookContext() : base("DefaultConnection")
        {
            Database.SetInitializer<PhoneBookContext>(new CreateDatabaseIfNotExists<PhoneBookContext>());
        }
        public DbSet<PhoneBook> PhoneBooks { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }

    }
}