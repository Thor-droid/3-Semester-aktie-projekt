using Aktie_loginForm.Model;
using Aktie_loginForm.Services;
namespace Aktie_loginForm
{
    public partial class EditUserForm : Form
    {
        private readonly AuthApiService _authService = new AuthApiService();

        public EditUserForm()
        {
            InitializeComponent();
        }

        private async void buttonUpdateUser_Click_1(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textEmail.Text) ||
                string.IsNullOrWhiteSpace(textName.Text) ||
                string.IsNullOrWhiteSpace(textPassword.Text))
            {
                MessageBox.Show("Udfyld alle felter");
                return;
            }

            var model = new RegisterViewModel
            {
                Email = textEmail.Text,
                KundeNavn = textName.Text,
                Password = textPassword.Text
            };

            var response = await _authService.UpdateUser(model);

            if (response.IsSuccessStatusCode)
            {
                MessageBox.Show("Bruger opdateret");
                this.Close();
            }
            else
            {
                MessageBox.Show("Kunne ikke opdatere bruger");
            }
        }
    }
}