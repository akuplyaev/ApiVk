using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VkApplication
{
    public class User
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long UserId { get; set; }
        public  string FirstName { get; set; }
        public  string LastName { get; set; }
    }
}
