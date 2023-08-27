using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Net.Sockets;
using System.Net;
using System.Text;
using Newtonsoft.Json;
using System.Data.SqlClient;
using CsvHelper;
using System.Globalization;
using CsvHelper.Configuration;
 
namespace Server
{
    /*public class LerMuni 
    {
        public string Name { get; set; }

        public LerMuni( string name)
        {
            Name = name;
        }
    }*/

    internal class Server
    {

        //Object with conection to database
        static SqlConnection connection = new SqlConnection("Data Source=Mariana;Initial Catalog=DBGrupo8;Integrated Security=True");

        //Create mutex
        static Mutex mutex = new Mutex(false, "process_file");

        static void Main()
        {
            TcpListener server = null;

            try
            {
                server = new TcpListener(IPAddress.Any, 5000);
                server.Start();
                Console.WriteLine("Servidor iniciado");

                while (true)
                {
                    //Acept client connection
                    TcpClient client = server.AcceptTcpClient();
                    Console.WriteLine("Cliente conectado.");

                    //Create stream to send and receive data
                    NetworkStream stream = client.GetStream();

                    //Send message "100 OK" to client
                    stream.Write(Encoding.ASCII.GetBytes("100 OK"), 0, 6);

                    //ReadMunicipios();
                    
                    //Create a thread to receive data from client
                    Thread clientThread = new Thread(() => ReceiveData(client,stream));
                    clientThread.Start();

                }

            }
            catch (Exception e)
            {
                Console.WriteLine("Error: "+ e);
            }
            finally
            {
                server.Stop();
  
            }
               
        }

        //Ler municipios do ficheiro municipios.csv e inseri los na base de dados
        /*static void ReadMunicipios()
        {
            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = false,
            };

            //Read file municipios.csv in folder Files
            using (var reader = new StreamReader(@"Files\municipios.csv"))
            using (var csv = new CsvReader(reader, config))
            {
                var records = csv.GetRecords<LerMuni>();
                foreach (var record in records)
                {
                    //Insert data into database
                    string sql = "INSERT INTO MUNICIPIO (Nome_municipio) VALUES (@Municipio)";
                    SqlCommand command = new SqlCommand(sql, connection);
                    command.Parameters.AddWithValue("@Municipio", record.Name);
                    command.ExecuteNonQuery();
                }
            }
        }*/

        //Open connection to database and use DBGrupo8
        static void OpenConnection()
        {
            connection.Open();

            //Use database DBGrupo8
            string sqluse = "USE DBGrupo8";
            SqlCommand commanduse = new SqlCommand(sqluse, connection);
            commanduse.ExecuteNonQuery();

        }

        //Receber dados do Cliente
        static void ReceiveData(TcpClient client, NetworkStream stream)
        {
            while(client.Connected)
            {
                //Buffer to receive data
                byte[] buffer = new byte[client.ReceiveBufferSize];
                //Receive data from client
                int bytesRead = stream.Read(buffer, 0, client.ReceiveBufferSize);

                if(bytesRead == 0)
                {
                    client.Close();
                    break;
                }
                else 
                {
                    //Convert buffer to string
                    string data = Encoding.ASCII.GetString(buffer);
                    //Remove null characters
                    data = data.Replace("\0", "");

                    //If data is "QUIT", close connection
                    if(data.ToUpper().Equals("QUIT"))
                    {
                        //Send message "400 BYE" to client
                        stream.Write(Encoding.ASCII.GetBytes("400 BYE"), 0, 7);
                        //Close connection
                        client.Close();
                        Console.WriteLine("Cliente Desconectado");
                        break;
                    }
                    //If data is "GET COBERTURAS" enviar informação das coberturas
                    else if(data.ToUpper().Equals("GET COBERTURAS"))
                    {
                        stream.Write(Encoding.ASCII.GetBytes(MunicipioClassification()),0,MunicipioClassification().Length);
                        Console.WriteLine("Informação enviada");
                    }
                    else
                    {
                        Ficheiro file = new Ficheiro();
                        try
                        {
                            //Desserialize data into a json object
                            file = JsonConvert.DeserializeObject<Ficheiro>(data);
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine("Error: " + e);
                        }

                        if(file!=null)
                        {
                            //Block other threads to process file
                            mutex.WaitOne();
                            //Open connection to database
                            OpenConnection();
                            //If database has any file with the same operator with state "OPEN" or "In_PROGRESS", send message "300 ERROR" to client
                            string sql = "SELECT * FROM FICHEIRO WHERE Operadora = @Operadora AND (Estado != 'COMPLETED')";
                            SqlCommand command = new SqlCommand(sql, connection);
                            command.Parameters.AddWithValue("@Operadora", file.Operadora);
                            SqlDataReader reader = command.ExecuteReader();
                            if (reader.HasRows)
                            {
                                //close reader
                                reader.Close();
                                //Send message "300 ERROR" to client
                                SendData("300 ERROR - Tente mais tarde", client, stream);
                                //Close connection to database
                                connection.Close();
                                //Release mutex
                                mutex.ReleaseMutex();
                            }
                            else
                            {
                                //close reader
                                reader.Close();
                                //Close connection to database
                                connection.Close();
                                FileProcessing(file, client, stream);
                                //Release mutex
                                mutex.ReleaseMutex();
                            }

                        }
                    }
                    
                }
            }
        }

        //Send data to client
        static void SendData(string data, TcpClient client, NetworkStream stream)
        {
            //Converter string para bytes
            byte[] buffer = Encoding.ASCII.GetBytes(data);
            //Send data to client
            stream.Write(buffer, 0, buffer.Length);
        }

        //Save file to folder "Files"
        static void SaveFileToFolder(Ficheiro file)
        {  
            File.WriteAllBytes("Files\\" + file.FileName, Encoding.ASCII.GetBytes(file.Content));
        }

        //Save file to database
        static void SaveFileToDatabase(Ficheiro file)
        {
            //Insert file to database
            string sql = "INSERT INTO FICHEIRO (Nome_fich, Operadora, Data_sub, Estado) VALUES (@FileName, @Operadora, @Data_sub, @Estado)";
            SqlCommand command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@FileName", file.FileName);
            command.Parameters.AddWithValue("@Operadora", file.Operadora);
            command.Parameters.AddWithValue("@Data_sub", System.DateTime.Now);
            command.Parameters.AddWithValue("@Estado", "OPEN");
            command.ExecuteNonQuery();
        }

        //Get id of file from database
        static int GetFileId(Ficheiro file)
        {
            int id= 0;
            string sql = "SELECT Id_ficheiro FROM FICHEIRO WHERE Nome_fich = @FileName";
            SqlCommand command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@FileName", file.FileName);
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                id = reader.GetInt32(0);
            }
            reader.Close();
            return id;
        }

        //Get operator of file
        static string GetOperadora(int id)
        {
            string operadora = "";
            string sql = "SELECT Operadora FROM FICHEIRO WHERE Id_ficheiro = @Id";
            SqlCommand command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@id", id);
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                operadora = reader.GetString(0);
            }
            reader.Close();
            return operadora;
        }

        //Read content of csv file and save it to database
        static void ReadContent(Ficheiro file)
        {
            //Open file to read content
            StreamReader reader = new StreamReader("Files\\" + file.FileName);

            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                Delimiter = ";",
                HasHeaderRecord = false,
            };

            var CoberturasFicheiro = new List<Cobertura>();

            using (var csv = new CsvReader(reader, config))
            {
                while (csv.Read())
                {
                    var Municipio = csv.GetField<string>(0);
                    var localidade = csv.GetField<string>(1);
                    var rua = csv.GetField<string>(2);
                    var numPorta = Convert.ToInt32(csv.GetField<string>(3));
                    var codPostal = csv.GetField<string>(4);
                    var owner = csv.GetField<string>(5);

                    var cobertura = new Cobertura(file, Municipio, localidade, rua, numPorta, codPostal, owner);
                    CoberturasFicheiro.Add(cobertura);
                }
            };

            //close reader
            reader.Close();

            //Change state of file to "IN_PROGRESS"
            string sqlUpdateState = "UPDATE FICHEIRO SET Estado = @Estado WHERE Id_ficheiro = @Id_ficheiro";
            SqlCommand command = new SqlCommand(sqlUpdateState, connection);
            command.Parameters.AddWithValue("@Estado", "IN_PROGRESS");
            command.Parameters.AddWithValue("@Id_ficheiro", GetFileId(file));
            command.ExecuteNonQuery();

            //Save all content about the operator from database to a list
            List<Cobertura> CoberturasDB = new List<Cobertura>();
            string sqlCoberturaDB = "  SELECT Municipio, Localidade, Rua,Num_porta, Cod_postal, N_Owner FROM COBERTURA c , FICHEIRO f Where c.Id_ficheiro = f.Id_ficheiro AND f.Operadora  = @Operadora";
            SqlCommand commandCoberturaDB = new SqlCommand(sqlCoberturaDB, connection);
            commandCoberturaDB.Parameters.AddWithValue("@Operadora", file.Operadora);
            commandCoberturaDB.ExecuteNonQuery();
            SqlDataReader readerCoberturaDB = commandCoberturaDB.ExecuteReader();


            while (readerCoberturaDB.Read())
            {
                var Municipio = readerCoberturaDB[0];
                var localidade = readerCoberturaDB[1];
                var rua = readerCoberturaDB[2];
                var numPorta = readerCoberturaDB[3];
                var codPostal = readerCoberturaDB[4];
                var Owner = readerCoberturaDB[5];
                string is_owner;
                if(Owner.ToString().Equals(file.Operadora))
                {
                    is_owner = "true";
                }
                else
                {
                    is_owner = "false";
                }
                var cobertura = new Cobertura(file, Convert.ToString(Municipio) , Convert.ToString(localidade), Convert.ToString(rua), Convert.ToInt32(numPorta), Convert.ToString(codPostal), is_owner);
                CoberturasDB.Add(cobertura);
            }

            //Close reader
            readerCoberturaDB.Close();

            //If the cobertura is in the file and not in the database, add it to the database
            foreach (var coberturaf in CoberturasFicheiro)
            {
                string owner; 
                //Get Owner if the owner if the owner of cobertura is false
                if (coberturaf.Owner.Equals("false"))
                {
                    owner = GetOwner(coberturaf);
                }
                else
                {
                    owner = GetOperadora(GetFileId(file));

                    //In this case search for cobertura of other operators in the database and change the owner to the operator of the file
                    string sqlUpdateOwner = "UPDATE COBERTURA SET N_Owner = @Owner WHERE Municipio = @Municipio AND Localidade = @Localidade AND Rua = @Rua AND Num_porta = @Num_porta AND Cod_postal = @Cod_postal";
                    SqlCommand commandUpdateOwner = new SqlCommand(sqlUpdateOwner, connection);
                    commandUpdateOwner.Parameters.AddWithValue("@Owner", owner);
                    commandUpdateOwner.Parameters.AddWithValue("@Municipio", coberturaf.Municipio);
                    commandUpdateOwner.Parameters.AddWithValue("@Localidade", coberturaf.Localidade);
                    commandUpdateOwner.Parameters.AddWithValue("@Rua", coberturaf.Rua);
                    commandUpdateOwner.Parameters.AddWithValue("@Num_porta", coberturaf.NumPorta);
                    commandUpdateOwner.Parameters.AddWithValue("@Cod_postal", coberturaf.CodPostal);
                    commandUpdateOwner.ExecuteNonQuery();

                }

                if (CoberturasDB.Find(x => x.Municipio == coberturaf.Municipio && x.Localidade == coberturaf.Localidade && x.Rua == coberturaf.Rua && x.NumPorta == coberturaf.NumPorta && x.CodPostal == coberturaf.CodPostal) == null)
                {
                    string sqlCobertura = "INSERT INTO COBERTURA (Municipio,Localidade,Rua,Num_porta, Cod_postal, Id_ficheiro,N_Owner) VALUES (@Municipio, @Localidade, @Rua, @Num_porta, @Cod_postal, @Id_ficheiro, @Owner)";
                    SqlCommand commandCobertura = new SqlCommand(sqlCobertura, connection);
                    commandCobertura.Parameters.AddWithValue("@Municipio", coberturaf.Municipio);
                    commandCobertura.Parameters.AddWithValue("@Localidade", coberturaf.Localidade);
                    commandCobertura.Parameters.AddWithValue("@Rua", coberturaf.Rua);
                    commandCobertura.Parameters.AddWithValue("@Num_porta", coberturaf.NumPorta);
                    commandCobertura.Parameters.AddWithValue("@Cod_postal", coberturaf.CodPostal);
                    commandCobertura.Parameters.AddWithValue("@Id_ficheiro", GetFileId(file));
                    commandCobertura.Parameters.AddWithValue("@Owner", owner);
                    commandCobertura.ExecuteNonQuery();  
                    CoberturasDB.Add(coberturaf);                  
                }

            }

            //If the cobertura is in the database and not in the file, delete it from the database
            foreach (var coberturadb in CoberturasDB)
            {
                if (CoberturasFicheiro.Find(x => x.Municipio == coberturadb.Municipio && x.Localidade == coberturadb.Localidade && x.Rua == coberturadb.Rua && x.NumPorta == coberturadb.NumPorta && x.CodPostal == coberturadb.CodPostal) == null)
                {
                    string sqlCobertura = "DELETE FROM COBERTURA WHERE Municipio = @Municipio AND Localidade = @Localidade AND Rua = @Rua AND Num_porta = @Num_porta AND Cod_postal = @Cod_postal AND Id_ficheiro != @Id_ficheiro";
                    SqlCommand commandCobertura = new SqlCommand(sqlCobertura, connection);
                    commandCobertura.Parameters.AddWithValue("@Municipio", coberturadb.Municipio);
                    commandCobertura.Parameters.AddWithValue("@Localidade", coberturadb.Localidade);
                    commandCobertura.Parameters.AddWithValue("@Rua", coberturadb.Rua);
                    commandCobertura.Parameters.AddWithValue("@Num_porta", coberturadb.NumPorta);
                    commandCobertura.Parameters.AddWithValue("@Cod_postal", coberturadb.CodPostal);
                    commandCobertura.Parameters.AddWithValue("@Id_ficheiro", GetFileId(file));
                    commandCobertura.ExecuteNonQuery();

                }
            }

            //Change state of file to "COMPLETED"
            string sqlUpdateCompleted = "UPDATE FICHEIRO SET Estado = @Estadonovo WHERE Id_ficheiro = @Id_ficheironovo";
            SqlCommand commandsqlUpdateC = new SqlCommand(sqlUpdateCompleted, connection);
            commandsqlUpdateC.Parameters.AddWithValue("@Estadonovo", "COMPLETED");
            commandsqlUpdateC.Parameters.AddWithValue("@Id_ficheironovo", GetFileId(file));
            commandsqlUpdateC.ExecuteNonQuery();
        }

        static string GetOwner(Cobertura cobertura)
        {
            string SqlOwner = "SELECT N_Owner FROM COBERTURA WHERE Municipio = @Municipio AND Localidade = @Localidade AND Rua = @Rua AND Num_porta = @Num_porta AND Cod_postal = @Cod_postal";
            SqlCommand commandOwner = new SqlCommand(SqlOwner, connection);
            commandOwner.Parameters.AddWithValue("@Municipio", cobertura.Municipio);
            commandOwner.Parameters.AddWithValue("@Localidade", cobertura.Localidade);
            commandOwner.Parameters.AddWithValue("@Rua", cobertura.Rua);
            commandOwner.Parameters.AddWithValue("@Num_porta", cobertura.NumPorta);
            commandOwner.Parameters.AddWithValue("@Cod_postal", cobertura.CodPostal);
            commandOwner.ExecuteNonQuery();
            SqlDataReader readerOwner = commandOwner.ExecuteReader();
            string owner = "N/D";
            while (readerOwner.Read())
            {
                owner = readerOwner[0].ToString();
            }
            readerOwner.Close();
            return owner;
        }

        //Municipio classification
        static string MunicipioClassification()
        {
            OpenConnection();
            string Info = "";
            //Get all coberturas from the database
            string sqlCobertura = "SELECT f.Operadora, c.Municipio, c.Localidade, c.Rua, c.Num_porta, c.Cod_postal, c.N_Owner FROM FICHEIRO f, COBERTURA c WHERE c.Id_ficheiro=f.Id_ficheiro";
            SqlCommand commandCobertura = new SqlCommand(sqlCobertura, connection);
            commandCobertura.ExecuteNonQuery();
            SqlDataReader readerCobertura = commandCobertura.ExecuteReader();
            List<Cobertura> Coberturas = new List<Cobertura>();
            while (readerCobertura.Read())
            {
                var operadora = readerCobertura[0];
                var Municipio = readerCobertura[1];
                var localidade = readerCobertura[2];
                var rua = readerCobertura[3];
                var numPorta = readerCobertura[4];
                var codPostal = readerCobertura[5];
                var Owner = readerCobertura[6];

                var cobertura = new Cobertura(Convert.ToString(operadora), Convert.ToString(Municipio), Convert.ToString(localidade), Convert.ToString(rua), Convert.ToInt32(numPorta), Convert.ToString(codPostal), Convert.ToString(Owner) );
                Coberturas.Add(cobertura);
            }
            readerCobertura.Close();
            
            
            //Create a list of coberturas with the same municipio, localidade, rua, num_porta and cod_postal without repeat 
            List<Cobertura> CoberturasFiltradas = new List<Cobertura>();
            
            foreach (var cobertura in Coberturas)
            {
                if (CoberturasFiltradas.Find(x => x.Municipio == cobertura.Municipio && x.Localidade == cobertura.Localidade && x.Rua == cobertura.Rua && x.NumPorta == cobertura.NumPorta && x.CodPostal == cobertura.CodPostal) == null)
                {
                    CoberturasFiltradas.Add(cobertura);
                }
            }

            //Get all municipios from the database
            List<Municipio> Municipios = GetMunicipios();

            foreach (var municipio in Municipios)
            {
                //Count each time municipio is in coberturas filtradas
                municipio.MunicipioClassification = CoberturasFiltradas.FindAll(x => x.Municipio == municipio.Name).Count;

                if(municipio.MunicipioClassification > 0)
                {
                    Info = Info + municipio.Name + " Classificação - " + municipio.MunicipioClassification + "\n";
                    //Console.WriteLine(municipio.Name);
                    //Console.WriteLine("Classificação - " + municipio.MunicipioClassification);
                }


                //For each cobertura filtrada use only the ones who has the same municipio
                foreach (var coberturaf in CoberturasFiltradas)
                {
                    if(coberturaf.Municipio == municipio.Name)
                    {
                        municipio.Sobreposicao+=Coberturas.FindAll(x => x.Municipio == coberturaf.Municipio && x.Localidade == coberturaf.Localidade && x.Rua == coberturaf.Rua && x.NumPorta == coberturaf.NumPorta && x.CodPostal == coberturaf.CodPostal).Count;
                        //if cobertura is in coberturas more than once increment sobreposicoes
                        if(municipio.MunicipioClassification > 0)
                        {
                            Info = Info + coberturaf.Localidade + ", " + coberturaf.Rua + ", " + coberturaf.NumPorta + ", " + coberturaf.CodPostal + " - " + GetOwner(coberturaf) + " - Operadoras: " ;
                            //Console.WriteLine(coberturaf.Localidade + ", " + coberturaf.Rua + ", " + coberturaf.NumPorta + ", " + coberturaf.CodPostal + " - " + GetOwner(coberturaf));
                            //Console.WriteLine("Operadoras: " );
                            //Display all operators who has the same cobertura
                            var cobOps = Coberturas.FindAll(x => x.Municipio == coberturaf.Municipio && x.Localidade == coberturaf.Localidade && x.Rua == coberturaf.Rua && x.NumPorta == coberturaf.NumPorta && x.CodPostal == coberturaf.CodPostal);
                            foreach(var cob in cobOps)
                            {
                                Info=Info+cob.Operadora + " ";
                                //Console.WriteLine(cob.Operadora);
                            }
                            Info=Info+"\n";
                        }

                    }
                } 

                if(municipio.Sobreposicao > 0)
                {
                    Info=Info+"Sobreposição - " + municipio.Sobreposicao +"\n\n";
                    //Console.WriteLine("Sobreposição - " + municipio.Sobreposicao);
                }                
                    
 
            }
            //Close connection
            connection.Close();
            return Info;
        }

        //Get municipios from database
        static List<Municipio> GetMunicipios()
        {
            string sqlMunicipio = "SELECT * FROM MUNICIPIO";
            SqlCommand commandMunicipio = new SqlCommand(sqlMunicipio, connection);
            commandMunicipio.ExecuteNonQuery();
            SqlDataReader readerMunicipio = commandMunicipio.ExecuteReader();
            List<Municipio> Municipios = new List<Municipio>();
            while (readerMunicipio.Read())
            {
                var nome = readerMunicipio[0];
                var municipio = new Municipio(Convert.ToString(nome));
                Municipios.Add(municipio);
            }
            readerMunicipio.Close();
            return Municipios;
        }

        //Process file received from client
        static void FileProcessing(Ficheiro file, TcpClient client, NetworkStream stream)
        {
                                
            //Open connection to database
            OpenConnection();

            SaveFileToFolder(file);
            SaveFileToDatabase(file);
            ReadContent(file);
            //string data = MunicipioClassification();
            SendData("O seu ficheiro foi processado", client, stream);

            //close connection to database
            connection.Close();


        }

    }
}

