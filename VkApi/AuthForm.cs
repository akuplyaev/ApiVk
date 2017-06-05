using System;
using System.IO;
using System.Windows.Forms;
using VkNet;
using VkNet.Enums.Filters;



namespace VkApplication
{
    
    public partial class AuthForm : Form
    {
              
        public string Login
        {
            get { return txtLogin.Text; }
        }
        public string Password
        {
            get { return txtPassword.Text; }
        }
        public AuthForm()
        {
            InitializeComponent();
        }

        private void btnEnter_Click(object sender, EventArgs e)
        {
            Singlet.Api = new VkApi();
            try
            {
                Singlet.Api.Authorize(new ApiAuthParams
                {
                    ApplicationId = 6061372,
                    Login = File.ReadAllText(@"E:\MyProject\VkApi\VkApi\bin\Debug\login.txt"),
                    //Login = Login,
                    Password = File.ReadAllText(@"E:\MyProject\VkApi\VkApi\bin\Debug\password.txt"), 
                    //Password = Password,
                    Settings = Settings.All
                });
                Singlet.UserId = Singlet.Api.UserId.Value;
                Singlet.UserFirstName = Singlet.Api.Users.Get(Singlet.UserId, ProfileFields.All).FirstName;
                Singlet.UserLastName = Singlet.Api.Users.Get(Singlet.UserId, ProfileFields.All).LastName;
                Hide();
                MainForm form = new MainForm();               
                form.Show();

            }
            catch
            {
                MessageBox.Show(@"Неверный логин или пароль");
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void AuthForm_Load(object sender, EventArgs e)
        {

        }
    }
}
