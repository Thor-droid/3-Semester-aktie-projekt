using Aktie_WebAPI.Models;
using Aktie_WebsiteMVCV2.Models;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace Aktie_WebAPI.DatabaseAccess
{
    public class AuthAccess
    {
        private readonly string connectionString;

        // Hent fra appsettings.json
        public AuthAccess(IConfiguration config)
        {
            connectionString = config.GetConnectionString("DefaultConnection");
        }

        public bool UserExists(string email, string kundeNavn)
        {
            using SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();

            string sql = @"SELECT 1 FROM Customers WHERE Email = @Email OR KundeNavn = @Name";

            using SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@Email", email);
            cmd.Parameters.AddWithValue("@Name", kundeNavn);

            return cmd.ExecuteScalar() != null;
        }

        public bool CreateUser(RegisterModel model)
        {
            using SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();

            string sql = @"INSERT INTO Customers (Email, KundeNavn, PasswordHash, AbonnementID) VALUES (@Email, @Name, @Password, NULL)";
            using SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@Email", model.Email);
            cmd.Parameters.AddWithValue("@Name", model.KundeNavn);
            cmd.Parameters.AddWithValue("@Password", model.Password);

            return cmd.ExecuteNonQuery() > 0;
        }
        public List<UserViewModel> GetAllUsers()
        {
            var users = new List<UserViewModel>();

            using SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();

            string sql = "SELECT KundeID, KundeNavn, Email FROM Customers";

            using SqlCommand cmd = new SqlCommand(sql, conn);
            using SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                var user = new UserViewModel( (int)reader["KundeID"], reader["KundeNavn"]?.ToString() ?? "", reader["Email"]?.ToString() ?? ""
                );
                users.Add(user);
            }
            return users;
        }

        public LoginResponse? Login(LoginModel model)
        {
            using SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();

            string sql = @"SELECT KundeID, KundeNavn, AbonnementID, IsAdmin FROM Customers WHERE Email = @Email AND PasswordHash = @Password";
            using SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@Email", model.Email);
            cmd.Parameters.AddWithValue("@Password", model.Password);

            using SqlDataReader reader = cmd.ExecuteReader();

            if (!reader.Read())
                return null;

            int kundeId = Convert.ToInt32(reader["KundeID"]);
            string navn = reader["KundeNavn"].ToString();

            int? abonnementId = null;

            if (reader["AbonnementID"] != DBNull.Value)
            {
                abonnementId = Convert.ToInt32(reader["AbonnementID"]);
            }

            bool isAdmin = false;

            if (reader["IsAdmin"] != DBNull.Value)
            {
                isAdmin = Convert.ToBoolean(reader["IsAdmin"]);
            }

            return new LoginResponse(true, kundeId, navn, abonnementId, isAdmin);
        }

        //DELETE USER

        public bool DeleteUserByEmail(string email)
        {
            using SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();

            string sql = "DELETE FROM Customers WHERE Email = @Email";

            using SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@Email", email);

            return cmd.ExecuteNonQuery() > 0;
        }

        // Opdater Bruger ud fra email

        public bool UpdateUser(RegisterModel model)
        {
            using SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();

            string sql = @"UPDATE Customers SET KundeNavn = @Name, PasswordHash = @Password WHERE Email = @Email";

            using SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@Email", model.Email);
            cmd.Parameters.AddWithValue("@Name", model.KundeNavn);
            cmd.Parameters.AddWithValue("@Password", model.Password);

            return cmd.ExecuteNonQuery() > 0;
        }
    }
  }