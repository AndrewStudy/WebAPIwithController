using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

namespace WebApplication1.Data
{
    public class IssueContext : DbContext
    {
        public DbSet<Issue> Issues {  get; set; }

        public IssueContext(DbContextOptions<IssueContext> options) : base(options)
        {
            
        }
    }
}
