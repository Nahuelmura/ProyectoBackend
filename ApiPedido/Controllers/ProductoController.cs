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
        

        
       



//obtenemos categorias que son usadas para crear un producto
        [HttpGet("idCategorias")]
        public async Task<IActionResult> ObtenerCategoria()
        {
            var categorias = await _context.Categorias
                .OrderBy(c => c.Nombre) // le decimos que la ordene por nombre
                .Select(c => new
                {
                    id = c.CategoriaID,
                    nombre = c.Nombre
                })
                .ToListAsync();

            return Ok(categorias);
        }


    

    


    }
    

    }
