using Microsoft.EntityFrameworkCore;
 
namespace wedding.Models
{
    public class MainContext : DbContext
    {
        // base() calls the parent class' constructor passing the "options" parameter along
        public MainContext(DbContextOptions<MainContext> options) : base(options) { }
        public DbSet<User> user { get; set;}

        public DbSet<WeddingPlanner> weddingplanner { get; set;}

        public  DbSet<RSVP> RSVP {get; set;}
    
    }
}