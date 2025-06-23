using System;
using System.Collections.Generic;

namespace Datos.Ventas
{
    public class Venta
    {
        public Guid Id { get; set; }
        public Guid IdCliente { get; set; }
        public string IdUsuario { get; set; }
        public List<Producto> Productos { get; set; }
        public decimal Total { get; set; }
        public DateTime FechaVenta { get; set; }

        public Venta()
        {
            Id = Guid.NewGuid();
            FechaVenta = DateTime.Now;
            Productos = new List<Producto>();
        }
    }
} 