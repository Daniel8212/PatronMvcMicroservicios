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

}

