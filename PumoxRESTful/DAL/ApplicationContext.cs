using Microsoft.EntityFrameworkCore;
using PumoxRESTful.Models;

namespace PumoxRESTful.DAL
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
            
        }

        public DbSet<Company> Companys { get; set; }
    }
}
