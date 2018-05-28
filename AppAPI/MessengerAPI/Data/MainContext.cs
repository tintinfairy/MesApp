using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace AppAPI.Models
{
    public class DataContext : DbContext
    {
        private static bool _created = false;
        public DataContext(DbContextOptions<DataContext> options)
            : base(options)
        {
            if (!_created)
            {
                Database.Migrate();
                _created = true;
            }
        }


        public DbSet<AppAPI.Models.Message> Message { get; set; }
        public DbSet<AppAPI.Models.UserContacts> UserContacts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserContacts>()
                .Property<string>("ExtendedDataStr")
                .HasField("_extendedData");
        }
    }
}
