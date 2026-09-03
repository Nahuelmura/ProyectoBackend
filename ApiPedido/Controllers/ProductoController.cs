using ApiPedido.Data;
using ApiPedido.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiPedidos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductoController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public ProductoController(ApplicationDbContext context)
        {
            _context = context;
        }
        

        
        

[HttpGet]
public async Task<IActionResult> ListadoProducto()
        {
            var listadoProducto = await _context.Productos.Include(p => p.Categoria).ToListAsync();

            var ProductoMostrar = listadoProducto.Select(p => new VistaProducto
            {
                ProductoID = p.ProductoID,
                NombreProducto = p.Nombre,
                DescripcionProducto = p.Descripcion,
                PrecioCostoProducto = p.PrecioCosto,
                PrecioVentaProducto = p.PrecioVenta,
                StockProducto = p.Stock,
                CategoriaID = p.CategoriaID,

                NombreCategoria = p.Categoria.Nombre


            }).ToList();
            return Ok(ProductoMostrar);

        }
    }
    

    }
