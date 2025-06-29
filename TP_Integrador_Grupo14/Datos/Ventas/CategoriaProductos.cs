﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Datos.Ventas
{
    public class CategoriaProductos
    {
        String _id;
        String _descripcion;

        public string Id { get => _id; set => _id = value; }
        public string Descripcion { get => _descripcion; set => _descripcion = value; }

        public CategoriaProductos(string id, string descripcion)
        {
            _id = id;
            _descripcion = descripcion;
        }

        public override string ToString()
        {
            return _descripcion;
        }
    }
}
