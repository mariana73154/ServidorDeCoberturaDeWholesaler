using Grpc.Core;
using GrpcServer;
using System.Data.SqlClient;
using System.Reflection.PortableExecutable;

namespace GrpcServer.Services
{
    
    public class LoginService : loginUser.loginUserBase
    {
        private readonly ILogger<LoginService> logger;

        //Object with conection to database
        static SqlConnection connection = new SqlConnection("Data Source=Mariana;Initial Catalog=DBGrupo8;Integrated Security=True");

        public LoginService(ILogger<LoginService> _logger)
        {
            logger = _logger;
        }

        //Create mutex to avoid concurrency
        static Mutex mutex = new Mutex();

        public override Task<LoginResult> loginUserOp(UserData userData, ServerCallContext context)
        {
            mutex.WaitOne();
            connection.Open();

            //Use database DBGrupo8
            string sqluse = "USE DBGrupo8";
            SqlCommand commanduse = new SqlCommand(sqluse, connection);
            commanduse.ExecuteNonQuery();

            string operadora = "";

            //Verify username and password in database
            string sqlUser = "SELECT Operadora FROM USEROPERADORA WHERE UserName = @Username AND Pass = @Password" ;
            SqlCommand command = new SqlCommand(sqlUser, connection);
            command.Parameters.AddWithValue("@Username", userData.UserName);
            command.Parameters.AddWithValue("@Password", userData.Password);
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                operadora = reader.GetString(0);
            }
            reader.Close();

            connection.Close();
            mutex.ReleaseMutex();

            if (operadora != "" )
            {
                Console.WriteLine("Login success");
                return Task.FromResult(new LoginResult
                {
                    Result = true,
                    Operadora = operadora.ToString(),
                    UserName = userData.UserName,
                });
            }
            else
            {
                Console.WriteLine("Login error");
                return Task.FromResult(new LoginResult {
                    Result = false,
                    Operadora = null,
                    UserName = userData.UserName,
                });
            }
        }
    }
}
