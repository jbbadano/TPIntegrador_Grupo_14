using System;

namespace Datos.Ventas
{
    public class ProductoCarrito
    {
        public Guid IdProducto { get; set; }
        public string Nombre { get; set; }
        public decimal Precio { get; set; }
        public int Cantidad { get; set; }

        public ProductoCarrito(Guid idProducto, string nombre, decimal precio, int cantidad)
        {
            IdProducto = idProducto;
            Nombre = nombre;
            Precio = precio;
            Cantidad = cantidad;
        }
    }
} 