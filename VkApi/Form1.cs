using System.Diagnostics.Eventing.Reader;
using System.Windows.Forms;
using VkNet.Enums.SafetyEnums;
using VkNet.Model.RequestParams;

namespace VkApi
{
    using VkNet;
    using VkNet.Enums.Filters;



    public partial class ApiApplication : Form
    {
        VkApi vk = new VkApi();       
        public ApiApplication()
        {
            InitializeComponent();
        }



        private void Form1_Load(object sender, System.EventArgs e)
        {
            
            Settings scope = Settings.Friends;
            vk.Authorize(new ApiAuthParams
            {
                ApplicationId = 6061372,
                Login = "89105741622",
                Password = "serega94",
                Settings = Settings.All
            });
            var CurrentUser = vk.Users.Get(vk.UserId.Value, ProfileFields.All);
            txtFirstName.Text = CurrentUser.FirstName;
            txtLastName.Text = CurrentUser.LastName;
        }

        private void btnListFriends_Click(object sender, System.EventArgs e)
        {
            var friends = vk.Friends.Get(new FriendsGetParams
            {
                UserId = 137280448,                
                Fields = ProfileFields.LastName,                
                Order = FriendsOrder.Name,
            });
           
            foreach (var friend  in friends)
            {
                listFriends.Items.Add(friend.FirstName+" "+ friend.LastName);
            }
            
        }
    }
}
