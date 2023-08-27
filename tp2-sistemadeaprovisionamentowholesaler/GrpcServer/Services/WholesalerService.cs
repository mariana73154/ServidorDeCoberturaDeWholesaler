using Grpc.Core;
using Grpc.Net.Client;
using GrpcServer.Models;
using RabbitMQ.Client;
using System.Data.SqlClient;
using System.Text;

namespace GrpcServer.Services
{
    public class WholesalerService : wholesaler.wholesalerBase
    {
        private readonly ILogger<WholesalerService> logger;

        //Object with conection to database
        static SqlConnection connection = new SqlConnection("Data Source=Mariana;Initial Catalog=DBGrupo8;Integrated Security=True");

        //Create mutex
        static readonly Mutex mutex = new Mutex();

        private const string hostName = "localhost";

        private const string rabbitQueue = "EVENTS";

        public WholesalerService(ILogger<WholesalerService> _logger)
        {
            logger = _logger;
        }

        //#6----------------------------------------------------------
        public override Task<ReserveResult> Reserve(DomicilioWholesaler domicilio, ServerCallContext context)
        {
            var coberturas = GetCoberturasByNum(domicilio);

            var cob = coberturas.Coberturas.Where(x => x.Estado == "FREE" && x.NumAdmin == domicilio.NumAd);
            //if the list has a cobertura with the same numAdmin as the domicilio and the state is "FREE" then the domicilio is reserved
            if (cob.Any())
            {
                mutex.WaitOne();
                connection.Open();
                //for all the coberturas with the same numAdmin as the domicilio, the state is changed to "RESERVED" and the owner change to the domicilio owner
                foreach (var c in cob)
                {
                    string sqlUpdate = "UPDATE COBERTURA SET Estado = 'RESERVED', N_Owner = @nOwner , Modalidade = @Modalidade WHERE Num_admin = @numAdmin";
                    SqlCommand updateCmd = new SqlCommand(sqlUpdate, connection);
                    updateCmd.Parameters.AddWithValue("@nOwner", domicilio.Operadora);
                    updateCmd.Parameters.AddWithValue("@Modalidade", domicilio.Modalidade);
                    updateCmd.Parameters.AddWithValue("@numAdmin", c.NumAdmin);
                    updateCmd.ExecuteNonQuery();
                }
                connection.Close();
                mutex.ReleaseMutex();
                return Task.FromResult(new ReserveResult { Success = true });
            }

            else return Task.FromResult(new ReserveResult { Success = false });

        }

        public override async Task<ActivateResult> Activate(DomicilioWholesaler domicilio, ServerCallContext context)
        {
            var coberturas = GetCoberturasByNum(domicilio);

            var cob = coberturas.Coberturas.Where(x => x.Estado == "RESERVED" && x.NumAdmin == domicilio.NumAd && x.NOwner == domicilio.Operadora);

            //if the list has a cobertura with the same numAdmin as the domicilio, the state is "RESERVED" and the owner is the operadora of user then the domicilio is activated
            if (cob.Any())
            {
                mutex.WaitOne();
                connection.Open();

                //for all the coberturas with the same numAdmin as the domicilio, the state is changed to "ACTIVE"
                foreach (var c in cob)
                {
                    string sqlUpdate = "UPDATE COBERTURA SET Estado = 'ACTIVE' WHERE Num_admin = @numAdmin";
                    SqlCommand updateCmd = new SqlCommand(sqlUpdate, connection);
                    updateCmd.Parameters.AddWithValue("@numAdmin", c.NumAdmin);
                    updateCmd.ExecuteNonQuery();
                }

                connection.Close(); 
                mutex.ReleaseMutex();

                string message = domicilio.User.ToString() + "A cobertura no domicilio " + domicilio.NumAd + " foi ativada com sucesso!";
                //send message to the queue
                await Task.Run(()=> PublishToQueue(message));
                return new ActivateResult { Success = true , Time = "3 MIN"};
            }
            else
            {
                string message = domicilio.User.ToString() + "A cobertura no domicilio " + domicilio.NumAd + " não pode ser ativada!";
                await Task.Run(() => PublishToQueue(message));
                return new ActivateResult { Success = false, Time = "0 MIN" };
            }
        }

        public override async Task<ActivateResult> Deactivate(DomicilioWholesaler domicilio, ServerCallContext context)
        {
            var coberturas = GetCoberturasByNum(domicilio);

            var cob = coberturas.Coberturas.Where(x => x.Estado == "ACTIVE" && x.NumAdmin == domicilio.NumAd && x.NOwner == domicilio.Operadora);

            //if the list has a cobertura with the same numAdmin as the domicilio, the state is "ACTIVE" and the owner is the operadora of user then the domicilio is deactivated
            if (cob.Any())
            {
                mutex.WaitOne();
                connection.Open();

                //for all the coberturas with the same numAdmin as the domicilio, the state is changed to "DEACTIVATED" 
                foreach (var c in cob)
                {
                    string sqlUpdate = "UPDATE COBERTURA SET Estado = 'DEACTIVATED' WHERE Num_admin = @numAdmin";
                    SqlCommand updateCmd = new SqlCommand(sqlUpdate, connection);
                    updateCmd.Parameters.AddWithValue("@numAdmin", c.NumAdmin);
                    updateCmd.ExecuteNonQuery();
                }

                connection.Close();
                mutex.ReleaseMutex();
                string message = domicilio.User.ToString() + "A cobertura no domicilio " + domicilio.NumAd + " foi desativada com sucesso!";
                //send message to the queue
                await Task.Run(() => PublishToQueue(message));

                return new ActivateResult { Success = true, Time = "3 min" };
            }
            else
            {
                string message = domicilio.User.ToString() + "A cobertura no domicilio " + domicilio.NumAd + " não pode ser desativada!";
                await Task.Run(() => PublishToQueue(message));
                return new ActivateResult { Success = false, Time = "0 min" };
            }
        }

        public async override Task<ActivateResult> Terminate (DomicilioWholesaler domicilio, ServerCallContext context)  
        {
            var coberturas = GetCoberturasByNum(domicilio);

            var cob = coberturas.Coberturas.Where(x => x.Estado == "DEACTIVATED" && x.NumAdmin == domicilio.NumAd && x.NOwner == domicilio.Operadora);

            //if the list has a cobertura with the same numAdmin as the domicilio, the state is "DEACTIVATED" and the owner is the operadora of user then the domicilio is terminated
            if (cob.Any())
            {
                mutex.WaitOne();
                connection.Open();
                //for all the coberturas with the same numAdmin as the domicilio, the state is changed to "FREE" and the owner is changed
                foreach (var c in cob)
                {
                    string sqlUpdate = "UPDATE COBERTURA SET Estado = 'FREE', N_Owner = @owner, Modalidade = @mod WHERE Num_admin = @numAdmin";
                    SqlCommand updateCmd = new SqlCommand(sqlUpdate, connection);
                    updateCmd.Parameters.AddWithValue("@numAdmin", c.NumAdmin);
                    updateCmd.Parameters.AddWithValue("@owner", "");
                    updateCmd.Parameters.AddWithValue("@mod", "");
                    updateCmd.ExecuteNonQuery();
                }

                connection.Close();
                mutex.ReleaseMutex();
                string message = domicilio.User.ToString() + "A cobertura no domicilio " + domicilio.NumAd + " foi terminada com sucesso!";
                //send message to the queue
                await Task.Run(() => PublishToQueue(message));
                return new ActivateResult { Success = true, Time = "3 min" };
            }
            else
            {
                string message = domicilio.User.ToString() + "A cobertura no domicilio " + domicilio.NumAd + " não pode ser terminada!";
                await Task.Run(() => PublishToQueue(message));
                return new ActivateResult { Success = false, Time = "0 min" };
            }
        }

        public InfoCobertura GetCoberturasByNum (DomicilioWholesaler domicilio)
        {
            mutex.WaitOne();
            connection.Open();

            InfoCobertura coberturas = new InfoCobertura();

            string sqlCobertura = "SELECT Operadora, Num_admin, N_Owner, Estado, Modalidade FROM COBERTURA WHERE Num_admin = @numAdmin";
            SqlCommand coberturasCmd = new SqlCommand(sqlCobertura, connection);

            coberturasCmd.Parameters.AddWithValue("@numAdmin", domicilio.NumAd);
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
                coberturas.Coberturas.Add(cobertura);
            }

            coberturaReader.Close();
            connection.Close();
            mutex.ReleaseMutex();

            return coberturas;
        }

        //#7----------------------------------------------------------
        private async Task PublishToQueue(string message)
        {
            var factory = new ConnectionFactory()
            {
                UserName = "guest",
                Password = "guest",
                HostName = hostName,
                Port = 5672,
                VirtualHost = "/",
                AutomaticRecoveryEnabled = true,

            };

            using (var connectionRabbit = factory.CreateConnection())
            
                using (var channel = connectionRabbit.CreateModel())
                {
                    channel.QueueDeclare(queue: rabbitQueue,
                                                 durable: false,
                                                 exclusive: false,
                                                 autoDelete: false,
                                                 arguments: null);

                    await Task.Delay(10000);
                    //send message
                    var body = Encoding.UTF8.GetBytes(message);
                    channel.BasicPublish(exchange: "", routingKey: rabbitQueue, basicProperties: null, body: body);

                    //send empty message to the queue to signal the end of the message
                    var emptyBody = Encoding.UTF8.GetBytes("");
                    channel.BasicPublish(exchange: "", routingKey: rabbitQueue, basicProperties: null, body: emptyBody);
                }
        }
    }
}
