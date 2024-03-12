using Microsoft.EntityFrameworkCore;
using Task_Management.Models;

namespace Task_Management.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public DbSet<taskManagement> tasks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<taskManagement>().HasData(
               new taskManagement { TaskID = 1, Title = "Review code", Description = "Finish all outstanding tasks", Status = "In Progress", Position = 1, Column = "In Progress" },
               new taskManagement { TaskID = 2, Title = "Test functionality", Description = "Check code quality and address any issues", Status = "Completed", Position = 2, Column = "Completed" }
                );
        }

    }
}
