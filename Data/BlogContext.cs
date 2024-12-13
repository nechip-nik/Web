using System.Collections.Generic;
using Kurs.Models;
using Microsoft.EntityFrameworkCore;
namespace Kurs.Data
{
    public class BlogContext : DbContext
    {
        public BlogContext(DbContextOptions<BlogContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Article> Articles { get; set; }


    }
}
