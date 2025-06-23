using Datos.Ventas;
using Persistencia;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Negocio
{
    public class VentasNegocio
    {
        private ProductoPersistencia _productoPersistencia;
        private VentaPersistencia _ventaPersistencia;

        public VentasNegocio()
        {
            _productoPersistencia = new ProductoPersistencia();
            _ventaPersistencia = new VentaPersistencia();
        }

        public List<Cliente> obtenerClientes()
        {
            string endpoint = "/api/Cliente/GetClientes";
            var response = Persistencia.WebService.Utils.WebHelper.Get(endpoint);
            if (response.IsSuccessStatusCode)
            {
                var contentStream = response.Content.ReadAsStringAsync().Result;
                return Newtonsoft.Json.JsonConvert.DeserializeObject<List<Cliente>>(contentStream);
            }
            return new List<Cliente>();
        }

        public List<CategoriaProductos> obtenerCategoriaProductos()
        {
            return _productoPersistencia.obtenerCategorias();
        }

        public List<Producto> obtenerProductosPorCategoria(string categoriaId)
        {
            return _productoPersistencia.obtenerProductosPorCategoria(categoriaId);
        }

        public List<string> procesarVenta(Venta venta)
        {
            return _ventaPersistencia.Guardar(venta);
        }
    }
}
