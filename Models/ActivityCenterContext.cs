using Microsoft.EntityFrameworkCore;

namespace ActivityCenter.Models
{
    public class ActivityCenterContext : DbContext
    {
        // base() calls the parent class' constructor passing the "options" parameter along
        public ActivityCenterContext(DbContextOptions<ActivityCenterContext> options) : base(options) { }
	
        public DbSet<User> Users { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<Participant> Participants { get; set; }

    }
}