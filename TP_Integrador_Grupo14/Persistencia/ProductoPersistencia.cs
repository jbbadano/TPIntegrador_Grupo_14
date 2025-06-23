using Datos.Ventas;
using Newtonsoft.Json;
using Persistencia.WebService.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Persistencia
{
    public class ProductoPersistencia
    {
        public List<Producto> obtenerProductosPorCategoria(string categoriaId)
        {
            string endpoint = $"/api/Producto/TraerProductosPorCategoria?catnum={categoriaId}";
            HttpResponseMessage response = WebHelper.Get(endpoint);

            if (response.IsSuccessStatusCode)
            {
                var contentStream = response.Content.ReadAsStringAsync().Result;
                return JsonConvert.DeserializeObject<List<Producto>>(contentStream);
            }
            
            return new List<Producto>();
        }

        public List<CategoriaProductos> obtenerCategorias()
        {
            return new List<CategoriaProductos>
            {
                new CategoriaProductos("1", "Audio"),
                new CategoriaProductos("2", "Celulares"),
                new CategoriaProductos("3", "Electro Hogar"),
                new CategoriaProductos("4", "Informática"),
                new CategoriaProductos("5", "Smart TV")
            };
        }
    }
}
