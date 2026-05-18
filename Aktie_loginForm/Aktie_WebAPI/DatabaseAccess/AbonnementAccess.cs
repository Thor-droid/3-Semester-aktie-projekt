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

            // Starter en transaction så alle queries bliver udført samlet.
            // Hvis noget fejler, rollbackes hele transaktionen.
            using SqlTransaction transaction = conn.BeginTransaction();

            try
            {
                // Tjekker først om kunden allerede har et abonnement
                // i den valgte kategori.
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

                // 
                // OPTIMISTIC CONCURRENCY
                //
                //
                // Her bliver optimistic concurrency håndteret.
                //
                // Ideen er:
                // Vi forsøger kun at opdatere kategorien HVIS der stadig er plads.
                //
                // WHERE AntalBrugere < MaxBrugere sørger for,
                // at flere brugere ikke kan overskride max-grænsen samtidig.
                //
                // Hvis to brugere prøver at subscribe på samme tid,
                // vil kun den første request kunne opdatere rækken,
                // når MaxBrugere er nået.
                //
                // ExecuteNonQuery returnerer antal rækker der blev ændret.
                //
                // Hvis rowsAffected == 0 betyder det:
                // - kategorien var allerede fuld
                // - eller en anden bruger nåede at tage den sidste plads først
                //
                // På den måde undgår vi race conditions
                // uden at låse hele tabellen manuelt.
                //
                string updateKategoriSql = @"
            UPDATE Kategori 
            SET AntalBrugere = AntalBrugere + 1 
            WHERE KategoriID = @KategoriID
            AND AntalBrugere < MaxBrugere";

                using (SqlCommand cmd = new SqlCommand(updateKategoriSql, conn, transaction))
                {
                    cmd.Parameters.AddWithValue("@KategoriID", kategoriId);

                    int rowsAffected = cmd.ExecuteNonQuery();

                    // Hvis ingen rækker blev opdateret,
                    // rollbackes transaktionen.
                    if (rowsAffected == 0)
                    {
                        transaction.Rollback();
                        return false;
                    }
                }

                // Opretter abonnementet
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

                // Kobler abonnementet sammen med aktiepakken
                string linkSql = @"
            INSERT INTO AktiepakkeAbonnement (AktiepakkeID, AbonnementID) 
            VALUES (@AktiepakkeID, @AbonnementID)";

                using (SqlCommand cmd = new SqlCommand(linkSql, conn, transaction))
                {
                    cmd.Parameters.AddWithValue("@AktiepakkeID", aktiepakkeId);
                    cmd.Parameters.AddWithValue("@AbonnementID", abonnementId);

                    cmd.ExecuteNonQuery();
                }

                // Hvis alt lykkedes commits transaktionen
                transaction.Commit();
                return true;
            }
            catch
            {
                // Hvis noget fejler rollbackes hele transaktionen
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