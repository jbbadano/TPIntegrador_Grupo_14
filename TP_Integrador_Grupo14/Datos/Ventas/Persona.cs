using System;
using System.Globalization;

namespace Datos.Ventas
{
    public class Persona
    {
        public string Legajo { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string DNI { get; set; }
        public DateTime FechaIngreso { get; set; }

        public string NombreCompleto => $"{Nombre} {Apellido}";

        public Persona(string registroCsv)
        {
            string[] datos = registroCsv.Split(';');
            if (datos.Length >= 5)
            {
                Legajo = datos[0];
                Nombre = datos[1];
                Apellido = datos[2];
                DNI = datos[3];
                FechaIngreso = DateTime.ParseExact(datos[4], "d/M/yyyy", CultureInfo.InvariantCulture);
            }
        }

        public string ToCsv()
        {
            return $"{Legajo};{Nombre};{Apellido};{DNI};{FechaIngreso:d/M/yyyy}";
        }
    }
} 