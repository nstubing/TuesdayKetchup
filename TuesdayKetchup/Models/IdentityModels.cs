using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace TuesdayKetchup.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
        public DbSet<Comment> comments { get; set; }
        public DbSet<Post> posts { get; set; }
        public DbSet<Episode> episodes { get; set; }
        public DbSet<Show> shows { get; set; }
        public DbSet<Thread> threads { get; set; }
        public DbSet<PostFlag> postFlags { get; set; }
        public DbSet<CommentFlag> commentFlags { get; set; }
        public DbSet<Event> events { get; set; }
        public DbSet<TextAlert> textAlerts { get; set; }
        public DbSet<Texts> texts { get; set; }
        public DbSet<Rating> ratings{ get; set; }
    }
}