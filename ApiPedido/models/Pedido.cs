using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiPedido.Models;

public class Pedido
{

    [Key]
    public int PedidoID { get; set; }

    public int? ClienteID { get; set; }
    public string? Nombre { get; set; }
    public Estado Estado { get; set; }

    public DateTime Fecha { get; set; }

    public decimal Total { get; set; }



    public ICollection<DetallePedido>? DetallePedidos { get; set; }

    [ForeignKey("ClienteID")]
    public virtual Cliente? Clientes { get; set; }



}



public enum Estado
{
    Pendiente,
    Envidado,

    Entregado
}

public class VistaPedido
{
    public int PedidoID { get; set; }
    public string? NombreDelCliente { get; set; }
    public string EstadoPedido { get; set; }

    public string FechaFormateada { get; set; }

    public decimal Total { get; set; }

}



