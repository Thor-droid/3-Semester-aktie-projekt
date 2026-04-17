using Aktie_WebsiteMVCV2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace Aktie_WebsiteMVCV2.Controllers
{
    public class AccountController : Controller
    {
        private string connectionString =
            "Data Source=hildur.ucn.dk;Initial Catalog=DMA-CSD-V251_10665995;User ID=DMA-CSD-V251_10665995;Password=Password1!;Trust Server Certificate=True";

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

                string sql = "SELECT CustomerId, Email, Password FROM Customers WHERE Email = @Email AND Password = @Password";

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@Email", email);
                cmd.Parameters.AddWithValue("@Password", password);

                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    int customerId = (int)reader["CustomerId"];

                    // TODO: later we will store login in session/cookie
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ViewBag.ErrorMessage = "Forkert email eller password";
                    return View();
                }
            }
        }
    

        // ---------------- REGISTER (GET) ----------------
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        // ---------------- REGISTER (POST) ----------------
        [HttpPost]
        public IActionResult Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                // Tjek om email eller navn findes
                string checkSql = @"
                    SELECT 1 
                    FROM Customers 
                    WHERE Email = @Email OR CustomerName = @Name";

                SqlCommand checkCmd = new SqlCommand(checkSql, conn);
                checkCmd.Parameters.AddWithValue("@Email", model.Email);
                checkCmd.Parameters.AddWithValue("@Name", model.Name);

                var exists = checkCmd.ExecuteScalar();

                if (exists != null)
                {
                    model.ErrorMessage = "Email eller brugernavn findes allerede.";
                    return View(model);
                }

                // INSERT bruger
                string sql = @"
                    INSERT INTO Customers (Email, CustomerName, Password)
                    VALUES (@Email, @Name, @Password)";

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@Email", model.Email);
                cmd.Parameters.AddWithValue("@Name", model.Name);
                cmd.Parameters.AddWithValue("@Password", model.Password);

                int rowsAffected = cmd.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    return RedirectToAction("Login");
                }

                model.ErrorMessage = "Der opstod en fejl under oprettelsen.";
                return View(model);
            }
        }
    }
}