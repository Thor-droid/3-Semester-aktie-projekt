using Aktie_loginForm.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;


namespace Aktie_loginForm
{
    public partial class AdminForm : Form
    {
        private readonly AuthApiService _authService = new AuthApiService();

        public AdminForm()
        {
            InitializeComponent();
        }

        private void buttonOpretBruger_Click(object sender, EventArgs e)
        {
            using RegisterForm registerForm = new RegisterForm();
            registerForm.ShowDialog();
        }

        private void buttonSletBruger_Click(object sender, EventArgs e)
        {
            using DeleteUserForm deleteUserForm = new DeleteUserForm();
            deleteUserForm.ShowDialog();
        }

        private void VisAlleBrugere_Click(object sender, EventArgs e)
        {
            using VisAlleBrugere visallebrugere = new VisAlleBrugere();
            visallebrugere.ShowDialog();
        }
    }
}
