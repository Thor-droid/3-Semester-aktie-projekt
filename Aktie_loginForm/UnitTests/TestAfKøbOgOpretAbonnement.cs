using Aktie_WebAPI.DatabaseAccess;
using Microsoft.Extensions.Configuration;
using Xunit;

namespace UnitTests
{

    //brug dette inden kørsel af testene

    //DELETE FROM AktiepakkeAbonnement
    //WHERE AbonnementID IN (
    //  SELECT AbonnementID FROM Abonnement WHERE KundeID = 19
    //);
    //DELETE FROM Abonnement
    //WHERE KundeID = 19;
    public class AbonnementIntegrationTests
    {
        private readonly AbonnementRepository repo;

        public AbonnementIntegrationTests()
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

            repo = new AbonnementRepository(config);
        }

        [Fact]
        public void Koeb_Basis_Pro_Premium_ReturnererTrue()
        {
            int kundeId = 19;

            bool basisResult = repo.Subscribe(kundeId, 1, 1);
            bool proResult = repo.Subscribe(kundeId, 2, 2);
            bool premiumResult = repo.Subscribe(kundeId, 3, 3);

            Assert.True(basisResult);
            Assert.True(proResult);
            Assert.True(premiumResult);
        }

        [Fact]
        public void Koeb_OpretterAbonnement_I_Database()
        {
            int kundeId = 19;
            int kategoriId = 1;
            int aktiepakkeId = 1;

            bool result = repo.Subscribe(kundeId, kategoriId, aktiepakkeId);

            Assert.True(result);

            int? kategoriFraDb = repo.GetKategoriByCustomer(kundeId);

            Assert.NotNull(kategoriFraDb);
            Assert.Equal(kategoriId, kategoriFraDb.Value);
        }
    }
}
