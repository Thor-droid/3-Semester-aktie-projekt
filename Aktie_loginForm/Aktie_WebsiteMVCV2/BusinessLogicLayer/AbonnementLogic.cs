using Aktie_WebsiteMVCV2.Models;
using Aktie_WebsiteMVCV2.Services;

namespace Aktie_WebsiteMVCV2.BusinessLogicLayer
{
    public class AbonnementLogic
    {
        private readonly AbonnementApiService _abonnementApiService;

        public AbonnementLogic(AbonnementApiService abonnementApiService)
        {
            _abonnementApiService = abonnementApiService;
        }

        public async Task<AbonnementOutcome> buyAbonnement(string pakkeId, int kundeId)
        {

            int kategoriId = pakkeId switch
            {
                "Basis" => 1,
                "Pro" => 2,
                "Premium" => 3,
                _ => 1
            };
            int aktiepakkeId = kategoriId;

            var response = await _abonnementApiService.Subscribe(kundeId, kategoriId, aktiepakkeId);

            if (response.IsSuccessStatusCode)
            {
               return new AbonnementOutcome
                {
                    Aktiepakke = pakkeId,
                    KategoriId = kategoriId,
                    Message = "Abonnement oprettet succesfuldt"
                };
            }

            return new AbonnementOutcome
            {
                Aktiepakke = pakkeId,
                KategoriId = kategoriId,
                Message = "Kunne ikke oprette abonnement"
            };
        }

    }
}
