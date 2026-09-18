using Microsoft.AspNetCore.Mvc;
using Pedidos.Api.Models;

namespace Pedidos.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PedidosController : ControllerBase
{

    private static readonly List<Pedido> _pedidos = new ();
    private static int _siguienteId = 1;

    private readonly IHttpClientFactory _httpClientFactory;
    private readonly ILogger<PedidosController> _logger;

    public PedidosController(IHttpClientFactory httpClientFactory, ILogger<PedidosController> logger)
    {
        _httpClientFactory = httpClientFactory;
        _logger = logger;
    }
    
    [HttpGet]
    public IActionResult GetAll() => Ok(_pedidos);

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] Pedido pedido)
    {
        if (pedido == null)
        {
            return BadRequest("El pedido no puede ser nulo.");
        }

        pedido.Id = _siguienteId++;
        pedido.Fecha = DateTime.UtcNow;
        _pedidos.Add(pedido);

        try
        {
            var client = _httpClientFactory.CreateClient("Notificaciones");
            await client.PostAsJsonAsync("/api/notificaciones", new
            {
               PedidoId = pedido.Id,
               Cliente = pedido.Cliente,
               Mensaje = $"Tu pedido fue recibido" 
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "No se pudo enviar la notificación para el pedido");
        }
        return CreatedAtAction(nameof(GetAll), new { id = pedido.Id }, pedido);
        }
    }

}

