using System.ComponentModel.DataAnnotations;

namespace ApiPedido.Models;

public class Producto
{

    [Key]

    public int ProductoID { get; set; }

    public int CategoriaID { get; set; }
    public string? Nombre { get; set; }
    public string? Descripcion { get; set; }

    public decimal PrecioCosto { get; set; }

    public decimal PrecioVenta { get; set; }

    public int Stock { get; set; }

    public virtual Categoria? Categoria{ get; set; }

    public ICollection<DetallePedido>? DetallePedidos { get; set; }



}

public class VistaProducto
{
    public int ProductoID { get; set; }

    public string? NombreProducto { get; set; }
    public string? DescripcionProducto { get; set; }

    public decimal PrecioCostoProducto { get; set; }

    public decimal PrecioVentaProducto { get; set; }

    public int StockProducto { get; set; }



    public int CategoriaID { get; set; }

    public string? NombreCategoria { get; set; }

}