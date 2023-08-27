namespace GrpcClient
{
    partial class LoginPage
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            Username = new TextBox();
            Password = new TextBox();
            label1 = new Label();
            label2 = new Label();
            LoginBtn = new Button();
            SuspendLayout();
            // 
            // Username
            // 
            Username.Location = new Point(297, 119);
            Username.Name = "Username";
            Username.Size = new Size(185, 23);
            Username.TabIndex = 1;
            Username.TextAlign = HorizontalAlignment.Center;
            // 
            // Password
            // 
            Password.Location = new Point(297, 167);
            Password.Name = "Password";
            Password.Size = new Size(185, 23);
            Password.TabIndex = 2;
            Password.TextAlign = HorizontalAlignment.Center;
            Password.UseSystemPasswordChar = true;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(363, 101);
            label1.Name = "label1";
            label1.Size = new Size(60, 15);
            label1.TabIndex = 3;
            label1.Text = "Username";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(363, 149);
            label2.Name = "label2";
            label2.Size = new Size(57, 15);
            label2.TabIndex = 4;
            label2.Text = "Password";
            // 
            // LoginBtn
            // 
            LoginBtn.Location = new Point(297, 212);
            LoginBtn.Name = "LoginBtn";
            LoginBtn.Size = new Size(185, 23);
            LoginBtn.TabIndex = 5;
            LoginBtn.Text = "Login";
            LoginBtn.UseVisualStyleBackColor = true;
            LoginBtn.Click += LoginBtn_Click;
            // 
            // LoginPage
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(LoginBtn);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(Password);
            Controls.Add(Username);
            Name = "LoginPage";
            Text = "Form1";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private TextBox Username;
        private TextBox Password;
        private Label label1;
        private Label label2;
        private Button LoginBtn;
    }
}