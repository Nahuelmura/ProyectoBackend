
using ApiPedido.Data;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiPedidos.Controllers
{
    [Microsoft.AspNetCore.Components.Route("api/[controller]")]
    [ApiController]
    public class CategoriaController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CategoriaController(ApplicationDbContext context)
        {
            _context = context;
        }

       
    

//     [HttpGet]
//  public async Task<IActionResult> ListadoCategoria()

//         {
//             var listaCategoria = await _context.Categorias.ToListAsync();
//             return Ok (listaCategoria);
            
//         }



// }


}


}