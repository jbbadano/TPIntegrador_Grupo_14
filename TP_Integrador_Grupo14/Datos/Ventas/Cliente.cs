using Newtonsoft.Json;
using System;

namespace Datos.Ventas
{
    public class Cliente
    {
        [JsonProperty("id")]
        public Guid Id { get; set; }

        [JsonProperty("nombre")]
        public string Nombre { get; set; }

        [JsonProperty("apellido")]
        public string Apellido { get; set; }

        [JsonProperty("dni")]
        public int Dni { get; set; }

        [JsonProperty("direccion")]
        public string Direccion { get; set; }

        [JsonProperty("telefono")]
        public string Telefono { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("fechaNacimiento")]
        public DateTime FechaNacimiento { get; set; }

        [JsonProperty("fechaAlta")]
        public DateTime FechaAlta { get; set; }

        [JsonProperty("fechaBaja")]
        public DateTime? FechaBaja { get; set; }

        [JsonProperty("host")]
        public string Host { get; set; }

        public string NombreCompleto => $"{Nombre} {Apellido}";
    }
} 