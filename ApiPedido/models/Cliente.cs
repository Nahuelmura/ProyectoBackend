using System.ComponentModel.DataAnnotations;

namespace ApiPedido.Models;

public class Cliente
{

    [Key]
    public int ClienteID { get; set; }
    public string? Nombre { get; set; }

    public string? Apellido { get; set; }

    public string? Telefono { get; set; }
    public string? Email { get; set; }

    public string? Direccion { get; set; }


    public ICollection<Pedido>? Pedidios { get; set; }

    

    


}

public class VistaCliente

{

    public int ClienteID { get; set; }
    public string? NombreCompleto { get; set; }
    public string? Telefono { get; set; }
    public string? Email { get; set; }


    public List<VistaPedido> Pedidos { get; set; } = new List<VistaPedido>();
}