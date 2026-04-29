using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Aktie_WebAPI.Models;

namespace Aktie_WebAPI.DatabaseAccess
{
    public class AuthRepository
    {
        private readonly string connectionString;

        // 🔹 Hent fra appsettings.json
        public AuthRepository(IConfiguration config)
        {
            connectionString = config.GetConnectionString("DefaultConnection");
        }

        public bool UserExists(string email, string kundeNavn)
        {
            using SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();

            string sql = @"
                SELECT 1
                FROM Customers
                WHERE Email = @Email OR KundeNavn = @Name";

            using SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@Email", email);
            cmd.Parameters.AddWithValue("@Name", kundeNavn);

            return cmd.ExecuteScalar() != null;
        }

        public bool CreateUser(RegisterModel model)
        {
            using SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();

            string sql = @"
                INSERT INTO Customers (Email, KundeNavn, PasswordHash, AbonnementID)
                VALUES (@Email, @Name, @Password, NULL)";

            using SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@Email", model.Email);
            cmd.Parameters.AddWithValue("@Name", model.KundeNavn);
            cmd.Parameters.AddWithValue("@Password", model.Password);

            return cmd.ExecuteNonQuery() > 0;
        }

        public LoginResponse? Login(LoginModel model)
        {
            using SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();

            string sql = @"
                SELECT KundeID, KundeNavn, AbonnementID
                FROM Customers
                WHERE Email = @Email AND PasswordHash = @Password";

            using SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@Email", model.Email);
            cmd.Parameters.AddWithValue("@Password", model.Password);

            using SqlDataReader reader = cmd.ExecuteReader();

            if (!reader.Read())
                return null;

            return new LoginResponse
            {
                Success = true,
                KundeId = Convert.ToInt32(reader["KundeID"]),
                Navn = reader["KundeNavn"].ToString(),
                AbonnementId = reader["AbonnementID"] == DBNull.Value
                    ? null
                    : Convert.ToInt32(reader["AbonnementID"])
            };
        }
    }
}