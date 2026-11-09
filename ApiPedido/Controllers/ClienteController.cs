using ApiPedido.Data;
using ApiPedido.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiPedidos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClienteController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ClienteController(ApplicationDbContext context)
        {
            _context = context;
        }



        [HttpGet]
        public async Task<IActionResult> ListadoClientes()
        {
            var clientes = await _context.Clientes.ToListAsync();

            return Ok(clientes);
        }



        [HttpPost]
        public async Task<IActionResult> CrearCliente([FromBody] Cliente cliente)
        {
            var nombreMayuscula = cliente.Nombre?.Trim().ToUpper();

                var nuevoCliente = new Cliente
                {
                    Nombre = nombreMayuscula,
                    Apellido = cliente.Apellido,
                    Telefono = cliente.Telefono,
                    Email = cliente.Email,
                    Direccion = cliente.Direccion,

                };
                _context.Add(nuevoCliente);
                await _context.SaveChangesAsync();
                return Ok("Cliente guardado");
            



            return Ok();
        }







        [HttpPut("{clienteID}")]
        public async Task<IActionResult> EditarCliente(int clienteID, [FromBody] Cliente cliente)
        {
            var nombreMayuscula = cliente.Nombre?.Trim().ToUpper(); // guardamos el nombre en mayuscula

            var editarCliente = await _context.Clientes.Where(e => e.ClienteID == clienteID).SingleOrDefaultAsync();
            // le decimos que busque en el contexto de categorias un id que coincida con el parametro que le esto pasando

            if (editarCliente == null)
            {
                return Ok("el cliente que quiere editar no existe");
            }
            ;

            var existeNombre = await _context.Clientes.AnyAsync(e => e.Nombre == nombreMayuscula && e.ClienteID != clienteID);

            // si Nombre es igual a la variable NombreMayuscula y  que sea distintp al id guardado
            if (!existeNombre)
            {
                editarCliente.Nombre = nombreMayuscula;
                await _context.SaveChangesAsync();

                return Ok("cliente editado exitosamente");
            }

            return Ok("Ya existe otro cliente con ese nombre");
        }





    }



}
