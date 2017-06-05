using VkNet;
namespace VkApplication
{

    //Класс-синглетон
    public  class Singlet
    {
        public static VkApi Api { get; set; }
        private static Singlet instance;
        public static Singlet Instance
        {
            get
            {                
            return    instance =  instance ?? new Singlet();
            }           
        }
        private Singlet()
        {
        }
    }
}
