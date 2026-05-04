namespace Aktie_loginForm
{
    partial class RegisterForm
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
            textName = new TextBox();
            textEmail = new TextBox();
            textPassword = new TextBox();
            buttonRegister = new Button();
            buttonCancel = new Button();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            SuspendLayout();
            // 
            // textName
            // 
            textName.Location = new Point(695, 219);
            textName.Name = "textName";
            textName.Size = new Size(200, 39);
            textName.TabIndex = 0;
            // 
            // textEmail
            // 
            textEmail.Location = new Point(695, 317);
            textEmail.Name = "textEmail";
            textEmail.Size = new Size(200, 39);
            textEmail.TabIndex = 1;
            // 
            // textPassword
            // 
            textPassword.Location = new Point(695, 417);
            textPassword.Name = "textPassword";
            textPassword.Size = new Size(200, 39);
            textPassword.TabIndex = 2;
            // 
            // buttonRegister
            // 
            buttonRegister.Location = new Point(717, 557);
            buttonRegister.Name = "buttonRegister";
            buttonRegister.Size = new Size(150, 46);
            buttonRegister.TabIndex = 3;
            buttonRegister.Text = "Opret bruger";
            buttonRegister.UseVisualStyleBackColor = true;
            buttonRegister.Click += btnRegister_Click;
            // 
            // buttonCancel
            // 
            buttonCancel.Location = new Point(717, 646);
            buttonCancel.Name = "buttonCancel";
            buttonCancel.Size = new Size(150, 46);
            buttonCancel.TabIndex = 4;
            buttonCancel.Text = "Annuller";
            buttonCancel.UseVisualStyleBackColor = true;
            buttonCancel.Click += buttonCancel_Click;

            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(483, 219);
            label1.Name = "label1";
            label1.Size = new Size(120, 32);
            label1.TabIndex = 5;
            label1.Text = "Brugernavn";

            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(483, 317);
            label2.Name = "label2";
            label2.Size = new Size(70, 32);
            label2.TabIndex = 6;
            label2.Text = "Email";

            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(483, 417);
            label3.Name = "label3";
            label3.Size = new Size(100, 32);
            label3.TabIndex = 7;
            label3.Text = "Password";

            // 
            // RegisterForm
            // 
            AutoScaleDimensions = new SizeF(13F, 32F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1583, 964);

            Controls.Add(label1);
            Controls.Add(label2);
            Controls.Add(label3);

            Controls.Add(buttonCancel);
            Controls.Add(buttonRegister);
            Controls.Add(textPassword);
            Controls.Add(textEmail);
            Controls.Add(textName);

            Name = "RegisterForm";
            Text = "RegisterForm";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox textName;
        private TextBox textEmail;
        private TextBox textPassword;
        private Button buttonRegister;
        private Button buttonCancel;
        private Label label1;
        private Label label2;
        private Label label3;
    }
}