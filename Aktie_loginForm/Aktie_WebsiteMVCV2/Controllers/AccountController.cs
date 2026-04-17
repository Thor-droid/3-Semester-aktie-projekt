using Aktie_WebsiteMVCV2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace Aktie_WebsiteMVCV2.Controllers
{
    public class AccountController : Controller
    {
        private string connectionString =
            "Data Source=hildur.ucn.dk;Initial Catalog=DMA-CSD-V251_10665995;User ID=DMA-CSD-V251_10665995;Password=Password1!;Trust Server Certificate=True";

        // ---------------- LOGIN (GET) ----------------
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        // ---------------- LOGIN (POST) ----------------
        [HttpPost]
        public IActionResult Login(string email, string password)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                string sql = @"
                    SELECT KundeID, KundeNavn
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
                return View(model);

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                string checkSql = @"
            SELECT 1 
            FROM Customers 
            WHERE Email = @Email OR KundeNavn = @Name";

                SqlCommand checkCmd = new SqlCommand(checkSql, conn);
                checkCmd.Parameters.AddWithValue("@Email", model.Email);
                checkCmd.Parameters.AddWithValue("@Name", model.KundeNavn);

                var exists = checkCmd.ExecuteScalar();

                if (exists != null)
                {
                    model.ErrorMessage = "Bruger findes allerede.";
                    return View(model);
                }

                string sql = @"
            INSERT INTO Customers (Email, KundeNavn, PasswordHash)
            VALUES (@Email, @Name, @Password)";

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@Email", model.Email);
                cmd.Parameters.AddWithValue("@Name", model.KundeNavn);
                cmd.Parameters.AddWithValue("@Password", model.Password);

                int rows = cmd.ExecuteNonQuery();

                if (rows > 0)
                    return RedirectToAction("Login");

                model.ErrorMessage = "Fejl ved oprettelse.";
                return View(model);
            }
        }
    }
}