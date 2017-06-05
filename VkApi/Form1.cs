using System.IO;
using System.Windows.Forms;
using VkNet.Enums.SafetyEnums;
using VkNet.Model.RequestParams;

namespace VkApi
{
    using VkNet;
    using VkNet.Enums.Filters;



    public partial class ApiApplication : Form
    {
        private long _currentUserId = 0;
        VkApi vk = new VkApi();        
        public ApiApplication()
        {
            InitializeComponent();
        }


        //Получение общей информации о пользователе
        private void Form1_Load(object sender, System.EventArgs e)
        {
            try
            {
                vk.Authorize(new ApiAuthParams
                {
                    ApplicationId = 6061372,
                    Login = File.ReadAllText(@"E:\MyProject\VkApi\VkApi\bin\Debug\login.txt"),
                    Password = File.ReadAllText(@"E:\MyProject\VkApi\VkApi\bin\Debug\password.txt"),
                    Settings = Settings.All
                });              
            }
            catch 
            {
                MessageBox.Show("Error");
            }
            _currentUserId =long.Parse(vk.UserId.ToString());
            var currentUser = vk.Users.Get(_currentUserId, ProfileFields.All);
            txtFirstName.Text = currentUser.FirstName;
            txtLastName.Text = currentUser.LastName;
            GetFriends();

        }
        //Получение списка друзей
        private void GetFriends()
        {
            var friends = vk.Friends.Get(new FriendsGetParams
            {
                UserId = 137280448,// UserId = _currentUserId
                Fields = ProfileFields.LastName,
                Order =  FriendsOrder.Name,
            });

            foreach (var friend in friends)
            {
                listFriends.Items.Add(friend.FirstName + " " + friend.LastName+" | "+friend.Id);
            }
        }
        //Отправка сообщений
        private void btnSendMsg_Click(object sender, System.EventArgs e)
        {
            if (txtMsg.Text != "")
            {
                vk.Messages.Send(new MessagesSendParams
                {
                    UserId = _currentUserId,
                    Message = txtMsg.Text
                });               
            }
            txtMsg.Text = "";
        }
    }
}
