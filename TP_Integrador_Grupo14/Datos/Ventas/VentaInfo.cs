using Newtonsoft.Json;
using System;

namespace Datos.Ventas
{
    public class VentaInfo
    {
        [JsonProperty("id")]
        public Guid Id { get; set; }

        [JsonProperty("cantidad")]
        public int Cantidad { get; set; }

        [JsonProperty("fechaAlta")]
        public DateTime FechaAlta { get; set; }

        [JsonProperty("estado")]
        public int Estado { get; set; }
    }
} 