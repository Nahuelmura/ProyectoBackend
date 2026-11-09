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

        public async Task<IActionResult> ListadoPedido()
        {
            var pedidos = await _context.Pedidios.Include(p => p.Clientes). Select(p => new VistaPedido
            {
                NombreDelCliente = p.Clientes.Nombre,
                PedidoID = p.PedidoID,
                FechaFormateada = p.Fecha.ToString("dd/MM/yyyy"),
                EstadoPedido = p.Estado.ToString(),
                Total = p.Total,


            }).ToListAsync();
            return Ok(pedidos);
        }




        [HttpPost]
        public async Task<IActionResult> CrearPedidoConDetalles([FromBody] Pedido pedido)
        {
            try
            {

                // validamos que tenga un cliente asignado
                if (pedido.ClienteID == null || pedido.ClienteID <= 0)
                {
                    return BadRequest("Debe seleccionar un cliente válido.");
                }

                //  Validamos que 1 producto al menos
                if (pedido.DetallePedidos == null || !pedido.DetallePedidos.Any())
                {
                    return BadRequest("El pedido debe contener al menos un producto.");
                }

                // validamos que no haya precios negativos
                if (pedido.DetallePedidos.Any(d => d.Cantidad <= 0 || d.PrecioUnitario <= 0))
                {
                    return BadRequest("La cantidad y el precio de los productos deben ser mayores a cero.");
                }

                var total = pedido.DetallePedidos.Sum(d => d.Cantidad * d.PrecioUnitario);

               
                var nuevoPedido = new Pedido
                {
                    Nombre = pedido.Nombre,
                    Fecha = pedido.Fecha,
                    Estado = pedido.Estado,
                    ClienteID = pedido.ClienteID,
                    Total = total,
                    DetallePedidos = pedido.DetallePedidos.Select(d => new DetallePedido
                    {
                        ProductoID = d.ProductoID,
                        Cantidad = d.Cantidad,
                        PrecioUnitario = d.PrecioUnitario
                    }).ToList()
                };

                await _context.Pedidios.AddAsync(nuevoPedido);
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(CrearPedidoConDetalles), new { id = nuevoPedido.PedidoID }, new
                {
                    mensaje = "Pedido creado exitosamente",
                    pedidoID = nuevoPedido.PedidoID
                });

            }
            catch (Exception ex)
            {
                return new JsonResult(new { error = "Error al guardar: " + ex.Message }) { StatusCode = 500 };
            }
        }

    







      [HttpGet("productos")]
        public IActionResult ObtenerProductos()
        {
           
                var productos = _context.Productos.ToList();
                return Ok(productos); 
        
          
        }




}


}
