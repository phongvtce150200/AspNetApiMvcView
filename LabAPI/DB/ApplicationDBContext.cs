using Microsoft.EntityFrameworkCore;
using LabAPI.Models;

namespace LabAPI.DB
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions options) : base(options)
        {
            
        }

        public DbSet<Product> products  { get; set; }
    }
}