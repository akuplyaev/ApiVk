using System.Data.Entity;

namespace VkApplication
{
    class UsersContext:DbContext
    {
        public UsersContext():base ("UsersContext")
        {
            
        }
        public DbSet<User> Users { get; set; }
    }
}
