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

        //User Exists

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

        // Create User

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
        SELECT KundeID, KundeNavn, AbonnementID, IsAdmin
        FROM Customers
        WHERE Email = @Email AND PasswordHash = @Password";

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

        // DELETE USER

        public bool DeleteUserByEmail(string email)
        {
            using SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();

            using SqlTransaction transaction = conn.BeginTransaction();

            try
            {
                int kundeId;

                // 1. Find KundeID, fordi det skal bruges til at slette hvis der er foreign keys
                string findSql = "SELECT KundeID FROM Customers WHERE Email = @Email";

                using (SqlCommand findCmd = new SqlCommand(findSql, conn, transaction))
                {
                    findCmd.Parameters.AddWithValue("@Email", email);

                    var result = findCmd.ExecuteScalar();

                    if (result == null)
                        return false;

                    kundeId = Convert.ToInt32(result);
                }

                // 2. Sætter AbonnementID til NULL for at fjerne reference,
                // så abonnementet kan slettes uden at bryde foreign key constraints
                string updateCustomer = "UPDATE Customers SET AbonnementID = NULL WHERE KundeID = @KundeID";

                using (SqlCommand cmd = new SqlCommand(updateCustomer, conn, transaction))
                {
                    cmd.Parameters.AddWithValue("@KundeID", kundeId);
                    cmd.ExecuteNonQuery();
                }

                // 3. Slet Notifikationer
                string deleteNoti = "DELETE FROM Notifikation WHERE KundeID = @KundeID";

                using (SqlCommand cmd = new SqlCommand(deleteNoti, conn, transaction))
                {
                    cmd.Parameters.AddWithValue("@KundeID", kundeId);
                    cmd.ExecuteNonQuery();
                }

                // 4. Slet relationer mellem abonnement og aktiepakker (NYT - vigtigt!)
                string deleteAktiepakkeAbonnement = @"
                    DELETE FROM AktiepakkeAbonnement
                    WHERE AbonnementID IN (
                        SELECT AbonnementID FROM Abonnement WHERE KundeID = @KundeID
                    )";

                using (SqlCommand cmd = new SqlCommand(deleteAktiepakkeAbonnement, conn, transaction))
                {
                    cmd.Parameters.AddWithValue("@KundeID", kundeId);
                    cmd.ExecuteNonQuery();
                }

                // 5. Slet Abonnement
                string deleteSub = "DELETE FROM Abonnement WHERE KundeID = @KundeID";

                using (SqlCommand cmd = new SqlCommand(deleteSub, conn, transaction))
                {
                    cmd.Parameters.AddWithValue("@KundeID", kundeId);
                    cmd.ExecuteNonQuery();
                }

                // 6. Slet Customer
                string deleteUser = "DELETE FROM Customers WHERE KundeID = @KundeID";

                using (SqlCommand cmd = new SqlCommand(deleteUser, conn, transaction))
                {
                    cmd.Parameters.AddWithValue("@KundeID", kundeId);
                    int rows = cmd.ExecuteNonQuery();

                    transaction.Commit();

                    return rows > 0;
                }
            }
            catch
            {
                transaction.Rollback();
                return false;
            }
        }
    }
}