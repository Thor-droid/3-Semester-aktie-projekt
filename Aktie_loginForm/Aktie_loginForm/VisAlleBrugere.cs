using Aktie_loginForm.BusinessLogic;
using Aktie_loginForm.Services;
using System;
using System.Windows.Forms;

namespace Aktie_loginForm
{
    public partial class VisAlleBrugere : Form
    {
        private readonly AuthBusinessLogic _authLogic;

        public VisAlleBrugere()
        {
            InitializeComponent();
            _authLogic = new AuthBusinessLogic(new AuthApiService());

            // Sørg for at Load event bliver kaldt
            this.Load += VisAlleBrugere_Load;
        }

        private async void VisAlleBrugere_Load(object sender, EventArgs e)
        {
            var users = await _authLogic.GetAllUsers();

            dataGridView1.DataSource = users;
        }
    }
}