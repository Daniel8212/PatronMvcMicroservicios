using Microsoft.AspNetCore.Mvc;

namespace Notificaciones.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class NotificacionesController : ControllerBase
{
    private readonly ILogger<NotificacionesController> _logger;
public NotificacionesController(ILogger<NotificacionesController> logger)
    {
        _logger = logger;
    }

[HttpPost]
    public IActionResult Enviar ([FromBody] Notificacion notificacion)
    {
        _logger.LogInformation("Notificacion enviada a {Cliente} sobre el pedido {PedidoId}: {Mensaje}", notificacion.Cliente, notificacion.PedidoId, notificacion.Mensaje);
       ;
       return Ok(new { message = "Notificación enviada correctamente" });

    }
}
