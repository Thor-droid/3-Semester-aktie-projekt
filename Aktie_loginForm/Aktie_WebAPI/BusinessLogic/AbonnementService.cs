using Aktie_WebAPI.DatabaseAccess;

namespace Aktie_WebAPI.BusinessLogic
{
    public class AbonnementService
    {
        private readonly AbonnementRepository abonnementRepository;

        public AbonnementService(AbonnementRepository abonnementRepository)
        {
            this.abonnementRepository = abonnementRepository;
        }

        public bool Subscribe(int kundeId, int kategoriId, int aktiepakkeId)
        {
            if (kundeId <= 0 || kategoriId <= 0 || aktiepakkeId <= 0)
                return false;

            return abonnementRepository.Subscribe(kundeId, kategoriId, aktiepakkeId);
        }

        public int? GetKategoriByCustomer(int kundeId)
        {
            if (kundeId <= 0)
                return null;

            return abonnementRepository.GetKategoriByCustomer(kundeId);
        }
    }
}