using ApiPedido.Data;
using ApiPedido.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiPedidos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InformeController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public InformeController(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpGet("InformeClientes")]


        public async Task<IActionResult> ObtenerInforme()
        {
            var clientes = await _context.Clientes.ToListAsync();
            var todosLosPedidos = await _context.Pedidios.ToListAsync(); 

            List<VistaCliente> listaClientes = new List<VistaCliente>();

            foreach (var c in clientes)
            {
                // filtramos los pedidos por id cliente
                var pedidosDelCliente = todosLosPedidos
                    .Where(p => p.ClienteID == c.ClienteID)
                    .ToList();

                var vistaCliente = new VistaCliente
                {
                    ClienteID = c.ClienteID,             
                    NombreCompleto = c.Nombre,
                    Telefono = c.Telefono,
                    
                    Pedidos = pedidosDelCliente.Select(p => new VistaPedido
                    {
                        PedidoID = p.PedidoID,
                        NombreDelCliente = c.Nombre,
                        EstadoPedido = p.Estado.ToString(),
                        FechaFormateada = p.Fecha.ToString("dd/MM/yyyy HH:mm"),
                        Total = p.Total
                     
                    }).ToList()
                };

                listaClientes.Add(vistaCliente);
            }

            return Ok(listaClientes);
        }

    }
    

    }