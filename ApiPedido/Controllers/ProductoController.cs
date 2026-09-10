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
    
    

        [HttpPost]

        public async Task<IActionResult> CrearProducto([FromBody] Producto producto)
        {

            
            var existeCategoria = await _context.Categorias.AnyAsync(c => c.CategoriaID == producto.CategoriaID);

            if (!existeCategoria)
            {
                return BadRequest($"Error: La categoría con ID {producto.CategoriaID} no existe.");
            }

            var nuevoProducto = new Producto
            {

                Nombre = producto.Nombre,
                PrecioCosto = producto.PrecioCosto,
                PrecioVenta = producto.PrecioVenta,
                Stock = producto.Stock,
                CategoriaID = producto.CategoriaID,
                Descripcion = producto.Descripcion,
            };
            {

                _context.Add(nuevoProducto);
                await _context.SaveChangesAsync();

                return Ok("Producto guardado exitosamente");
            }
        }

    
    

    
        [HttpPut("{productoid}")]
        public async Task<IActionResult> EditarProducto(int productoid, [FromBody] Producto producto)
        {
            if (producto.Nombre == null)
            {
                return NotFound("El nombre esta vacio");
            }

            var NombreMayuscula = producto.Nombre.ToUpper().Trim();

            var editarProducto = await _context.Productos.Where(p => p.ProductoID == productoid).FirstOrDefaultAsync();

            if (editarProducto == null)
            {
                return NotFound("El producto no existe ");

            }

            var existeNombre = await _context.Productos.Where(p => p.Nombre == NombreMayuscula && p.ProductoID != productoid).AnyAsync();
            if (!existeNombre)
            {
                editarProducto.Nombre = NombreMayuscula;
                editarProducto.CategoriaID = producto.CategoriaID;
                editarProducto.PrecioCosto = producto.PrecioCosto;
                editarProducto.PrecioVenta = producto.PrecioVenta;
                editarProducto.Stock = producto.Stock;
                await _context.SaveChangesAsync();

                return Ok("Producto Editado exitosamente");
            }
            return NotFound("Ya existe otro producto con ese nombre");
        }





    


    

    }
    

    }
