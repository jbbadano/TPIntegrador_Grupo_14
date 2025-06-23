using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos
{
    public class Credencial
    {
        private String _legajo;
        private String _nombreUsuario;
        private String _contrasena;
        private DateTime _fechaAlta;
        private DateTime? _fechaUltimoLogin; // Nullable para manejar casos sin login previo

        public string Legajo { get => _legajo; set => _legajo = value; }
        public string NombreUsuario { get => _nombreUsuario; set => _nombreUsuario = value; }
        public string Contrasena { get => _contrasena; set => _contrasena = value; }
        public DateTime FechaAlta { get => _fechaAlta; set => _fechaAlta = value; }
        public DateTime? FechaUltimoLogin { get => _fechaUltimoLogin; set => _fechaUltimoLogin = value; }

        // Constructor que maneja el parsing desde CSV
        public Credencial(String registro)
        {
            String[] datos = registro.Split(';');

            if (datos.Length < 4)
                throw new ArgumentException("Registro de credencial inválido - faltan campos");

            this._legajo = datos[0];
            this._nombreUsuario = datos[1];
            this._contrasena = datos[2];
            this._fechaAlta = DateTime.ParseExact(datos[3], "d/M/yyyy", CultureInfo.InvariantCulture);

            // Maneja fecha de último login que puede estar vacía
            if (datos.Length > 4 && !string.IsNullOrWhiteSpace(datos[4]))
            {
                this._fechaUltimoLogin = DateTime.ParseExact(datos[4], "d/M/yyyy", CultureInfo.InvariantCulture);
            }
            else
            {
                this._fechaUltimoLogin = null; // Primer login
            }
        }

        // Constructor para crear credenciales nuevas
        public Credencial(string legajo, string nombreUsuario, string contrasena)
        {
            this._legajo = legajo;
            this._nombreUsuario = nombreUsuario;
            this._contrasena = contrasena;
            this._fechaAlta = DateTime.Now;
            this._fechaUltimoLogin = null;
        }

        // Métodos útiles para el negocio
        public bool EsPrimerLogin()
        {
            return _fechaUltimoLogin == null;
        }

        public bool ContrasenaExpirada()
        {
            if (_fechaUltimoLogin == null) return false; // Primer login, no está expirada

            TimeSpan diferencia = DateTime.Now - _fechaUltimoLogin.Value;
            return diferencia.TotalDays > 30;
        }

        public void ActualizarUltimoLogin()
        {
            _fechaUltimoLogin = DateTime.Now;
        }

        // Para guardar en CSV
        public string ToCsvString()
        {
            string fechaLogin = _fechaUltimoLogin?.ToString("d/M/yyyy") ?? "";
            return $"{_legajo};{_nombreUsuario};{_contrasena};{_fechaAlta:d/M/yyyy};{fechaLogin}";
        }
    }
}