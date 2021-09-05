using distribution_plaza.Models;
using Microsoft.EntityFrameworkCore;

namespace distribution_plaza.Data
{
    public class DataContext:DbContext
    {
        public DataContext(DbContextOptions<DataContext> _options):base(_options)
        {
            
        }

        public DbSet<Value> Values { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
