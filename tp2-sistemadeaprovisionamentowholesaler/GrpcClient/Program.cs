using Grpc.Net.Client;
using GrpcServer;
using System.Security.Cryptography;
using System.Text;

namespace GrpcClient
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static async Task Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see http://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            Application.Run(new LoginPage());

            //var input = new HelloRequest { Name = "Tim" };

            //var channel = GrpcChannel.ForAddress("http://localhost:5253");
            //var client = new Greeter.GreeterClient(channel);

            //var reply = await client.SayHelloAsync(input);

            //Console.WriteLine(reply.Message);
        }

        
    }
}