using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace Aktie_WebAPI.DatabaseAccess
{
    public class AbonnementAccess
    {
        private readonly string connectionString;

        public AbonnementAccess(IConfiguration config)
        {
            connectionString = config.GetConnectionString("DefaultConnection");
        }

        //virtual er til unit test
        public virtual bool Subscribe(int kundeId, int kategoriId, int aktiepakkeId)
        {
            using SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();

            using SqlTransaction transaction = conn.BeginTransaction();

            try
            {
                // tjekker om kunden allerede har abonnement i kategorien
                string alreadySubscribedSql = @"
            SELECT COUNT(*)
            FROM Abonnement
            WHERE KundeID = @KundeID
            AND KategoriID = @KategoriID";

                using (SqlCommand cmd = new SqlCommand(alreadySubscribedSql, conn, transaction))
                {
                    cmd.Parameters.AddWithValue("@KundeID", kundeId);
                    cmd.Parameters.AddWithValue("@KategoriID", kategoriId);

                    int alreadySubscribed = Convert.ToInt32(cmd.ExecuteScalar());

                    if (alreadySubscribed > 0)
                    {
                        transaction.Rollback();
                        return false;
                    }
                }

                int antalBrugere;
                int maxBrugere;
                byte[] rowVersion;

                // læser kategoriens nuværende værdier
                string getKategoriSql = @"
            SELECT AntalBrugere, MaxBrugere, RowVersion
            FROM Kategori
            WHERE KategoriID = @KategoriID";

                using (SqlCommand cmd = new SqlCommand(getKategoriSql, conn, transaction))
                {
                    cmd.Parameters.AddWithValue("@KategoriID", kategoriId);

                    using SqlDataReader reader = cmd.ExecuteReader();

                    if (!reader.Read())
                    {
                        transaction.Rollback();
                        return false;
                    }

                    antalBrugere = Convert.ToInt32(reader["AntalBrugere"]);
                    maxBrugere = Convert.ToInt32(reader["MaxBrugere"]);
                    rowVersion = (byte[])reader["RowVersion"];
                }

                if (antalBrugere >= maxBrugere)
                {
                    transaction.Rollback();
                    return false;
                }

                // optimistic concurrency:
                // opdaterer kun hvis RowVersion stadig er den samme som den vi læste
                string updateKategoriSql = @"
            UPDATE Kategori
            SET AntalBrugere = AntalBrugere + 1
            WHERE KategoriID = @KategoriID
            AND RowVersion = @RowVersion
            AND AntalBrugere < MaxBrugere";

                using (SqlCommand cmd = new SqlCommand(updateKategoriSql, conn, transaction))
                {
                    cmd.Parameters.AddWithValue("@KategoriID", kategoriId);
                    cmd.Parameters.Add("@RowVersion", System.Data.SqlDbType.Timestamp).Value = rowVersion;

                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected == 0)
                    {
                        transaction.Rollback();
                        return false;
                    }
                }

                // opretter abonnement
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

                // kobler abonnementet til aktiepakken
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
            using SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();

            string sql = @"
                SELECT TOP 1 KategoriID
                FROM Abonnement
                WHERE KundeID = @KundeID
                ORDER BY Dato DESC";

            using SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@KundeID", kundeId);

            object? result = cmd.ExecuteScalar();

            if (result == null || result == DBNull.Value)
                return null;

            return Convert.ToInt32(result);
        }

        public int CountByKategori(int kategoriId)
        {
            using SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();

            string sql = @"
                SELECT COUNT(*)
                FROM Abonnement
                WHERE KategoriID = @KategoriID";

            using SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@KategoriID", kategoriId);

            return Convert.ToInt32(cmd.ExecuteScalar());
        }
    }
}