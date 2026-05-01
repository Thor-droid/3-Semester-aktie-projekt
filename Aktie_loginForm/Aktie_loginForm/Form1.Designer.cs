namespace Aktie_loginForm
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            txtUsername = new TextBox();
            txtPassword = new TextBox();
            label2 = new Label();
            label3 = new Label();
            button1 = new Button();
            SuspendLayout();

            // txtUsername
            txtUsername.Location = new Point(347, 168);
            txtUsername.Name = "txtUsername";
            txtUsername.Size = new Size(100, 23);
            txtUsername.TabIndex = 0;

            // txtPassword
            txtPassword.Location = new Point(347, 222);
            txtPassword.Name = "txtPassword";
            txtPassword.Size = new Size(100, 23);
            txtPassword.TabIndex = 1;

            // label2 (Username)
            label2.AutoSize = true;
            label2.Location = new Point(281, 171);
            label2.Name = "label2";
            label2.Size = new Size(60, 15);
            label2.Text = "Username";

            // label3 (Password)
            label3.AutoSize = true;
            label3.Location = new Point(284, 225);
            label3.Name = "label3";
            label3.Size = new Size(57, 15);
            label3.Text = "Password";

            // button1 (Login)
            button1.Location = new Point(361, 269);
            button1.Name = "button1";
            button1.Size = new Size(75, 23);
            button1.Text = "Login";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;

            // Form1
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(button1);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(txtPassword);
            Controls.Add(txtUsername);
            Name = "Form1";
            Text = "Login";
            ResumeLayout(false);
            PerformLayout();
        }

        private TextBox txtUsername;
        private TextBox txtPassword;
        private Label label2;
        private Label label3;
        private Button button1;
    }
}