using Datos.Ventas;
using Persistencia.WebService.Utils;
using System.Collections.Generic;
using System.Net.Http;
using System.Linq;

namespace Persistencia
{
    public class VentaPersistencia
    {
        private const string ENDPOINT = "/api/Venta/AgregarVenta";

        public List<string> Guardar(Venta venta)
        {
            List<string> errores = new List<string>();

            foreach (var producto in venta.Productos)
            {
                var ventaItem = new
                {
                    idCliente = venta.IdCliente,
                    idUsuario = venta.IdUsuario,
                    idProducto = producto.Id,
                    cantidad = producto.Cantidad
                };

                string jsonRequest = Newtonsoft.Json.JsonConvert.SerializeObject(ventaItem);
                HttpResponseMessage response = WebHelper.Post(ENDPOINT, jsonRequest);

                if (!response.IsSuccessStatusCode)
                {
                    string errorMsg = response.Content.ReadAsStringAsync().Result;
                    errores.Add($"Error al guardar el producto {producto.Nombre}. Status code: {response.StatusCode}\n{errorMsg}");
                }
            }
            return errores;
        }
    }
}

