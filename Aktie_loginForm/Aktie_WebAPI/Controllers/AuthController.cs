using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Aktie_WebAPI.Models;

namespace Aktie_WebAPI.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private string connectionString =
            "Data Source=hildur.ucn.dk;Initial Catalog=DMA-CSD-V251_10665995;User ID=DMA-CSD-V251_10665995;Password=Password1!;TrustServerCertificate=True";

        // ---------------- REGISTER ----------------
        [HttpPost("register")]
        public IActionResult Register(RegisterModel model)
        {
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
                        return BadRequest(new { message = "Bruger findes allerede" });
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
                    return Ok(new { success = true });

                return BadRequest(new { success = false });
            }
        }

        // ---------------- LOGIN ----------------
        [HttpPost("login")]
        public IActionResult Login(LoginModel model)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                string sql = @"
                    SELECT KundeID, KundeNavn
                    FROM Customers
                    WHERE Email = @Email AND PasswordHash = @Password";

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@Email", model.Email);
                cmd.Parameters.AddWithValue("@Password", model.Password);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return Ok(new
                        {
                            success = true,
                            kundeId = reader["KundeID"],
                            navn = reader["KundeNavn"]
                        });
                    }
                }

                return Unauthorized(new { success = false });
            }
        }
    }
}