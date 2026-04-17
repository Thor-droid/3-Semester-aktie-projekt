using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;

namespace Aktie_Website.Pages
{
    public class RegisterModel : PageModel
    {

        [BindProperty]
        public string Email { get; set; }

        [BindProperty]
        public string Password { get; set; }

        [BindProperty]
        public string Name { get; set; }

        public string ErrorMessage { get; set; }

        private string connectionString = "Data Source=hildur.ucn.dk;Initial Catalog=DMA-CSD-V251_10665995;User ID=DMA-CSD-V251_10665995;Password=Password1!;Trust Server Certificate=True";

        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            if (string.IsNullOrEmpty(Email) || !Email.Contains("@"))
            {
                ErrorMessage = "Email skal indeholde @";
                return Page();
            }

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                // Tjek om email eller navn findes
                string checkSql = "SELECT COUNT(*) FROM Customers WHERE Email = @Email OR CustomerName = @Name";
                SqlCommand checkCmd = new SqlCommand(checkSql, conn);
                checkCmd.Parameters.AddWithValue("@Email", Email);
                checkCmd.Parameters.AddWithValue("@Name", Name);

                int count = (int)checkCmd.ExecuteScalar();

                if (count > 0)
                {
                    ErrorMessage = "Email eller brugernavn findes allerede.";
                    return Page();
                }


                // Insert bruger i databasen
                string sql = "INSERT INTO Customers (Email, CustomerName, Password) VALUES (@Email, @Name, @Password)";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@Name", Name);
                cmd.Parameters.AddWithValue("@Email", Email);
                cmd.Parameters.AddWithValue("@Password", Password);

                int rowsAffected = cmd.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    // Registration successful
                    return RedirectToPage("/Login");
                }
                else
                {
                    // Registration failed
                    ErrorMessage = "Der opstod en fejl under oprettelsen af brugeren.";
                    return Page();
                }
            }
        }
    }
}