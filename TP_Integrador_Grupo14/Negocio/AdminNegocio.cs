using Datos.Ventas;
using Persistencia;
using Persistencia.DataBase;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Negocio
{
    public class SolicitudCambioPersona
    {
        public string IdOperacion { get; set; }
        public string LegajoSupervisor { get; set; }
        public DateTime FechaSolicitud { get; set; }
        public string LegajoPersona { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string DNI { get; set; }
        public DateTime FechaIngreso { get; set; }
    }

    public class AdminNegocio
    {
        private DataBaseUtils _db;

        public AdminNegocio()
        {
            _db = new DataBaseUtils();
        }

        public List<SolicitudCambioPersona> ObtenerSolicitudesPendientes()
        {
            var operaciones = _db.BuscarRegistro("operaciones.csv")
                .Select(r => r.Split(';'))
                .Where(d => d.Length >= 4 && d[3] == "MOD_PERSONA")
                .ToDictionary(d => d[0], d => new { LegajoSup = d[1], Fecha = DateTime.Parse(d[2]) });

            var cambios = _db.BuscarRegistro("operacion_cambio_persona.csv")
                .Select(r => r.Split(';'))
                .Where(d => d.Length >= 6 && operaciones.ContainsKey(d[0]));

            return cambios.Select(d => new SolicitudCambioPersona
            {
                IdOperacion = d[0],
                LegajoSupervisor = operaciones[d[0]].LegajoSup,
                FechaSolicitud = operaciones[d[0]].Fecha,
                LegajoPersona = d[1],
                Nombre = d[2],
                Apellido = d[3],
                DNI = d[4],
                FechaIngreso = DateTime.Parse(d[5])
            }).ToList();
        }

        public bool AprobarSolicitud(string idOperacion)
        {
            var solicitud = ObtenerSolicitudesPendientes().FirstOrDefault(s => s.IdOperacion == idOperacion);
            if (solicitud == null) return false;

            LimpiarOperacion(idOperacion);
            return true;
        }

        public bool RechazarSolicitud(string idOperacion)
        {
            LimpiarOperacion(idOperacion);
            return true;
        }

        private void LimpiarOperacion(string idOperacion)
        {
            _db.BorrarRegistro(idOperacion, "operaciones.csv");
            _db.BorrarRegistro(idOperacion, "operacion_cambio_persona.csv");
        }
    }
} 