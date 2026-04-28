using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Aktie_WebAPI.Models;
public class AuthRepository
{
    private readonly string _connectionString;

    public AuthRepository(string connectionString)
    {
        _connectionString = connectionString;
    }

    public bool UserExists(string email, string name)
    {
        using var conn = new SqlConnection(_connectionString);
        conn.Open();

        string sql = "SELECT 1 FROM Customers WHERE Email = @Email OR KundeNavn = @Name";

        using var cmd = new SqlCommand(sql, conn);
        cmd.Parameters.AddWithValue("@Email", email);
        cmd.Parameters.AddWithValue("@Name", name);

        return cmd.ExecuteScalar() != null;
    }

    public bool CreateUser(RegisterModel model)
    {
        using var conn = new SqlConnection(_connectionString);
        conn.Open();

        string sql = @"
            INSERT INTO Customers (Email, KundeNavn, PasswordHash, AbonnementID)
            VALUES (@Email, @Name, @Password, NULL)";

        using var cmd = new SqlCommand(sql, conn);
        cmd.Parameters.AddWithValue("@Email", model.Email);
        cmd.Parameters.AddWithValue("@Name", model.KundeNavn);
        cmd.Parameters.AddWithValue("@Password", model.Password);

        return cmd.ExecuteNonQuery() > 0;
    }

    public (int kundeId, string navn, int? abonnementId)? Login(LoginModel model)
    {
        using var conn = new SqlConnection(_connectionString);
        conn.Open();

        string sql = @"
            SELECT KundeID, KundeNavn, AbonnementID
            FROM Customers
            WHERE Email = @Email AND PasswordHash = @Password";

        using var cmd = new SqlCommand(sql, conn);
        cmd.Parameters.AddWithValue("@Email", model.Email);
        cmd.Parameters.AddWithValue("@Password", model.Password);

        using var reader = cmd.ExecuteReader();

        if (reader.Read())
        {
            int? abonnementId = reader["AbonnementID"] == DBNull.Value
                ? null
                : Convert.ToInt32(reader["AbonnementID"]);

            return (
                Convert.ToInt32(reader["KundeID"]),
                reader["KundeNavn"].ToString(),
                abonnementId
            );
        }

        return null;
    }
}