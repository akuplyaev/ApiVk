using VkNet.Enums.Filters;
using System.Windows.Forms;
using VkNet.Enums.SafetyEnums;
using VkNet.Model.RequestParams;
using System;
namespace VkApplication
{

    public partial class MainForm : Form
    {        
        public long SelectedUserID { get; set; }
        public MainForm()
        {
            InitializeComponent();
        }
        //Получение общей информации о пользователе
        private void Form1_Load(object sender, System.EventArgs e)
        {                                            
            txtFirstName.Text = Singlet.UserFirstName;
            txtLastName.Text = Singlet.UserLastName;
            GetFriends();
          //  HistoryMessageBox.Items.Add(Singlet.Api.Messages.Get());

        }
        //Получение списка друзей
        private void GetFriends()
        {
            var friends = Singlet.Api.Friends.Get(new FriendsGetParams
            {
                UserId = Singlet.UserId,
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
                    UserId = SelectedUserID,
                    Message = txtMsg.Text
                });
            }
            txtMsg.Text = "";
        }
        //Получение id выьранного пользователя в списке
        private void listFriends_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            SelectedUserID = long.Parse(listFriends.Text.Substring(listFriends.Text.LastIndexOf(" ")));
            GetHistori();
        }
        //получение списка сообщений для выбранного пользователя
        private void GetHistori()
        {
            HistoryMessageBox.Items.Clear();
            var getHistory = Singlet.Api.Messages.GetHistory(new MessagesGetHistoryParams()
            {
                Count = 100,
                UserId = SelectedUserID
            });
            foreach (var message in getHistory.Messages)
            {
                HistoryMessageBox.Items.Add(message.Body);
            }
        }

        private void btnExit_Click(object sender, System.EventArgs e)
        {
            Application.Exit();

        }
    }
}
