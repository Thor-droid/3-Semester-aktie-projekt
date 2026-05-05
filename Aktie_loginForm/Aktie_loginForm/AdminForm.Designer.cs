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
            SuspendLayout();
            // 
            // buttonOpretBruger
            // 
            buttonOpretBruger.Location = new Point(142, 82);
            buttonOpretBruger.Margin = new Padding(2, 1, 2, 1);
            buttonOpretBruger.Name = "buttonOpretBruger";
            buttonOpretBruger.Size = new Size(159, 22);
            buttonOpretBruger.TabIndex = 0;
            buttonOpretBruger.Text = "Opret Bruger";
            buttonOpretBruger.UseVisualStyleBackColor = true;
            buttonOpretBruger.Click += buttonOpretBruger_Click;
            // 
            // buttonSletBruger
            // 
            buttonSletBruger.Location = new Point(142, 113);
            buttonSletBruger.Margin = new Padding(2, 1, 2, 1);
            buttonSletBruger.Name = "buttonSletBruger";
            buttonSletBruger.Size = new Size(159, 22);
            buttonSletBruger.TabIndex = 1;
            buttonSletBruger.Text = "Slet Bruger";
            buttonSletBruger.UseVisualStyleBackColor = true;
            buttonSletBruger.Click += buttonSletBruger_Click;
            // 
            // VisAlleBrugere
            // 
            VisAlleBrugere.Location = new Point(142, 148);
            VisAlleBrugere.Margin = new Padding(2, 1, 2, 1);
            VisAlleBrugere.Name = "VisAlleBrugere";
            VisAlleBrugere.Size = new Size(159, 22);
            VisAlleBrugere.TabIndex = 2;
            VisAlleBrugere.Text = "Vis Brugere";
            VisAlleBrugere.UseVisualStyleBackColor = true;
            VisAlleBrugere.Click += VisAlleBrugere_Click;
            // 
            // AdminForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(487, 252);
            Controls.Add(VisAlleBrugere);
            Controls.Add(buttonSletBruger);
            Controls.Add(buttonOpretBruger);
            Margin = new Padding(2, 1, 2, 1);
            Name = "AdminForm";
            Text = "AdminForm";
            ResumeLayout(false);
        }

        #endregion

        private Button buttonOpretBruger;
        private Button buttonSletBruger;
        private Button button1;
        private Button VisAlleBrugere;
    }
}