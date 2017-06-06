using VkNet.Enums.Filters;
using System.Windows.Forms;
using VkNet.Enums.SafetyEnums;
using VkNet.Model.RequestParams;
using System;
using System.Collections.Generic;
using Application = System.Windows.Forms.Application;

namespace VkApplication
{
    public partial class MainForm : Form
    {
        public long SelectedUserId { get; set; }
        public MainForm()
        {
            InitializeComponent();
        }
        //Получение общей информации о пользователе
        private void Form1_Load(object sender, EventArgs e)
        {
            lblFirstName.Text = Singlet.UserFirstName;
            lblLastName.Text = Singlet.UserLastName;
            GetFriends();
            GetDialogs();
        }
        //Получение списка друзей и добавление их в ListFriends
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
                ListFriends.Items.Add(friend.FirstName + " " + friend.LastName + " | " + friend.Id);
            }

        }
        //Получение истории сообщений пользователя     
        private void GetHistori(long userId)
        {
            HistoryMessageBox.Items.Clear();
            var getHistory = Singlet.Api.Messages.GetHistory(new MessagesGetHistoryParams()
            {
                Count = 100,
                UserId = userId
            });
            foreach (var message in getHistory.Messages)
            {
                HistoryMessageBox.Items.Add(message.Body);
            }
        }
        //Получения списка диалогов
        private void GetDialogs()
        {
            HistoryMessageBox.Items.Clear();
            var getDialogs = Singlet.Api.Messages.GetDialogs(new MessagesDialogsGetParams
            {
                Count = 30,

            });
            foreach (var dialog in getDialogs.Messages)
            {

                HistoryMessageBox.Items.Add(dialog.UserId + ":" + GetName(dialog.UserId.Value) + ":" + dialog.Body);
            }
        }
        //Функция получения имени и фамилии пользователя с определенным id
        private string GetName(long userId)
        {
            var user = Singlet.Api.Users.Get(userId);
            return user.FirstName + " " + user.LastName;
        }
        //Отправка сообщений выбранному пользователю
        private void btnSendMsg_Click(object sender, System.EventArgs e)
        {
            if (txtMsg.Text != "")
            {
                Singlet.Api.Messages.Send(new MessagesSendParams
                {
                    UserId = SelectedUserId,
                    Message = txtMsg.Text
                });
            }
            txtMsg.Text = "";
        }
        //Получение id выьранного пользователя в списке
        private void listFriends_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            SelectedUserId = long.Parse(ListFriends.Text.Substring(ListFriends.Text.LastIndexOf(" ")));
            GetHistori(SelectedUserId);
        }
        //получение списка сообщений для выбранного пользователя       

        private void btnExit_Click(object sender, System.EventArgs e)
        {
            Application.Exit();

        }

        private void HistoryMessageBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            long userId = long.Parse(HistoryMessageBox.Text.Substring(0, HistoryMessageBox.Text.IndexOf(":")));
            GetHistori(userId);
        }
        //Получение списка друзей
        private List<User> GetFriendsList()
        {
            var friends = Singlet.Api.Friends.Get(new FriendsGetParams
            {
                UserId = Singlet.UserId,
                Fields = ProfileFields.LastName,
                Order = FriendsOrder.Name,
            });
            List<User> listUsers = new List<User>();
            foreach (var friend in friends)
            {
                User user = new User
                {
                    FirstName = friend.FirstName,
                    LastName = friend.LastName,
                    UserId = friend.Id
                };
                listUsers.Add(user);
            }
            return listUsers;
        }
        //Добавляем список друзей в бд
        private void btnAdd_Click(object sender, EventArgs e)
        {
            List<User> listUsers = GetFriendsList();
            using (var context = new UsersContext())
            {
                foreach (var user in listUsers)
                {
                    context.Users.Add(user);
                    
                }
                context.SaveChanges();

            }

        }

    }
}
