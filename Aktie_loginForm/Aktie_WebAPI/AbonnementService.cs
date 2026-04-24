namespace Aktie_WebAPI;
using Microsoft.Data.SqlClient;
public class AbonnementService
{
    private readonly string _connectionString;

    public AbonnementService(string connectionString)
    {
        _connectionString = connectionString;
    }

    public bool Subscribe(int kundeId, int pakkeId)
    {
        using (SqlConnection conn = new SqlConnection(_connectionString))
        {
            conn.Open();

            using (SqlTransaction transaction = conn.BeginTransaction())
            {
                try
                {
                    // 🔒 LOCK rækken (CONCURRENCY)
                    string checkSql = @"
                        SELECT CurrentUsers, MaxUsers
                        FROM Aktiepakker WITH (UPDLOCK, ROWLOCK)
                        WHERE Id = @Id";

                    int current = 0;
                    int max = 0;

                    using (SqlCommand cmd = new SqlCommand(checkSql, conn, transaction))
                    {
                        cmd.Parameters.AddWithValue("@Id", pakkeId);

                        using (var reader = cmd.ExecuteReader())
                        {
                            if (!reader.Read())
                                return false;

                            current = (int)reader["CurrentUsers"];
                            max = (int)reader["MaxUsers"];
                        }
                    }

                    if (current >= max)
                    {
                        transaction.Rollback();
                        return false;
                    }

                    // ➕ Opdater antal brugere
                    string updateSql = @"
                        UPDATE Aktiepakker
                        SET CurrentUsers = CurrentUsers + 1
                        WHERE Id = @Id";

                    using (SqlCommand cmd = new SqlCommand(updateSql, conn, transaction))
                    {
                        cmd.Parameters.AddWithValue("@Id", pakkeId);
                        cmd.ExecuteNonQuery();
                    }

                    // ➕ Opret abonnement
                    string insertSql = @"
                        INSERT INTO Abonnement (KundeId, AktiepakkeId, Dato)
                        VALUES (@KundeId, @PakkeId, GETDATE())";

                    using (SqlCommand cmd = new SqlCommand(insertSql, conn, transaction))
                    {
                        cmd.Parameters.AddWithValue("@KundeId", kundeId);
                        cmd.Parameters.AddWithValue("@PakkeId", pakkeId);
                        cmd.ExecuteNonQuery();
                    }

                    transaction.Commit();
                    return true;
                }
                catch
                {
                    transaction.Rollback();
                    return false;
                }
            }
        }
    }
}