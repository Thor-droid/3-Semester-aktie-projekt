using Aktie_loginForm.Services;
using System;
using System.Windows.Forms;

namespace Aktie_loginForm
{
    public partial class DeleteUserForm : Form
    {
        private readonly AuthApiService _authService = new AuthApiService();

        public DeleteUserForm()
        {
            InitializeComponent();
        }

        private async void buttonDeleteUser_Click(object sender, EventArgs e)
        {
            string email = textEmail.Text;

            if (string.IsNullOrWhiteSpace(email))
            {
                MessageBox.Show("Skriv email på brugeren der skal slettes");
                return;
            }

            var confirm = MessageBox.Show(
                $"Er du sikker på, at du vil slette {email}?",
                "Slet bruger",
                MessageBoxButtons.YesNo);

            if (confirm != DialogResult.Yes)
                return;

            var response = await _authService.DeleteUserByEmail(email);

            if (response.IsSuccessStatusCode)
            {
                MessageBox.Show("Bruger slettet");
                this.Close();
            }
            else
            {
                MessageBox.Show("Kunne ikke slette bruger");
            }
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}