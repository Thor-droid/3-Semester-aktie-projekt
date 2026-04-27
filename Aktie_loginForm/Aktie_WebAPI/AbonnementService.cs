namespace Aktie_WebAPI;

using Microsoft.Data.SqlClient;

public class AbonnementService
{
    private readonly string _connectionString;

    public AbonnementService(string connectionString)
    {
        _connectionString = connectionString;
    }

    public bool Subscribe(int kundeId, int kategoriId, int aktiepakkeId)
    {
        using SqlConnection conn = new SqlConnection(_connectionString);
        conn.Open();

        using SqlTransaction transaction = conn.BeginTransaction();

        try
        {
            // 🔥 Hent max brugere + nuværende antal
            string checkSql = @"
                SELECT 
                    k.MaxBrugere,
                    COUNT(a.AbonnementID) AS CurrentUsers
                FROM Kategori k
                LEFT JOIN Abonnement a ON a.KategoriID = k.KategoriID
                WHERE k.KategoriID = @KategoriID
                GROUP BY k.MaxBrugere";

            int maxBrugere;
            int currentUsers;

            using (SqlCommand cmd = new SqlCommand(checkSql, conn, transaction))
            {
                cmd.Parameters.AddWithValue("@KategoriID", kategoriId);

                using SqlDataReader reader = cmd.ExecuteReader();

                if (!reader.Read())
                    return false;

                maxBrugere = Convert.ToInt32(reader["MaxBrugere"]);
                currentUsers = Convert.ToInt32(reader["CurrentUsers"]);
            }

            if (currentUsers >= maxBrugere)
            {
                transaction.Rollback();
                return false;
            }

            // 🔥 Opret abonnement
            string insertAbonnementSql = @"
                INSERT INTO Abonnement (Dato, KategoriID, KundeID)
                OUTPUT INSERTED.AbonnementID
                VALUES (GETDATE(), @KategoriID, @KundeID)";

            int abonnementId;

            using (SqlCommand cmd = new SqlCommand(insertAbonnementSql, conn, transaction))
            {
                cmd.Parameters.AddWithValue("@KategoriID", kategoriId);
                cmd.Parameters.AddWithValue("@KundeID", kundeId);

                abonnementId = Convert.ToInt32(cmd.ExecuteScalar());
            }

            // 🔥 Link til aktiepakke
            string linkSql = @"
            INSERT INTO AktiepakkeAbonnement (AktiepakkeID, AbonnementID)
            VALUES (@AktiepakkeID, @AbonnementID)";

            using (SqlCommand cmd = new SqlCommand(linkSql, conn, transaction))
            {
                cmd.Parameters.AddWithValue("@AktiepakkeID", aktiepakkeId);
                cmd.Parameters.AddWithValue("@AbonnementID", abonnementId);

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

    public int? GetKategoriByCustomer(int kundeId)
    {
        using SqlConnection conn = new SqlConnection(_connectionString);
        conn.Open();

        string sql = @"
            SELECT TOP 1 KategoriID
            FROM Abonnement
            WHERE KundeID = @KundeID
            ORDER BY Dato DESC";

        using SqlCommand cmd = new SqlCommand(sql, conn);
        cmd.Parameters.AddWithValue("@KundeID", kundeId);

        var result = cmd.ExecuteScalar();

        if (result == null)
            return null;

        return Convert.ToInt32(result);
    }
}