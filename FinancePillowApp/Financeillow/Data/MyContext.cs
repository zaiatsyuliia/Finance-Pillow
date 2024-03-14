using Financeillow.Presentation.Models;
using Microsoft.EntityFrameworkCore;
using System.Xml;

namespace Financeillow.Data
{
    public class MyContext : DbContext
    {
        public MyContext(DbContextOptions<MyContext> options) 
            : base(options) { }

        public DbSet<Users> Users { get; set; }
    }
}
