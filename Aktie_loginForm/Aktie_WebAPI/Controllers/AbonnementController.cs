using Aktie_WebAPI;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/abonnement")]
public class AbonnementController : ControllerBase
{
    private readonly AbonnementService _service;

    public AbonnementController()
    {
        _service = new AbonnementService("DIN_CONNECTION_STRING");
    }

    [HttpPost("subscribe")]
    public IActionResult Subscribe(int kundeId, int pakkeId)
    {
        var success = _service.Subscribe(kundeId, pakkeId);

        if (success)
            return Ok(new { message = "Tilmeldt!" });

        return BadRequest(new { message = "Ingen pladser tilbage" });
    }
}