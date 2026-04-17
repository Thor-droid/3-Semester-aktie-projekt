using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Aktie_WebsiteMVCV2.Models;

namespace Aktie_WebsiteMVCV2.Controllers.Api
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private string connectionString =
            "Data Source=hildur.ucn.dk;Initial Catalog=DMA-CSD-V251_10665995;User ID=DMA-CSD-V251_10665995;Password=Password1!;TrustServerCertificate=True";

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginModel model)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                string sql = @"
            SELECT KundeID, KundeNavn, PasswordHash
            FROM Customers
            WHERE Email = @Email";

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@Email", model.Email);

                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    string dbPassword = reader["PasswordHash"].ToString();

                    if (dbPassword == model.Password)
                    {
                        return Ok(new
                        {
                            success = true,
                            kundeId = reader["KundeID"],
                            name = reader["KundeNavn"]
                        });
                    }
                }

                return Unauthorized(new { success = false });
            }
        }
    }
}