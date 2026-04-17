using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace Aktie_Website.Controllers
{
    public class AccountController : Controller
    {
        private string connectionString =
            "Data Source=hildur.ucn.dk;Initial Catalog=DMA-CSD-V251_10665995;User ID=DMA-CSD-V251_10665995;Password=Password1!;Trust Server Certificate=True";

        // ---------------- REGISTER ----------------
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(string email, string password, string name)
        {
            if (string.IsNullOrEmpty(email) || !email.Contains("@"))
            {
                ViewBag.ErrorMessage = "Email skal indeholde @";
                return View();
            }

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                // TJEK OM BRUGER FINDES
                string checkSql = @"
                    SELECT COUNT(*) 
                    FROM Customers 
                    WHERE Email = @Email OR KundeNavn = @Name";

                SqlCommand checkCmd = new SqlCommand(checkSql, conn);
                checkCmd.Parameters.AddWithValue("@Email", email);
                checkCmd.Parameters.AddWithValue("@Name", name);

                int exists = (int)checkCmd.ExecuteScalar();

                if (exists > 0)
                {
                    ViewBag.ErrorMessage = "Email eller brugernavn findes allerede.";
                    return View();
                }

                // INSERT BRUGER
                string sql = @"
                    INSERT INTO Customers (Email, KundeNavn, PasswordHash)
                    VALUES (@Email, @Name, @Password)";

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@Email", email);
                cmd.Parameters.AddWithValue("@Name", name);
                cmd.Parameters.AddWithValue("@Password", password);

                cmd.ExecuteNonQuery();

                return RedirectToAction("Login", "Account");
            }
        }

        // ---------------- LOGIN ----------------
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string email, string password)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                string sql = @"
                    SELECT KundeID 
                    FROM Customers 
                    WHERE Email = @Email AND PasswordHash = @Password";

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@Email", email);
                cmd.Parameters.AddWithValue("@Password", password);

                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    return RedirectToAction("Index", "Home");
                }

                ViewBag.ErrorMessage = "Forkert email eller password";
                return View();
            }
        }
    }
}