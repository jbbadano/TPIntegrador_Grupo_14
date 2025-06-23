using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos.Ventas
{
    public class Producto
    {
        [JsonProperty("id")]
        public Guid Id { get; set; }

        [JsonProperty("idCategoria")]
        public int IdCategoria { get; set; }

        [JsonProperty("nombre")]
        public string Nombre { get; set; }

        [JsonProperty("fechaAlta")]
        public DateTime FechaAlta { get; set; }

        [JsonProperty("fechaBaja")]
        public DateTime? FechaBaja { get; set; }

        [JsonProperty("precio")]
        public decimal Precio { get; set; }

        [JsonProperty("stock")]
        public int Stock { get; set; }

        // Esta propiedad no viene de la API, se usa para la lógica de la venta.
        public int Cantidad { get; set; }
    }
}
