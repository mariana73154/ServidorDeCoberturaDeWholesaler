namespace GrpcClient
{
    partial class AdminPage
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            cbOptions = new ComboBox();
            label1 = new Label();
            btnAddOperator = new Button();
            btnRemove = new Button();
            coberturasList = new ListView();
            AdminNumber = new ColumnHeader();
            County = new ColumnHeader();
            Locality = new ColumnHeader();
            Street = new ColumnHeader();
            Door = new ColumnHeader();
            ZipCode = new ColumnHeader();
            usersList = new ListView();
            UserName = new ColumnHeader();
            Operadora = new ColumnHeader();
            label3 = new Label();
            operadoraList = new ListView();
            btnRemoveUser = new Button();
            btnAddUser = new Button();
            tbUsername = new TextBox();
            tbPassword = new TextBox();
            label2 = new Label();
            label4 = new Label();
            label5 = new Label();
            label6 = new Label();
            cbOperator = new ComboBox();
            cbOperatorAdd = new ComboBox();
            GoToMyPage = new Button();
            SuspendLayout();
            // 
            // cbOptions
            // 
            cbOptions.Font = new Font("Segoe UI Semibold", 14.25F, FontStyle.Bold, GraphicsUnit.Point);
            cbOptions.FormattingEnabled = true;
            cbOptions.Items.AddRange(new object[] { "All", "Reserved", "Activated", "Deactivated", "Terminated" });
            cbOptions.Location = new Point(27, 52);
            cbOptions.Name = "cbOptions";
            cbOptions.Size = new Size(387, 33);
            cbOptions.TabIndex = 2;
            cbOptions.Tag = "Actions";
            cbOptions.Text = "Actions";
            cbOptions.SelectedIndexChanged += ListOfOptions_SelectedIndexChanged;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 15.75F, FontStyle.Bold, GraphicsUnit.Point);
            label1.Location = new Point(25, 14);
            label1.Name = "label1";
            label1.Size = new Size(108, 30);
            label1.TabIndex = 4;
            label1.Text = "Domiciles";
            // 
            // btnAddOperator
            // 
            btnAddOperator.Enabled = false;
            btnAddOperator.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point);
            btnAddOperator.ImageAlign = ContentAlignment.TopCenter;
            btnAddOperator.Location = new Point(172, 357);
            btnAddOperator.Name = "btnAddOperator";
            btnAddOperator.Size = new Size(99, 31);
            btnAddOperator.TabIndex = 8;
            btnAddOperator.Text = "+";
            btnAddOperator.TextAlign = ContentAlignment.TopCenter;
            btnAddOperator.UseVisualStyleBackColor = true;
            btnAddOperator.Click += btnAddOperator_Click;
            // 
            // btnRemove
            // 
            btnRemove.Enabled = false;
            btnRemove.Font = new Font("Segoe UI Semibold", 14.25F, FontStyle.Bold, GraphicsUnit.Point);
            btnRemove.Location = new Point(172, 389);
            btnRemove.Name = "btnRemove";
            btnRemove.Size = new Size(99, 33);
            btnRemove.TabIndex = 9;
            btnRemove.Text = "Remove";
            btnRemove.UseVisualStyleBackColor = true;
            btnRemove.Click += btnRemove_Click;
            // 
            // coberturasList
            // 
            coberturasList.Columns.AddRange(new ColumnHeader[] { AdminNumber, County, Locality, Street, Door, ZipCode });
            coberturasList.FullRowSelect = true;
            coberturasList.Location = new Point(27, 91);
            coberturasList.Name = "coberturasList";
            coberturasList.Size = new Size(387, 215);
            coberturasList.TabIndex = 10;
            coberturasList.UseCompatibleStateImageBehavior = false;
            coberturasList.View = View.Details;
            coberturasList.ItemSelectionChanged += coberturasList_ItemSelectionChanged;
            // 
            // AdminNumber
            // 
            AdminNumber.Text = "Number";
            // 
            // County
            // 
            County.Text = "County";
            // 
            // Locality
            // 
            Locality.Text = "Locality";
            // 
            // Street
            // 
            Street.Text = "Street";
            Street.Width = 80;
            // 
            // Door
            // 
            Door.Text = "Door";
            // 
            // ZipCode
            // 
            ZipCode.Text = "Zip Code";
            // 
            // usersList
            // 
            usersList.Columns.AddRange(new ColumnHeader[] { UserName, Operadora });
            usersList.FullRowSelect = true;
            usersList.Location = new Point(456, 52);
            usersList.Name = "usersList";
            usersList.Size = new Size(204, 254);
            usersList.TabIndex = 11;
            usersList.UseCompatibleStateImageBehavior = false;
            usersList.View = View.Details;
            usersList.ItemSelectionChanged += usersList_ItemSelectionChanged;
            // 
            // UserName
            // 
            UserName.Text = "Username";
            UserName.Width = 90;
            // 
            // Operadora
            // 
            Operadora.Text = "Operator";
            Operadora.Width = 90;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 15.75F, FontStyle.Bold, GraphicsUnit.Point);
            label3.Location = new Point(456, 14);
            label3.Name = "label3";
            label3.Size = new Size(65, 30);
            label3.TabIndex = 12;
            label3.Text = "Users";
            // 
            // operadoraList
            // 
            operadoraList.Location = new Point(27, 312);
            operadoraList.Name = "operadoraList";
            operadoraList.Size = new Size(139, 110);
            operadoraList.TabIndex = 13;
            operadoraList.UseCompatibleStateImageBehavior = false;
            operadoraList.View = View.List;
            operadoraList.ItemSelectionChanged += operadoraList_ItemSelectionChanged;
            // 
            // btnRemoveUser
            // 
            btnRemoveUser.Enabled = false;
            btnRemoveUser.Font = new Font("Segoe UI Semibold", 13.8F, FontStyle.Bold, GraphicsUnit.Point);
            btnRemoveUser.Location = new Point(665, 218);
            btnRemoveUser.Margin = new Padding(3, 2, 3, 2);
            btnRemoveUser.Name = "btnRemoveUser";
            btnRemoveUser.Size = new Size(109, 32);
            btnRemoveUser.TabIndex = 14;
            btnRemoveUser.Text = "Remove";
            btnRemoveUser.UseVisualStyleBackColor = true;
            btnRemoveUser.Click += btnRemoveUser_Click;
            // 
            // btnAddUser
            // 
            btnAddUser.Enabled = false;
            btnAddUser.Font = new Font("Segoe UI Semibold", 13.8F, FontStyle.Bold, GraphicsUnit.Point);
            btnAddUser.Location = new Point(665, 52);
            btnAddUser.Margin = new Padding(3, 2, 3, 2);
            btnAddUser.Name = "btnAddUser";
            btnAddUser.Size = new Size(109, 30);
            btnAddUser.TabIndex = 15;
            btnAddUser.Text = "Add user";
            btnAddUser.UseVisualStyleBackColor = true;
            btnAddUser.Click += btnAddUser_Click;
            // 
            // tbUsername
            // 
            tbUsername.Location = new Point(665, 102);
            tbUsername.Margin = new Padding(3, 2, 3, 2);
            tbUsername.Name = "tbUsername";
            tbUsername.Size = new Size(110, 23);
            tbUsername.TabIndex = 16;
            tbUsername.TextAlign = HorizontalAlignment.Center;
            // 
            // tbPassword
            // 
            tbPassword.Location = new Point(665, 142);
            tbPassword.Margin = new Padding(3, 2, 3, 2);
            tbPassword.Name = "tbPassword";
            tbPassword.Size = new Size(110, 23);
            tbPassword.TabIndex = 17;
            tbPassword.TextAlign = HorizontalAlignment.Center;
            tbPassword.UseSystemPasswordChar = true;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point);
            label2.Location = new Point(172, 310);
            label2.Name = "label2";
            label2.Size = new Size(54, 15);
            label2.TabIndex = 18;
            label2.Text = "Operator";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point);
            label4.Location = new Point(665, 84);
            label4.Name = "label4";
            label4.Size = new Size(60, 15);
            label4.TabIndex = 19;
            label4.Text = "Username";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point);
            label5.Location = new Point(665, 126);
            label5.Name = "label5";
            label5.Size = new Size(57, 15);
            label5.TabIndex = 20;
            label5.Text = "Password";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point);
            label6.Location = new Point(665, 167);
            label6.Name = "label6";
            label6.Size = new Size(54, 15);
            label6.TabIndex = 21;
            label6.Text = "Operator";
            // 
            // cbOperator
            // 
            cbOperator.FormattingEnabled = true;
            cbOperator.Location = new Point(665, 185);
            cbOperator.Margin = new Padding(3, 2, 3, 2);
            cbOperator.Name = "cbOperator";
            cbOperator.Size = new Size(110, 23);
            cbOperator.TabIndex = 22;
            cbOperator.SelectedIndexChanged += cbOperator_SelectedIndexChanged;
            // 
            // cbOperatorAdd
            // 
            cbOperatorAdd.FormattingEnabled = true;
            cbOperatorAdd.Location = new Point(173, 331);
            cbOperatorAdd.Margin = new Padding(3, 2, 3, 2);
            cbOperatorAdd.Name = "cbOperatorAdd";
            cbOperatorAdd.Size = new Size(98, 23);
            cbOperatorAdd.TabIndex = 23;
            cbOperatorAdd.SelectedIndexChanged += cbOperatorAdd_SelectedIndexChanged;
            // 
            // GoToMyPage
            // 
            GoToMyPage.Font = new Font("Segoe UI Semibold", 13.8F, FontStyle.Bold, GraphicsUnit.Point);
            GoToMyPage.Location = new Point(571, 390);
            GoToMyPage.Margin = new Padding(3, 2, 3, 2);
            GoToMyPage.Name = "GoToMyPage";
            GoToMyPage.Size = new Size(204, 32);
            GoToMyPage.TabIndex = 24;
            GoToMyPage.Text = "Go To My User Page";
            GoToMyPage.UseVisualStyleBackColor = true;
            GoToMyPage.Click += GoToMyPage_Click;
            // 
            // AdminPage
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(GoToMyPage);
            Controls.Add(cbOperatorAdd);
            Controls.Add(cbOperator);
            Controls.Add(label6);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(label2);
            Controls.Add(tbPassword);
            Controls.Add(tbUsername);
            Controls.Add(btnAddUser);
            Controls.Add(btnRemoveUser);
            Controls.Add(operadoraList);
            Controls.Add(label3);
            Controls.Add(usersList);
            Controls.Add(coberturasList);
            Controls.Add(btnRemove);
            Controls.Add(btnAddOperator);
            Controls.Add(label1);
            Controls.Add(cbOptions);
            Name = "AdminPage";
            Text = "AdminPage";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private ComboBox cbOptions;
        private Label label1;
        private Button btnAddOperator;
        private Button btnRemove;
        private ListView coberturasList;
        private ColumnHeader AdminNumber;
        private ColumnHeader County;
        private ColumnHeader Locality;
        private ColumnHeader Street;
        private ColumnHeader Door;
        private ColumnHeader ZipCode;
        private ListView usersList;
        private ColumnHeader UserName;
        private ColumnHeader Operadora;
        private Label label3;
        private ListView operadoraList;
        private Button btnRemoveUser;
        private Button btnAddUser;
        private TextBox tbUsername;
        private TextBox tbPassword;
        private Label label2;
        private Label label4;
        private Label label5;
        private Label label6;
        private ComboBox cbOperator;
        private ComboBox cbOperatorAdd;
        private Button GoToMyPage;
    }
}