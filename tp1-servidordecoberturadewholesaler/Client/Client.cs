using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Net;

namespace Client
{
    internal class Client
    {
        static void Main()
        {

            //Conect to server
            //Console.WriteLine("Inserir IP do servidor: ");
            //string ip = Console.ReadLine();

            //Para testes, usar o IP do servidor local
            IPAddress ip = IPAddress.Parse("127.0.0.1");

            TcpClient client = null;
            try
            {
                //Create a client
                client = new TcpClient();
                client.Connect(ip, 5000);

                Console.WriteLine("Conectado ao servidor");

                //Stream to send and receive data
                NetworkStream stream = client.GetStream();

                SendData(client, stream);
            }
            catch (Exception e)
            {
                Console.WriteLine("Error" + e);
            }
            finally
            {
                client.Close();
            }
        }

        //Receber dados do Servidor
        static void ReceiveData(TcpClient client, NetworkStream stream)
        {
            byte[] buffer = new byte[client.ReceiveBufferSize];
            //Receive data from server
            int bytesRead = stream.Read(buffer, 0, client.ReceiveBufferSize);

            if (bytesRead == 0)
            {
                client.Close();
            }
            else
            {
                string data = Encoding.ASCII.GetString(buffer);
                data = data.Replace("\0", "");
                Console.WriteLine(data);
            }
        }

        //Send data to server
        static void SendData(TcpClient client, NetworkStream stream)
        {
            while (true)
            {
                ReceiveData(client, stream);
                string option = "0";

                //Chose operator
                Console.WriteLine("Indicar a operadora: ");
                string operadora = Console.ReadLine();

                while(option != "X"){
                    //Menu
                    Console.WriteLine("1 - Enviar ficheiro");
                    if(operadora=="OWNER")
                    {
                        Console.WriteLine("2 - Consultar informação de coberturas");
                    }
                    Console.WriteLine("X - QUIT");
                    Console.WriteLine("Escolha uma opção: ");
                    option = Console.ReadLine();

                    switch (option)
                    {
                        case "1":
                            //Enviar ficheiro
                            SendFile(client, stream,operadora);
                            break;

                        case "2":
                            if(operadora == "OWNER")
                            {
                                //send request to server
                                stream.Write(Encoding.ASCII.GetBytes("GET COBERTURAS"), 0, 14);
                                ReceiveData(client, stream);
                            }
                            break;

                        default:
                            Console.WriteLine("Opção inválida");
                            break;
                    }
                }

                //Send QUIT to server
                stream.Write(Encoding.ASCII.GetBytes("QUIT"), 0, 4);
                //Close connection
                client.Close();
                Console.WriteLine("Conexão encerrada");

            }
        }

        //Parse file to json object
        static string ParseFile(string path, string operadora)
        {
            string content = File.ReadAllText(path);
            string fileextension = Path.GetExtension(path);
            string filename = Path.GetFileName(path);

            //Create json object
            Ficheiro file = new Ficheiro(filename, content, operadora, fileextension);
            string json = Newtonsoft.Json.JsonConvert.SerializeObject(file);

            return json;

        }

        //Send file to server
        static void SendFile(TcpClient client, NetworkStream stream, string operadora)
        {
            Console.WriteLine("O ficheiro a enviar deve ter o seguinte formato: \n Municipio(string);Localidade(string);Rua(string);Num de Porta(int);Codigo Postal(string);IsOwner(true/false)");
            Console.WriteLine("Indique o caminho do ficheiro: ");
            string path = Console.ReadLine();

            string streamData = ParseFile(path, operadora);

            //buffer with streamdata
            byte[] buffer = Encoding.ASCII.GetBytes(streamData);

            //Send parsed file to server
            stream.Write(buffer, 0, buffer.Length);
            ReceiveData(client, stream);
        }

    }
}

