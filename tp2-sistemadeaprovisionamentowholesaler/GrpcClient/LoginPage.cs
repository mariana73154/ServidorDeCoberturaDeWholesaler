using Grpc.Net.Client;
using GrpcServer;
using System.Security.Cryptography;
using System.Text;

namespace GrpcClient
{
    public partial class LoginPage : Form
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        private void LoginBtn_Click(object sender, EventArgs e)
        {
            //Create channel
            var channel = GrpcChannel.ForAddress("http://25.35.32.233:7044");

            var log = new loginUser.loginUserClient(channel);

            var userData = new UserData { UserName = Username.Text, Password = Password.Text /*HashPassword(Password.Text)*/};
            var reply = log.loginUserOp(userData);

            if (reply != null )
            {
                if (reply.Operadora == "OWNER")
                {
                    this.Hide();
                    //Open new window
                    AdminPage Administrador = new AdminPage(reply.UserName);
                    Administrador.ShowDialog();


                } else
                {
                    this.Hide();
                    //Open new widnow
                    ExternUserPage UserExterno = new ExternUserPage(reply.Operadora, reply.UserName);
                    UserExterno.ShowDialog();

                }
            }
            else { this.Close(); }
        }

        private string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                var hash = BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
                return hash;
            }
        }
    }
}