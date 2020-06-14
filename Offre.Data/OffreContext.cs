using Microsoft.EntityFrameworkCore;
using Offre.Data.Models.User;

namespace Offre.Data
{
    public interface IOffreContext
    {
        DbSet<User> Users { get; set; }
    }
    public class OffreContext : DbContext, IOffreContext
    {
        public OffreContext(DbContextOptions<OffreContext> options) : base(options)
        {

        }
        public DbSet<User> Users { get; set; }
    }
}
