using Aktie_WebAPI.Service;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/abonnement")]
public class AbonnementController : ControllerBase
{
    private string connectionString =
        "Data Source=hildur.ucn.dk;Initial Catalog=DMA-CSD-V251_10665995;User ID=DMA-CSD-V251_10665995;Password=Password1!;TrustServerCertificate=True";

    private readonly AbonnementService _service;

    public AbonnementController()
    {
        _service = new AbonnementService(connectionString);
    }

    [HttpPost("subscribe")]
    public IActionResult Subscribe(int kundeId, int kategoriId, int aktiepakkeId)
    {
        var success = _service.Subscribe(kundeId, kategoriId, aktiepakkeId);

        if (success)
            return Ok(new { message = "Tilmeldt!" });

        return BadRequest(new { message = "Ingen pladser tilbage eller fejl" });
    }

    [HttpGet("getByCustomer")]
    public IActionResult GetByCustomer(int kundeId)
    {
        var kategoriId = _service.GetKategoriByCustomer(kundeId);

        if (kategoriId == null)
            return NotFound();

        return Ok(new { kategoriId = kategoriId });
    }
}