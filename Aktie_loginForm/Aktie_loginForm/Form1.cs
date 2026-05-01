using Aktie_loginForm.BusinessLogic;
using Aktie_loginForm.Services;

namespace Aktie_loginForm
{
    public partial class Form1 : Form
    {
        private readonly AuthBusinessLogic _authBusinessLogic;

        public Form1()
        {
            InitializeComponent();

            var authService = new AuthApiService();
            _authBusinessLogic = new AuthBusinessLogic(authService);
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            string email = txtUsername.Text;
            string password = txtPassword.Text;

            var result = await _authBusinessLogic.Login(email, password);

            if (!result.Success)
            {
                MessageBox.Show(result.Message);
                return;
            }

            MessageBox.Show("Login successful!");

            if (result.User.IsAdmin)
            {
                AdminForm adminForm = new AdminForm();
                adminForm.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Du er ikke administrator.");
            }
        }
    }
}