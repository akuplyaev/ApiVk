using System.Data.Entity;

namespace VkApplication
{
    public class UsersContext : DbContext
    {
        public UsersContext() : base("UsersContext")
        {

        }
        public DbSet<User> Users { get; set; }
    }

}
