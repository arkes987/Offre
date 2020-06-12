using Microsoft.EntityFrameworkCore;
using Offre.Data.Models.User;

namespace Offre.Data
{
    public class OffreContext : DbContext
    {
        public DbSet<User> Users { get; set; }
    }
}
