using Microsoft.EntityFrameworkCore;
using Offre.Data.Models.User;

namespace Offre.Data
{
    public class OffreContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //Server=localhost;Database=master;Trusted_Connection=True;
            optionsBuilder.UseSqlServer(
                @"Server=localhost;Database=Offre;Trusted_Connection=True;");
        }
    }
}
