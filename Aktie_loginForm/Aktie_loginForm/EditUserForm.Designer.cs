namespace Aktie_loginForm
{
    partial class EditUserForm
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
            textEmail = new TextBox();
            textName = new TextBox();
            textPassword = new TextBox();
            labelEmail = new Label();
            labelNavn = new Label();
            labelPassword = new Label();
            buttonUpdateUser = new Button();
            SuspendLayout();
            // 
            // textEmail
            // 
            textEmail.Location = new Point(305, 100);
            textEmail.Name = "textEmail";
            textEmail.Size = new Size(200, 39);
            textEmail.TabIndex = 0;
            // 
            // textName
            // 
            textName.Location = new Point(305, 169);
            textName.Name = "textName";
            textName.Size = new Size(200, 39);
            textName.TabIndex = 1;
            // 
            // textPassword
            // 
            textPassword.Location = new Point(305, 242);
            textPassword.Name = "textPassword";
            textPassword.Size = new Size(200, 39);
            textPassword.TabIndex = 2;
            // 
            // labelEmail
            // 
            labelEmail.AutoSize = true;
            labelEmail.Location = new Point(160, 100);
            labelEmail.Name = "labelEmail";
            labelEmail.Size = new Size(71, 32);
            labelEmail.TabIndex = 3;
            labelEmail.Text = "Email";
            // 
            // labelNavn
            // 
            labelNavn.AutoSize = true;
            labelNavn.Location = new Point(160, 169);
            labelNavn.Name = "labelNavn";
            labelNavn.Size = new Size(70, 32);
            labelNavn.TabIndex = 4;
            labelNavn.Text = "Navn";
            // 
            // labelPassword
            // 
            labelPassword.AutoSize = true;
            labelPassword.Location = new Point(160, 242);
            labelPassword.Name = "labelPassword";
            labelPassword.Size = new Size(111, 32);
            labelPassword.TabIndex = 5;
            labelPassword.Text = "Password";
            // 
            // buttonUpdateUser
            // 
            buttonUpdateUser.Location = new Point(305, 335);
            buttonUpdateUser.Name = "buttonUpdateUser";
            buttonUpdateUser.Size = new Size(200, 46);
            buttonUpdateUser.TabIndex = 6;
            buttonUpdateUser.Text = "Confirm Update";
            buttonUpdateUser.UseVisualStyleBackColor = true;
            buttonUpdateUser.Click += buttonUpdateUser_Click_1;
            // 
            // EditUserForm
            // 
            AutoScaleDimensions = new SizeF(13F, 32F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(buttonUpdateUser);
            Controls.Add(labelPassword);
            Controls.Add(labelNavn);
            Controls.Add(labelEmail);
            Controls.Add(textPassword);
            Controls.Add(textName);
            Controls.Add(textEmail);
            Name = "EditUserForm";
            Text = "EditUserForm";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox textEmail;
        private TextBox textName;
        private TextBox textPassword;
        private Label labelEmail;
        private Label labelNavn;
        private Label labelPassword;
        private Button buttonUpdateUser;
    }
}