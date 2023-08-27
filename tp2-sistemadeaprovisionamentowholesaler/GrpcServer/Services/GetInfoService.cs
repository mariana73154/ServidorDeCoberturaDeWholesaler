using GrpcServer;
using Grpc.Core;
using System.Data.SqlClient;
using System.Reflection;
using GrpcServer.Models;


namespace GrpcServer.Services
{
    public class GetInfoService : getInfo.getInfoBase
    {
        private readonly ILogger<GetInfoService> logger;
       

        //Object with conection to database
        static SqlConnection connection = new SqlConnection("Data Source=Mariana;Initial Catalog=DBGrupo8;Integrated Security=True");

        //Create mutex to avoid concurrency
        static Mutex mutex = new Mutex();

        public GetInfoService(ILogger<GetInfoService> _logger)
        {
            logger = _logger;
        }

        public override Task<InfoCobertura> GetInfoCobertura(TypeInfo typeInfo, ServerCallContext context)
        {
            mutex.WaitOne();
            connection.Open();

            InfoCobertura listCob = new InfoCobertura();

            string sqlCobertura = "SELECT Operadora, Num_admin, N_Owner, Estado, Modalidade FROM COBERTURA";
            SqlCommand coberturasCmd = new SqlCommand(sqlCobertura, connection);
            coberturasCmd.ExecuteNonQuery();

            SqlDataReader coberturaReader = coberturasCmd.ExecuteReader();
            while (coberturaReader.Read())
            {
                Cobertura cobertura = new Cobertura();
                cobertura.Operadora = (string)(coberturaReader[0]);
                cobertura.NumAdmin = (Int32)(coberturaReader[1]);
                cobertura.NOwner = (string)(coberturaReader[2]);
                cobertura.Estado = (string)(coberturaReader[3]);
                cobertura.Modalidade = (string)(coberturaReader[4]);
                listCob.Coberturas.Add(cobertura);
            }

            coberturaReader.Close();
            connection.Close();
            mutex.ReleaseMutex();

            return Task.FromResult(listCob);

        }

        public override Task<InfoDomicilio> GetInfoDomicilio(TypeInfo typeInfo, ServerCallContext context)
        {
            mutex.WaitOne();
            connection.Open();

            InfoDomicilio listDom = new InfoDomicilio();

            string sqlDomicilio = "SELECT * FROM DOMICILIO";
            SqlCommand domiciliosCmd = new SqlCommand(sqlDomicilio, connection);
            domiciliosCmd.ExecuteNonQuery();

            SqlDataReader domicilioReader = domiciliosCmd.ExecuteReader();
            while (domicilioReader.Read())
            {
                Domicilio dom = new Domicilio();
                dom.Municipio = (string)(domicilioReader[0]);
                dom.Localidade = (string)(domicilioReader[1]);
                dom.Rua = (string)(domicilioReader[2]);
                dom.NumPorta = (Int32)(domicilioReader[3]);
                dom.CodPostal = (string)(domicilioReader[4]);
                dom.NumAdmin = (Int32)(domicilioReader[5]);

                listDom.Domicilios.Add(dom);
            }

            domicilioReader.Close();
            connection.Close();
            mutex.ReleaseMutex();
            return Task.FromResult(listDom);

        }

        public override Task<InfoUser> GetInfoUser(TypeInfo typeInfo, ServerCallContext context)
        {
            mutex.WaitOne();
            connection.Open();

            InfoUser listUsers = new InfoUser();

            string sqlUsers = "SELECT UserName, Operadora FROM USEROPERADORA";
            SqlCommand userCmd = new SqlCommand(sqlUsers, connection);
            userCmd.ExecuteNonQuery();

            SqlDataReader userReader = userCmd.ExecuteReader();
            while (userReader.Read())
            {
                User user = new User();
                user.Username = (string)(userReader[0]);
                user.Operadora = (string)(userReader[1]);
                listUsers.Users.Add(user);
            }

            userReader.Close();
            connection.Close();
            mutex.ReleaseMutex();

            return Task.FromResult(listUsers);
        }

        public override Task<InfoOperadora> GetInfoOperadora(TypeInfo typeInfo, ServerCallContext context)
        {
            mutex.WaitOne();
            connection.Open();

            InfoOperadora listOp = new InfoOperadora();

            string sqlOp = "SELECT * FROM OPERADORA";
            SqlCommand opCmd = new SqlCommand(sqlOp, connection);
            opCmd.ExecuteNonQuery();

            SqlDataReader opReader = opCmd.ExecuteReader();
            while (opReader.Read())
            {
                Operadora op = new Operadora();
                op.Operadora_ = (string)(opReader[0]);
                listOp.Operadoras.Add(op);
            }

            opReader.Close();
            connection.Close();
            mutex.ReleaseMutex();

            return Task.FromResult(listOp);
        }

        public override Task<Result> RemoveCobertura(Cobertura cobertura, ServerCallContext context)
        {
            mutex.WaitOne();
            connection.Open();

            string sqlRemoveCobertura = "DELETE FROM COBERTURA WHERE Operadora = @Operadora AND Num_admin = @Num_admin AND N_Owner = @N_Owner AND Estado = @Estado AND Modalidade = @Modalidade";
            SqlCommand removeCoberturaCmd = new SqlCommand(sqlRemoveCobertura, connection);

            removeCoberturaCmd.Parameters.AddWithValue("@Operadora", cobertura.Operadora);
            removeCoberturaCmd.Parameters.AddWithValue("@Num_admin", cobertura.NumAdmin);
            removeCoberturaCmd.Parameters.AddWithValue("@N_Owner", cobertura.NOwner);
            removeCoberturaCmd.Parameters.AddWithValue("@Estado", cobertura.Estado);
            removeCoberturaCmd.Parameters.AddWithValue("@Modalidade", cobertura.Modalidade);
            removeCoberturaCmd.ExecuteNonQuery();

            connection.Close();
            mutex.ReleaseMutex();

            return Task.FromResult(new Result { Result_ = true });  
        }

        public override Task<Result> AddCobertura(Cobertura cobertura, ServerCallContext context)
        {
            mutex.WaitOne();
            connection.Open();

            string sqlAddCobertura = "INSERT INTO COBERTURA (Id_ficheiro, Operadora, Num_admin, N_Owner, Estado, Modalidade) VALUES (@Id_ficheiro, @Operadora, @Num_admin, @N_Owner, @Estado, @Modalidade)";
            SqlCommand addCoberturaCmd = new SqlCommand(sqlAddCobertura, connection);
            //We create a new file for coberturas added by admins (id_ficheiro = 103)
            addCoberturaCmd.Parameters.AddWithValue("@Id_ficheiro", 103);
            addCoberturaCmd.Parameters.AddWithValue("@Operadora", cobertura.Operadora);
            addCoberturaCmd.Parameters.AddWithValue("@Num_admin", cobertura.NumAdmin);
            addCoberturaCmd.Parameters.AddWithValue("@N_Owner", cobertura.NOwner);
            addCoberturaCmd.Parameters.AddWithValue("@Estado", cobertura.Estado);
            addCoberturaCmd.Parameters.AddWithValue("@Modalidade", cobertura.Modalidade);
            addCoberturaCmd.ExecuteNonQuery();
            connection.Close();
            mutex.ReleaseMutex();

            return Task.FromResult(new Result { Result_ = true });
        }

        public override Task<Result> AddUser(User user, ServerCallContext context)
        {
            mutex.WaitOne();
            connection.Open();

            string sqlAddUser = "INSERT INTO USEROPERADORA (UserName, Pass, Operadora) VALUES (@UserName, @Pass, @Operadora)";
            SqlCommand addUserCmd = new SqlCommand(sqlAddUser, connection);
            addUserCmd.Parameters.AddWithValue("@UserName", user.Username);
            addUserCmd.Parameters.AddWithValue("@Pass", user.Password);
            addUserCmd.Parameters.AddWithValue("@Operadora", user.Operadora);
            addUserCmd.ExecuteNonQuery();
            connection.Close();
            mutex.ReleaseMutex();
            return Task.FromResult(new Result { Result_ = true });
        }

        public override Task<Result> RemoveUser(User user, ServerCallContext context)
        {
            mutex.WaitOne();
            connection.Open();

            string sqlRemoveCobertura = "DELETE FROM USEROPERADORA WHERE UserName = @UserName AND Operadora = @Operadora";
            SqlCommand removeUserCmd = new SqlCommand(sqlRemoveCobertura, connection);

            removeUserCmd.Parameters.AddWithValue("@UserName", user.Username);
            removeUserCmd.Parameters.AddWithValue("@Operadora", user.Operadora);
            removeUserCmd.ExecuteNonQuery();

            connection.Close();
            mutex.ReleaseMutex();
            return Task.FromResult(new Result { Result_ = true });
        }
    }
}
