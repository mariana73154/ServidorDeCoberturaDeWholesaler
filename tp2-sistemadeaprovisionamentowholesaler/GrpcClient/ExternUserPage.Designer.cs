namespace GrpcClient
{
    partial class ExternUserPage
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
            coberturasList = new ListView();
            AdminNumber = new ColumnHeader();
            County = new ColumnHeader();
            Locality = new ColumnHeader();
            Street = new ColumnHeader();
            Door = new ColumnHeader();
            ZipCode = new ColumnHeader();
            label1 = new Label();
            btnReserve = new Button();
            btnActivate = new Button();
            btnDeactivate = new Button();
            btnTermin = new Button();
            lbHello = new Label();
            cbModalidade = new ComboBox();
            label2 = new Label();
            label3 = new Label();
            lbNotifications = new ListBox();
            SuspendLayout();
            // 
            // coberturasList
            // 
            coberturasList.Columns.AddRange(new ColumnHeader[] { AdminNumber, County, Locality, Street, Door, ZipCode });
            coberturasList.FullRowSelect = true;
            coberturasList.Location = new Point(18, 100);
            coberturasList.Name = "coberturasList";
            coberturasList.Size = new Size(453, 322);
            coberturasList.TabIndex = 11;
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
            County.Width = 80;
            // 
            // Locality
            // 
            Locality.Text = "Locality";
            Locality.Width = 80;
            // 
            // Street
            // 
            Street.Text = "Street";
            Street.Width = 100;
            // 
            // Door
            // 
            Door.Text = "Door";
            // 
            // ZipCode
            // 
            ZipCode.Text = "Zip Code";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 15.75F, FontStyle.Bold, GraphicsUnit.Point);
            label1.Location = new Point(18, 64);
            label1.Name = "label1";
            label1.Size = new Size(108, 30);
            label1.TabIndex = 12;
            label1.Text = "Domiciles";
            // 
            // btnReserve
            // 
            btnReserve.Enabled = false;
            btnReserve.Location = new Point(477, 155);
            btnReserve.Name = "btnReserve";
            btnReserve.Size = new Size(77, 37);
            btnReserve.TabIndex = 13;
            btnReserve.Text = "Reserve";
            btnReserve.UseVisualStyleBackColor = true;
            btnReserve.Click += btnReserve_Click;
            // 
            // btnActivate
            // 
            btnActivate.Enabled = false;
            btnActivate.Location = new Point(477, 199);
            btnActivate.Name = "btnActivate";
            btnActivate.Size = new Size(77, 37);
            btnActivate.TabIndex = 14;
            btnActivate.Text = "Activate";
            btnActivate.UseVisualStyleBackColor = true;
            btnActivate.Click += btnActivate_Click;
            // 
            // btnDeactivate
            // 
            btnDeactivate.Enabled = false;
            btnDeactivate.Location = new Point(477, 242);
            btnDeactivate.Name = "btnDeactivate";
            btnDeactivate.Size = new Size(77, 37);
            btnDeactivate.TabIndex = 15;
            btnDeactivate.Text = "Deactivate";
            btnDeactivate.UseVisualStyleBackColor = true;
            btnDeactivate.Click += btnDeactivate_Click;
            // 
            // btnTermin
            // 
            btnTermin.Enabled = false;
            btnTermin.Location = new Point(477, 285);
            btnTermin.Name = "btnTermin";
            btnTermin.Size = new Size(77, 34);
            btnTermin.TabIndex = 16;
            btnTermin.Text = "Terminate";
            btnTermin.UseVisualStyleBackColor = true;
            btnTermin.Click += btnTermin_Click;
            // 
            // lbHello
            // 
            lbHello.AutoSize = true;
            lbHello.Font = new Font("Segoe UI Black", 21.75F, FontStyle.Bold, GraphicsUnit.Point);
            lbHello.Location = new Point(15, 24);
            lbHello.Name = "lbHello";
            lbHello.Size = new Size(92, 40);
            lbHello.TabIndex = 17;
            lbHello.Text = "Hello";
            // 
            // cbModalidade
            // 
            cbModalidade.Enabled = false;
            cbModalidade.FormattingEnabled = true;
            cbModalidade.Items.AddRange(new object[] { "100Mb", "200Mb", "500Mb" });
            cbModalidade.Location = new Point(477, 126);
            cbModalidade.Name = "cbModalidade";
            cbModalidade.Size = new Size(77, 23);
            cbModalidade.TabIndex = 18;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold, GraphicsUnit.Point);
            label2.Location = new Point(483, 103);
            label2.Name = "label2";
            label2.Size = new Size(61, 17);
            label2.TabIndex = 19;
            label2.Text = "Modality";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 15.75F, FontStyle.Bold, GraphicsUnit.Point);
            label3.Location = new Point(560, 64);
            label3.Name = "label3";
            label3.Size = new Size(141, 30);
            label3.TabIndex = 21;
            label3.Text = "Notifications";
            // 
            // lbNotifications
            // 
            lbNotifications.FormattingEnabled = true;
            lbNotifications.ItemHeight = 15;
            lbNotifications.Location = new Point(560, 103);
            lbNotifications.Name = "lbNotifications";
            lbNotifications.Size = new Size(228, 319);
            lbNotifications.TabIndex = 22;
            // 
            // ExternUserPage
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(lbNotifications);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(cbModalidade);
            Controls.Add(lbHello);
            Controls.Add(btnTermin);
            Controls.Add(btnDeactivate);
            Controls.Add(btnActivate);
            Controls.Add(btnReserve);
            Controls.Add(label1);
            Controls.Add(coberturasList);
            Name = "ExternUserPage";
            Text = "ExternUserPage";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ListView coberturasList;
        private ColumnHeader AdminNumber;
        private ColumnHeader County;
        private ColumnHeader Locality;
        private ColumnHeader Street;
        private ColumnHeader Door;
        private ColumnHeader ZipCode;
        private Label label1;
        private Button btnReserve;
        private Button btnActivate;
        private Button btnDeactivate;
        private Button btnTermin;
        private Label lbHello;
        private ComboBox cbModalidade;
        private Label label2;
        private Label label3;
        private ListBox lbNotifications;
    }
}