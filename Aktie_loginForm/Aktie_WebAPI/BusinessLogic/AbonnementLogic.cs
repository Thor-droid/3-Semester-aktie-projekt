using Aktie_WebAPI.DatabaseAccess;

namespace Aktie_WebAPI.BusinessLogic
{
    public class AbonnementLogic
    {
        private readonly AbonnementAccess _abonnementAccess;

        public AbonnementLogic(AbonnementAccess _abonnementAccess)
        {
            this._abonnementAccess = _abonnementAccess;
        }

        public bool Subscribe(int kundeId, int kategoriId, int aktiepakkeId)
        {
            if (kundeId <= 0 || kategoriId <= 0 || aktiepakkeId <= 0)
                return false;

            return _abonnementAccess.Subscribe(kundeId, kategoriId, aktiepakkeId);
        }

        public int? GetKategoriByCustomer(int kundeId)
        {
            if (kundeId <= 0)
                return null;

            return _abonnementAccess.GetKategoriByCustomer(kundeId);
        }

        public int CountByKategori(int kategoriId)
        {
            if (kategoriId <= 0)
                return 0;

            return _abonnementAccess.CountByKategori(kategoriId);
        }
    }
}