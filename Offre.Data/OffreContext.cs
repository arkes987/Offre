using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Offre.Data.Models.User;

namespace Offre.Data
{
    public interface IOffreContext
    {
        DbSet<User> Users { get; set; }

        void SaveChanges();

        public IDbContextTransaction BeginTransaction();


    }
    public class OffreContext : DbContext, IOffreContext
    {
        public OffreContext(DbContextOptions<OffreContext> options) : base(options)
        {

        }
        public DbSet<User> Users { get; set; }
        public new void SaveChanges()
        {
            base.SaveChanges();
        }

        IDbContextTransaction IOffreContext.BeginTransaction()
        {
            return base.Database.BeginTransaction();
        }
    }
}
