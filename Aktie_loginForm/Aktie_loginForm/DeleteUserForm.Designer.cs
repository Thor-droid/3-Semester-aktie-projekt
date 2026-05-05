namespace Aktie_loginForm
{
    partial class DeleteUserForm
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
            textEmail = new TextBox();
            labelEmail = new Label();
            buttonDeleteUser = new Button();
            SuspendLayout();

            // 
            // textEmail
            // 
            textEmail.Location = new Point(638, 209);
            textEmail.Name = "textEmail";
            textEmail.Size = new Size(200, 39);
            textEmail.TabIndex = 0;

            // 
            // labelEmail
            // 
            labelEmail.AutoSize = true;
            labelEmail.Location = new Point(561, 212);
            labelEmail.Name = "labelEmail";
            labelEmail.Size = new Size(71, 32);
            labelEmail.TabIndex = 1;
            labelEmail.Text = "Email";

            // 
            // buttonDeleteUser
            // 
            buttonDeleteUser.Location = new Point(638, 300);
            buttonDeleteUser.Name = "buttonDeleteUser";
            buttonDeleteUser.Size = new Size(200, 46);
            buttonDeleteUser.TabIndex = 2;
            buttonDeleteUser.Text = "Slet bruger";
            buttonDeleteUser.UseVisualStyleBackColor = true;
            buttonDeleteUser.Click += buttonDeleteUser_Click;

            // 
            // DeleteUserForm
            // 
            AutoScaleDimensions = new SizeF(13F, 32F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1481, 988);

            Controls.Add(buttonDeleteUser);
            Controls.Add(labelEmail);
            Controls.Add(textEmail);

            Name = "DeleteUserForm";
            Text = "Slet bruger";

            ResumeLayout(false);
            PerformLayout();
        }

        private TextBox textEmail;
        private Label labelEmail;
        private Button buttonDeleteUser;
    }
}