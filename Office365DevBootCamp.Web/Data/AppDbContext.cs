using Microsoft.EntityFrameworkCore;
using Office365DevBootCamp.Web.Models.Data;

namespace Office365DevBootCamp.Web.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }
        public DbSet<Attendee> Attendees { get; set; }
    }
}
