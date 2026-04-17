using Aktie_WebsiteMVCV2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace Aktie_WebsiteMVCV2.Controllers
{
    public class AccountController : Controller
    {
        private string connectionString =
            "Data Source=hildur.ucn.dk;Initial Catalog=DMA-CSD-V251_10665995;User ID=DMA-CSD-V251_10665995;Password=Password1!;Trust Server Certificate=True";

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
                    SELECT KundeID, KundeNavn
                    FROM Customers
                    WHERE Email = @Email AND PasswordHash = @Password";

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@Email", email);
                cmd.Parameters.AddWithValue("@Password", password);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }

                ViewBag.ErrorMessage = "Forkert email eller password";
                return View();
            }
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View(new RegisterViewModel());
        }
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

                string checkSql = @"
            SELECT KundeID
            FROM Customers
            WHERE Email = @Email OR KundeNavn = @Name";

                SqlCommand checkCmd = new SqlCommand(checkSql, conn);
                checkCmd.Parameters.AddWithValue("@Email", model.Email);
                checkCmd.Parameters.AddWithValue("@Name", model.KundeNavn);

                using (SqlDataReader reader = checkCmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        model.ErrorMessage = "Email eller kundenavn findes allerede.";
                        return View(model);
                    }
                }

                string insertSql = @"
            INSERT INTO Customers (Email, KundeNavn, PasswordHash)
            VALUES (@Email, @Name, @Password)";

                SqlCommand cmd = new SqlCommand(insertSql, conn);
                cmd.Parameters.AddWithValue("@Email", model.Email);
                cmd.Parameters.AddWithValue("@Name", model.KundeNavn);
                cmd.Parameters.AddWithValue("@Password", model.Password);

                int rows = cmd.ExecuteNonQuery();

                if (rows > 0)
                {
                    return RedirectToAction("Login");
                }

                model.ErrorMessage = "Brugeren blev ikke oprettet.";
                return View(model);
            }
        }
    }
}