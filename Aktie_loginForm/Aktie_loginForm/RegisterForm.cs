using Aktie_loginForm.Model;    // RegisterViewModel
using Aktie_loginForm.Services;   // AuthApiService
using System;
using System.Windows.Forms;

namespace Aktie_loginForm
{
    public partial class RegisterForm : Form
    {
        private readonly AuthApiService _authService = new AuthApiService();

        public RegisterForm()
        {
            InitializeComponent();
        }

        private async void btnRegister_Click(object sender, EventArgs e)
        {
            // Basic validation
            if (string.IsNullOrWhiteSpace(textName.Text) ||
                string.IsNullOrWhiteSpace(textEmail.Text) ||
                string.IsNullOrWhiteSpace(textPassword.Text))
            {
                MessageBox.Show("Udfyld alle felter");
                return;
            }

            var model = new RegisterViewModel
            {
                KundeNavn = textName.Text,
                Email = textEmail.Text,
                Password = textPassword.Text
            };

            try
            {
                var response = await _authService.Register(model);

                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show("Bruger oprettet!");
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    string error = await response.Content.ReadAsStringAsync();
                    MessageBox.Show($"Fejl: {error}");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Noget gik galt: {ex.Message}");
            }
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}