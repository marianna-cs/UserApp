using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using UserApp.Data;

namespace UserApp.Repo
{
    public class UserAppContext : DbContext
    {
        public UserAppContext(DbContextOptions<UserAppContext> options) : base(options)
        {
            
        }

        public DbSet<User> Users { get; set; }
    }
}
