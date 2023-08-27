using Grpc.Core;
using Grpc.Net.Client;
using GrpcServer;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.Design.AxImporter;

namespace GrpcClient
{
    public partial class ExternUserPage : Form
    {
        private const string hostName = "localhost";
        private const string rabbitQueue = "EVENTS";

        private string Operadora;
        private string User;

        private IConnection connectionRabbit;
        private IModel channel;
        //Create mutex
        private static Mutex mutex = new Mutex();

        public ExternUserPage(string operadora, string user)
        {
            InitializeComponent();

            this.Operadora = operadora;
            this.User = user;
            lbHello.Text = "Hello " + User + "!";

            LoadDomiciles(operadora);

            Task.Run(() => StartListening());
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            //close connection
            if (connectionRabbit != null && connectionRabbit.IsOpen)
                connectionRabbit.Close();
            if (channel != null && channel.IsOpen)
                channel.Close();
            base.OnClosing(e);
        }

        private void LoadDomiciles(string operadora)
        {
            coberturasList.Items.Clear();
            var domicilios = GetDomicilios().Domicilios;


            var coberturas = GetCoberturas().Coberturas.ToList().Where(x => x.Operadora == operadora);


            foreach (var cobertura in coberturas)
            {
                //get numadm
                var numAdm = cobertura.NumAdmin;

                //get domicilio
                var domicilio = domicilios.FirstOrDefault(x => x.NumAdmin == numAdm);

                if (domicilio != null)
                {
                    //add domicilio to list
                    string[] row = { domicilio.NumAdmin.ToString(), domicilio.Municipio, domicilio.Localidade, domicilio.Rua, domicilio.NumPorta.ToString(), domicilio.CodPostal };
                    //Create item
                    ListViewItem item = new ListViewItem(row[0]);

                    //Put info of domicilio in columns
                    item.SubItems.Add(row[1]);
                    item.SubItems.Add(row[2]);
                    item.SubItems.Add(row[3]);
                    item.SubItems.Add(row[4]);
                    item.SubItems.Add(row[5]);

                    //Add item to listview
                    coberturasList.Items.Add(item);
                }

            }

        }

        private InfoDomicilio GetDomicilios()
        {
            //Get domicilios
            var channel = GrpcChannel.ForAddress("http://25.35.32.233:7044");

            var load = new getInfo.getInfoClient(channel);

            var typeInfo = new TypeInfo { Tipo = "Domicilio" };

            var reply = load.GetInfoDomicilio(typeInfo);

            return reply;
        }

        private InfoCobertura GetCoberturas()
        {
            //Get coberturas
            var channel = GrpcChannel.ForAddress("http://25.35.32.233:7044");

            var load = new getInfo.getInfoClient(channel);

            var typeInfo = new TypeInfo { Tipo = "Cobertura" };

            var reply = load.GetInfoCobertura(typeInfo);

            return reply;
        }

        private void coberturasList_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            //if selected item is not null then enable reserve button
            if (coberturasList.SelectedItems.Count > 0)
            {
                //if coberturas with numAdmin selected is FREE then enable reserve button
                var coberturas = GetCoberturas().Coberturas;

                var cobs = coberturas.Where(x => x.NumAdmin == Int32.Parse(coberturasList.SelectedItems[0].SubItems[0].Text)).ToList();

                if (cobs.All(x => x.Estado == "FREE"))
                {
                    btnReserve.Enabled = true;
                    cbModalidade.Enabled = true;
                }
                else if (cobs.All(x => x.Estado == "RESERVED"))
                {
                    btnActivate.Enabled = true;
                }
                else if (cobs.All(x => x.Estado == "ACTIVE"))
                {
                    btnDeactivate.Enabled = true;
                }
                else if (cobs.All(x => x.Estado == "DEACTIVATED"))
                {
                    btnTermin.Enabled = true;
                }

            }
            else
            {
                cbModalidade.Enabled = false;
                btnReserve.Enabled = false;
                btnActivate.Enabled = false;
                btnDeactivate.Enabled = false;
                btnTermin.Enabled = false;
            }
        }

        //#6----------------------------------------------------------

        private void btnReserve_Click(object sender, EventArgs e)
        {
            //Get domicilio selected
            var domicilio = coberturasList.SelectedItems[0];

            //Get numAdmin of domicilio
            var numAdmin = Int32.Parse(domicilio.SubItems[0].Text);

            var channel = GrpcChannel.ForAddress("http://25.35.32.233:7044");

            var reserve = new wholesaler.wholesalerClient(channel);

            var domicilioReserve = new DomicilioWholesaler { Operadora = Operadora, NumAd = numAdmin, Modalidade = cbModalidade.Text };

            var reply = reserve.Reserve(domicilioReserve);

            if (reply.Success)
            {
                MessageBox.Show("Reserva efetuada com sucesso!");
                LoadDomiciles(Operadora);
            }
            else
            {
                MessageBox.Show("Reserva não efetuada!");
            }

        }

        private async void btnActivate_Click(object sender, EventArgs e)
        {
            var domicilio = coberturasList.SelectedItems[0];

            //Get numAdmin of domicilio
            var numAdmin = Int32.Parse(domicilio.SubItems[0].Text);

            var channel = GrpcChannel.ForAddress("http://25.35.32.233:7044");

            var activate = new wholesaler.wholesalerClient(channel);

            var domicilioActivate = new DomicilioWholesaler { Operadora = Operadora, NumAd = numAdmin, Modalidade = "", User = User };

            var reply = await activate.ActivateAsync(domicilioActivate);

        }

        private async void btnDeactivate_Click(object sender, EventArgs e)
        {
            var domicilio = coberturasList.SelectedItems[0];

            //Get numAdmin of domicilio
            var numAdmin = Int32.Parse(domicilio.SubItems[0].Text);

            var channel = GrpcChannel.ForAddress("http://25.35.32.233:7044");

            var activate = new wholesaler.wholesalerClient(channel);

            var domicilioActivate = new DomicilioWholesaler { Operadora = Operadora, NumAd = numAdmin, Modalidade = "", User = User };

            var reply = await activate.DeactivateAsync(domicilioActivate);

            await Task.Run(() => StartListening());
        }

        private async void btnTermin_Click(object sender, EventArgs e)
        {
            var domicilio = coberturasList.SelectedItems[0];

            //Get numAdmin of domicilio
            var numAdmin = Int32.Parse(domicilio.SubItems[0].Text);

            var channel = GrpcChannel.ForAddress("http://25.35.32.233:7044");

            var activate = new wholesaler.wholesalerClient(channel);

            var domicilioActivate = new DomicilioWholesaler { Operadora = Operadora, NumAd = numAdmin, Modalidade = "", User = User };

            var reply = await activate.TerminateAsync(domicilioActivate);

            await Task.Run(() => StartListening());
        }


        //#8----------------------------------------------------------
        private async Task StartListening()
        {
            var factory = new ConnectionFactory()
            {
                UserName = "guest",
                Password = "guest",
                Port = 5672,
                VirtualHost = "/",
                HostName = "localhost",
            };

            var connectionRabbit = factory.CreateConnection();

            var channel = connectionRabbit.CreateModel();

            channel.QueueDeclare(queue: rabbitQueue,
                                     durable: false,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);

            channel.BasicQos(prefetchSize: 0, prefetchCount: 1, global: false);

            await Task.Delay(10000);
            var consumer = new EventingBasicConsumer(channel);

            consumer.Received += (model, e) =>
            {
                var body = e.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                if (message != "")
                {
                    if (message.StartsWith(User))
                    {
                        //message = message without operadora
                        message = message.Substring(User.Length);
                        lbNotifications.Invoke((MethodInvoker)(() =>
                        {
                            lbNotifications.Items.Add(message);
                        }));
                        channel.BasicAck(deliveryTag: e.DeliveryTag, multiple: false);
                    }
                    else
                    {
                        //requeue the message
                        channel.BasicReject(deliveryTag: e.DeliveryTag, requeue: true);
                    }
                }
                else
                { channel.BasicReject(deliveryTag: e.DeliveryTag, requeue: false); }
            };

            string consumerTag = channel.BasicConsume(queue: rabbitQueue,
                   autoAck: false,
                   consumer: consumer);


        }

    }
}
