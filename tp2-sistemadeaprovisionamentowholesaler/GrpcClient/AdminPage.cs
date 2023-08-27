using Google.Protobuf.Collections;
using Grpc.Net.Client;
using GrpcServer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GrpcClient
{
    public partial class AdminPage : Form
    {
        private string Username;
        public AdminPage(string Username)
        {
            this.Username = Username;
            InitializeComponent();
            LoadData();
        }

        private void LoadData()
        {
            cbOptions.SelectedItem = "All";
            //Load users
            LoadUsers();
            //Load domicilios
            LoadDomiciles();
            //Load operators
            LoadOperators();

        }

        private void LoadDomiciles()
        {
            coberturasList.Items.Clear();
            var domicilios = GetDomicilios().Domicilios;

            foreach (Domicilio domicilio in domicilios)
            {
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

        private void LoadUsers()
        {
            usersList.Items.Clear();
            var users = GetUsers().Users;

            foreach (User user in users)
            {
                string[] row = { user.Username, user.Operadora };

                //Create item
                ListViewItem item = new ListViewItem(row[0]);

                //Put info of domicilio in columns
                item.SubItems.Add(row[1]);

                //Add item to listview
                usersList.Items.Add(item);
            }
        }

        private void LoadOperators()
        {
            cbOperator.Items.Clear();
            cbOperatorAdd.Items.Clear();

            var operators = GetOperators().Operadoras;

            foreach (Operadora operadora in operators)
            {
                if (operadora.Operadora_ != "N/D")
                {
                    //Add operadora to combobox
                    cbOperator.Items.Add(operadora.Operadora_);
                    cbOperatorAdd.Items.Add(operadora.Operadora_);
                }
            }
        }

        private InfoOperadora GetOperators()
        {
            //Get operators
            var channel = GrpcChannel.ForAddress("http://25.35.32.233:7044");

            var load = new getInfo.getInfoClient(channel);

            var typeInfo = new TypeInfo { Tipo = "Operadora" };

            var reply = load.GetInfoOperadora(typeInfo);

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

        private InfoDomicilio GetDomicilios()
        {
            //Get domicilios
            var channel = GrpcChannel.ForAddress("http://25.35.32.233:7044");

            var load = new getInfo.getInfoClient(channel);

            var typeInfo = new TypeInfo { Tipo = "Domicilio" };

            var reply = load.GetInfoDomicilio(typeInfo);

            return reply;
        }

        private InfoUser GetUsers()
        {
            //Get users
            var channel = GrpcChannel.ForAddress("http://25.35.32.233:7044");

            var load = new getInfo.getInfoClient(channel);

            var typeInfo = new TypeInfo { Tipo = "User" };

            var reply = load.GetInfoUser(typeInfo);

            return reply;
        }

        private void coberturasList_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            //CLEAR OPERADORA LIST
            operadoraList.Items.Clear();
            btnRemove.Enabled = false;
            btnAddOperator.Enabled = false;
            //Get selected item
            var item = e.Item;
            //Get selected item number of admin
            var numAdmin = item.SubItems[0].Text;

            //Serarch for coberturas with that numAdmin
            var coberturas = GetCoberturas().Coberturas;

            foreach (var c in coberturas)
            {
                if (c.NumAdmin == Int32.Parse(numAdmin))
                {
                    string op = c.Operadora.ToString();
                    if (!operadoraList.Items.ContainsKey(op))
                    {
                        ListViewItem operadora = new ListViewItem(op);

                        //If operadora is the OWNER and the state is not FREE change the text to bold
                        if (c.Estado != "FREE" && op == c.NOwner)
                        {
                            operadora.Font = new Font(operadora.Font, FontStyle.Bold);
                        }

                        operadoraList.Items.Add(operadora);

                    }
                }
            }
        }

        private void operadoraList_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            btnRemove.Enabled = true;
        }

        private void tbOperatorInsert_TextChanged(object sender, EventArgs e)
        {
            btnAddOperator.Enabled = true;
        }

        private void ListOfOptions_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Filter the list of domicilios by the selected option
            var selected = cbOptions.SelectedItem.ToString();
            coberturasList.Items.Clear();

            var coberturas = GetCoberturas().Coberturas;

            var domicilios = GetDomicilios().Domicilios;

            if (selected != "All")
            {
                foreach (var dom in domicilios)
                {
                    //verify if the domicilio already is in the list
                    if (coberturasList.FindItemWithText(dom.NumAdmin.ToString()) == null)
                    {

                        //if the selected option is equal to the state of the cobertura with the same numAdmin as the domicilio show it
                        if (coberturas.FirstOrDefault(c => c.NumAdmin == dom.NumAdmin && c.Estado == selected) != null)
                        {
                            string[] row = { dom.NumAdmin.ToString(), dom.Municipio, dom.Localidade, dom.Rua, dom.NumPorta.ToString(), dom.CodPostal };
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
            }
            else { LoadDomiciles(); }

        }

        private void usersList_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            if (usersList.SelectedItems.Count == 0)
            {
                btnRemoveUser.Enabled = false;
            }
            else
            {
                btnRemoveUser.Enabled = true;
            }

        }

        private void cbOperator_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if the selected item is not null and the username and password are not null enable the button
            if (cbOperator.SelectedItem != null && tbUsername.Text != null && tbPassword.Text != null)
            {
                btnAddUser.Enabled = true;
            }
            else { btnAddUser.Enabled = false; }
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            //Get domicilio selected
            var domicilio = coberturasList.SelectedItems[0];
            //Get numAdmin of domicilio
            var numAdmin = domicilio.SubItems[0].Text;

            //Get name operadora selected
            var operadora = operadoraList.SelectedItems[0].SubItems[0].Text;

            //Get cobertura with that numAdmin and operadora
            var coberturas = GetCoberturas().Coberturas;
            var coberturaToRemove = coberturas.FirstOrDefault(c => c.NumAdmin == Int32.Parse(numAdmin) && c.Operadora == operadora);

            //Ask to remove coberturaToRemove
            var channel = GrpcChannel.ForAddress("http://25.35.32.233:7044");

            var remove = new getInfo.getInfoClient(channel);

            var reply = remove.RemoveCobertura(coberturaToRemove);


            if (reply != null)
            {
                //Remove cobertura from list coberturas
                coberturas.Remove(coberturaToRemove);

                //If the domicilio has not more coberturas remove it from the list
                if (coberturas.FirstOrDefault(c => c.NumAdmin == Int32.Parse(numAdmin)) == null)
                {
                    coberturasList.Items.Remove(coberturasList.SelectedItems[0]);
                }

                //Remove operadora from list


                MessageBox.Show("Cobertura removida com sucesso!");
            }

        }

        private void btnAddOperator_Click(object sender, EventArgs e)
        {
            //Get domicilio selected
            var domicilio = coberturasList.SelectedItems[0];

            //Get numAdmin of domicilio
            var numAdmin = domicilio.SubItems[0].Text;

            //Get name operadora selected
            var operadoraToAdd = cbOperatorAdd.SelectedItem.ToString();

            //if domicilio exist in other cobertura get the NOwner
            var coberturas = GetCoberturas().Coberturas;
            var coberturaOwner = coberturas.FirstOrDefault(c => c.NumAdmin == Int32.Parse(numAdmin)).NOwner.ToString();

            //Cobertura to add
            var coberturaToAdd = new Cobertura
            {
                NumAdmin = Int32.Parse(numAdmin),
                Operadora = operadoraToAdd,
                Estado = "FREE",
                NOwner = coberturaOwner,
                Modalidade = ""
            };

            //Add cobertura
            var channel = GrpcChannel.ForAddress("http://25.35.32.233:7044");

            var add = new getInfo.getInfoClient(channel);

            var reply = add.AddCobertura(coberturaToAdd);

            if (reply != null)
            {
                //Add cobertura to list coberturas
                coberturas.Add(coberturaToAdd);
                //Add operadora to list

                ListViewItem operadora = new ListViewItem(operadoraToAdd);
                operadoraList.Items.Add(operadora);
                MessageBox.Show("Cobertura adicionada com sucesso!");
            }

        }

        private void cbOperatorAdd_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if the selected item is not null and the username and password are not null enable the button
            if (cbOperatorAdd.SelectedItem != null)
            {
                btnAddOperator.Enabled = true;
            }
            else { btnAddOperator.Enabled = false; }

        }

        private void btnRemoveUser_Click(object sender, EventArgs e)
        {
            //Get user selected
            var user = usersList.SelectedItems[0];

            //get user to remove
            var users = GetUsers().Users;
            var userToRemove = users.FirstOrDefault(u => u.Username == user.SubItems[0].Text);

            //Remove
            var channel = GrpcChannel.ForAddress("http://25.35.32.233:7044");

            var remove = new getInfo.getInfoClient(channel);

            var reply = remove.RemoveUser(userToRemove);

            if (reply != null)
            {
                //Remove user from list
                usersList.Items.Remove(user);
                MessageBox.Show("Utilizador removido com sucesso!");
            }
        }

        private void btnAddUser_Click(object sender, EventArgs e)
        {
            //Create user to add
            var userToAdd = new User
            {
                Username = tbUsername.Text,
                Password = tbPassword.Text,
                Operadora = cbOperator.SelectedItem.ToString()
            };

            //Add user
            var channel = GrpcChannel.ForAddress("http://25.35.32.233:7044");

            var add = new getInfo.getInfoClient(channel);

            var reply = add.AddUser(userToAdd);

            if (reply != null)
            {
                //Add user to list
                ListViewItem user = new ListViewItem(userToAdd.Username);
                user.SubItems.Add(userToAdd.Operadora);
                usersList.Items.Add(user);
                MessageBox.Show("Utilizador adicionado com sucesso!");
            }
        }

        private void GoToMyPage_Click(object sender, EventArgs e)
        {
            this.Hide();
            //Open new widnow
            ExternUserPage UserExterno = new ExternUserPage("OWNER", Username);
            UserExterno.ShowDialog();
        }
    }
}
