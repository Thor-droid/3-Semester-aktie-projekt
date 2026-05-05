namespace Aktie_loginForm
{
    partial class AdminForm
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
            buttonOpretBruger = new Button();
            buttonSletBruger = new Button();
            SuspendLayout();
            // 
            // buttonOpretBruger
            // 
            buttonOpretBruger.Location = new Point(264, 175);
            buttonOpretBruger.Name = "buttonOpretBruger";
            buttonOpretBruger.Size = new Size(295, 46);
            buttonOpretBruger.TabIndex = 0;
            buttonOpretBruger.Text = "Opret Bruger";
            buttonOpretBruger.UseVisualStyleBackColor = true;
            buttonOpretBruger.Click += buttonOpretBruger_Click;
            // 
            // buttonSletBruger
            // 
            buttonSletBruger.Location = new Point(264, 241);
            buttonSletBruger.Name = "buttonSletBruger";
            buttonSletBruger.Size = new Size(295, 46);
            buttonSletBruger.TabIndex = 1;
            buttonSletBruger.Text = "Slet Bruger";
            buttonSletBruger.UseVisualStyleBackColor = true;
            buttonSletBruger.Click += buttonSletBruger_Click;
            // 
            // AdminForm
            // 
            AutoScaleDimensions = new SizeF(13F, 32F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(904, 538);
            Controls.Add(buttonSletBruger);
            Controls.Add(buttonOpretBruger);
            Name = "AdminForm";
            Text = "AdminForm";
            ResumeLayout(false);
        }

        #endregion

        private Button buttonOpretBruger;
        private Button buttonSletBruger;
    }
}