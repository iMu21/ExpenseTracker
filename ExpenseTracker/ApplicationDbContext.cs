using ExpenseTracker.Areas.Identity.Data;
using Microsoft.EntityFrameworkCore;
using ExpenseTracker.Models;

namespace ExpenseTracker
{
    public partial class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<ExpenseTrackerUser> Users { get; set; }

        public DbSet<ExpenseTracker.Models.Category> Category { get; set; }

        public DbSet<ExpenseTracker.Models.Expense> Expense { get; set; }
    }
}
