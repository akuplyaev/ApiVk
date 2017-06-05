using System.IO;
using VkNet;
using VkNet.Enums.Filters;
using System.Windows.Forms;
using VkNet.Enums.SafetyEnums;
using VkNet.Model.RequestParams;

namespace VkApplication
{

    public partial class MainForm : Form
    {       
        long? currentUserId = 0;
        long? selectedUserId = 0;
        public MainForm()
        {
            InitializeComponent();
        }
        //Получение общей информации о пользователе
        private void Form1_Load(object sender, System.EventArgs e)
        {           
            currentUserId = Singlet.Api.UserId;            
            var currentUser = Singlet.Api.Users.Get((long)currentUserId, ProfileFields.All);
            txtFirstName.Text = currentUser.FirstName;
            txtLastName.Text = currentUser.LastName;
            GetFriends();

        }
        //Получение списка друзей
        private void GetFriends()
        {
            var friends = Singlet.Api.Friends.Get(new FriendsGetParams
            {
                UserId = 137280448,// UserId = _currentUserId
                Fields = ProfileFields.LastName,
                Order = FriendsOrder.Name,
            });

            foreach (var friend in friends)
            {
                listFriends.Items.Add(friend.FirstName + " " + friend.LastName + " | " + friend.Id);
            }
        }
        //Отправка сообщений выбранному пользователю
        private void btnSendMsg_Click(object sender, System.EventArgs e)
        {
            if (txtMsg.Text != "")
            {
               Singlet.Api.Messages.Send(new MessagesSendParams
                {
                    UserId = selectedUserId,
                    Message = txtMsg.Text
                });
            }
            txtMsg.Text = "";
        }
        //Получение id выьранного пользователя в списке
        private void listFriends_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            selectedUserId = long.Parse(listFriends.Text.Substring(listFriends.Text.LastIndexOf(" ")));
        }

        private void btnExit_Click(object sender, System.EventArgs e)
        {
            Application.Exit();

        }
    }
}
