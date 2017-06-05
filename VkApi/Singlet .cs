using VkNet;
using VkNet.Enums.Filters;

namespace VkApplication
{

    //Класс-синглетон
    public class Singlet
    {
        public static VkApi Api { get; set; }
        private static Singlet _instance;        
        public static long UserId { get; set; }
        public static string UserFirstName { get; set; }      
        public static string UserLastName { get; set; }
        public static Singlet Instance
        {
            get
            {
                return _instance = _instance ?? new Singlet();
            }
        }
        private Singlet()
        {
        }
    }
}
