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
            VisAlleBrugere = new Button();
            buttonRedigerBruger = new Button();
            SuspendLayout();
            // 
            // buttonOpretBruger
            // 
            buttonOpretBruger.Location = new Point(264, 175);
            buttonOpretBruger.Margin = new Padding(4, 2, 4, 2);
            buttonOpretBruger.Name = "buttonOpretBruger";
            buttonOpretBruger.Size = new Size(295, 47);
            buttonOpretBruger.TabIndex = 0;
            buttonOpretBruger.Text = "Opret Bruger";
            buttonOpretBruger.UseVisualStyleBackColor = true;
            buttonOpretBruger.Click += buttonOpretBruger_Click;
            // 
            // buttonSletBruger
            // 
            buttonSletBruger.Location = new Point(264, 241);
            buttonSletBruger.Margin = new Padding(4, 2, 4, 2);
            buttonSletBruger.Name = "buttonSletBruger";
            buttonSletBruger.Size = new Size(295, 47);
            buttonSletBruger.TabIndex = 1;
            buttonSletBruger.Text = "Slet Bruger";
            buttonSletBruger.UseVisualStyleBackColor = true;
            buttonSletBruger.Click += buttonSletBruger_Click;
            // 
            // VisAlleBrugere
            // 
            VisAlleBrugere.Location = new Point(264, 372);
            VisAlleBrugere.Margin = new Padding(4, 2, 4, 2);
            VisAlleBrugere.Name = "VisAlleBrugere";
            VisAlleBrugere.Size = new Size(295, 47);
            VisAlleBrugere.TabIndex = 2;
            VisAlleBrugere.Text = "Vis Brugere";
            VisAlleBrugere.UseVisualStyleBackColor = true;
            VisAlleBrugere.Click += VisAlleBrugere_Click;
            // 
            // buttonRedigerBruger
            // 
            buttonRedigerBruger.Location = new Point(264, 307);
            buttonRedigerBruger.Name = "buttonRedigerBruger";
            buttonRedigerBruger.Size = new Size(295, 46);
            buttonRedigerBruger.TabIndex = 3;
            buttonRedigerBruger.Text = "Rediger Bruger";
            buttonRedigerBruger.UseVisualStyleBackColor = true;
            buttonRedigerBruger.Click += buttonRedigerBruger_Click;
            // 
            // AdminForm
            // 
            AutoScaleDimensions = new SizeF(13F, 32F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(904, 538);
            Controls.Add(buttonRedigerBruger);
            Controls.Add(VisAlleBrugere);
            Controls.Add(buttonSletBruger);
            Controls.Add(buttonOpretBruger);
            Margin = new Padding(4, 2, 4, 2);
            Name = "AdminForm";
            Text = "AdminForm";
            ResumeLayout(false);
        }

        #endregion

        private Button buttonOpretBruger;
        private Button buttonSletBruger;
        private Button button1;
        private Button VisAlleBrugere;
        private Button buttonRedigerBruger;
    }
}