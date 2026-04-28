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

        // Register metode
        [HttpPost("register")]
        public IActionResult Register(RegisterModel model)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                string checkSql = @"
                    SELECT 1
                    FROM Customers
                    WHERE Email = @Email OR KundeNavn = @Name";

                using (SqlCommand checkCmd = new SqlCommand(checkSql, conn))
                {
                    checkCmd.Parameters.AddWithValue("@Email", model.Email);
                    checkCmd.Parameters.AddWithValue("@Name", model.KundeNavn);

                    var exists = checkCmd.ExecuteScalar();

                    if (exists != null)
                    {
                        return BadRequest(new { message = "Bruger findes allerede" });
                    }
                }

                string insertSql = @"
                    INSERT INTO Customers (Email, KundeNavn, PasswordHash, AbonnementID)
                    VALUES (@Email, @Name, @Password, NULL)";

                using (SqlCommand cmd = new SqlCommand(insertSql, conn))
                {
                    cmd.Parameters.AddWithValue("@Email", model.Email);
                    cmd.Parameters.AddWithValue("@Name", model.KundeNavn);
                    cmd.Parameters.AddWithValue("@Password", model.Password);

                    int rows = cmd.ExecuteNonQuery();

                    if (rows > 0)
                        return Ok(new { success = true });

                    return BadRequest(new { success = false });
                }
            }
        }

        // 
        [HttpPost("login")]
        public IActionResult Login(LoginModel model)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                string sql = @"
                    SELECT KundeID, KundeNavn, AbonnementID
                    FROM Customers
                    WHERE Email = @Email AND PasswordHash = @Password";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@Email", model.Email);
                    cmd.Parameters.AddWithValue("@Password", model.Password);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            int? abonnementId = reader["AbonnementID"] == DBNull.Value
                                ? null
                                : Convert.ToInt32(reader["AbonnementID"]);

                            return Ok(new
                            {
                                success = true,
                                kundeId = Convert.ToInt32(reader["KundeID"]),
                                navn = reader["KundeNavn"].ToString(),
                                abonnementId = abonnementId
                            });
                        }
                    }
                }

                return Unauthorized(new { success = false });
            }
        }
    }
}