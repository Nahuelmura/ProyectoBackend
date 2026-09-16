using ApiPedido.Data;
using ApiPedido.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiPedidos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PedidoController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public PedidoController(ApplicationDbContext context)
        {
            _context = context;
        }




        

        [HttpGet]
        
public async Task <IActionResult> ListadoPedido ()
        {
            var pedidos = await _context.Pedidios.Select(p => new VistaPedido
            {
                PedidoID = p.PedidoID,
                NombreDelCliente = p.Nombre,
                FechaFormateada = p.Fecha.ToString("dd/MM/yyyy"),
                EstadoPedido = p.Estado.ToString()


            }).ToListAsync();
            return Ok(pedidos);
        }
        }
        

        }


    