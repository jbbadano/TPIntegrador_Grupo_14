using Datos;
using Persistencia.DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Negocio
{
    public class LoginService
    {
        private DataBaseUtils _dataBaseUtils;
        private const int MAX_INTENTOS = 3;

        public LoginService()
        {
            _dataBaseUtils = new DataBaseUtils();
        }

        public ResultadoLogin Autenticar(string nombreUsuario, string contrasena)
        {
            try
            {
                // 1. Buscar credencial por nombre de usuario
                Credencial credencial = BuscarCredencialPorUsuario(nombreUsuario);
                if (credencial == null)
                {
                    return new ResultadoLogin { Exitoso = false, Mensaje = "Usuario no encontrado" };
                }

                // 2. Verificar si el usuario está bloqueado
                if (EstaUsuarioBloqueado(credencial.Legajo))
                {
                    return new ResultadoLogin { Exitoso = false, Mensaje = "Usuario bloqueado. Contacte al administrador." };
                }

                // 3. Validar contraseña
                if (!credencial.Contrasena.Equals(contrasena))
                {
                    RegistrarIntentoFallido(credencial.Legajo);
                    int intentos = ContarIntentosFallidos(credencial.Legajo);

                    if (intentos >= MAX_INTENTOS)
                    {
                        BloquearUsuario(credencial.Legajo);
                        return new ResultadoLogin { Exitoso = false, Mensaje = "Usuario bloqueado por exceder intentos máximos" };
                    }

                    return new ResultadoLogin
                    {
                        Exitoso = false,
                        Mensaje = $"Contraseña incorrecta. Intentos restantes: {MAX_INTENTOS - intentos}"
                    };
                }

                // 4. Login exitoso - limpiar intentos fallidos
                LimpiarIntentosFallidos(credencial.Legajo);

                // 5. Verificar si es primer login
                if (credencial.EsPrimerLogin())
                {
                    return new ResultadoLogin
                    {
                        Exitoso = true,
                        Credencial = credencial,
                        RequiereCambioContrasena = true,
                        Mensaje = "Primer login - debe cambiar su contraseña"
                    };
                }

                // 6. Verificar si la contraseña está expirada
                if (credencial.ContrasenaExpirada())
                {
                    return new ResultadoLogin
                    {
                        Exitoso = true,
                        Credencial = credencial,
                        RequiereCambioContrasena = true,
                        Mensaje = "Su contraseña ha expirado - debe cambiarla"
                    };
                }

                // 7. Login normal - actualizar último login
                credencial.ActualizarUltimoLogin();
                ActualizarCredencial(credencial);

                return new ResultadoLogin
                {
                    Exitoso = true,
                    Credencial = credencial,
                    RequiereCambioContrasena = false,
                    Mensaje = "Login exitoso"
                };

            }
            catch (Exception ex)
            {
                return new ResultadoLogin { Exitoso = false, Mensaje = $"Error en autenticación: {ex.Message}" };
            }
        }

        public ResultadoCambioContrasena CambiarContrasena(string legajo, string contrasenaActual, string contrasenaNueva)
        {
            try
            {
                // Validaciones básicas
                if (string.IsNullOrWhiteSpace(contrasenaNueva) || contrasenaNueva.Length < 8)
                {
                    return new ResultadoCambioContrasena
                    {
                        Exitoso = false,
                        Mensaje = "La nueva contraseña debe tener al menos 8 caracteres"
                    };
                }

                Credencial credencial = BuscarCredencialPorLegajo(legajo);
                if (credencial == null)
                {
                    return new ResultadoCambioContrasena { Exitoso = false, Mensaje = "Usuario no encontrado" };
                }

                // Validar contraseña actual (solo si no es primer login)
                if (!credencial.EsPrimerLogin() && !credencial.Contrasena.Equals(contrasenaActual))
                {
                    return new ResultadoCambioContrasena { Exitoso = false, Mensaje = "Contraseña actual incorrecta" };
                }

                // Validar que la nueva contraseña sea diferente
                if (credencial.Contrasena.Equals(contrasenaNueva))
                {
                    return new ResultadoCambioContrasena
                    {
                        Exitoso = false,
                        Mensaje = "La nueva contraseña debe ser diferente a la actual"
                    };
                }

                // Cambiar contraseña
                credencial.Contrasena = contrasenaNueva;
                credencial.ActualizarUltimoLogin();
                ActualizarCredencial(credencial);

                return new ResultadoCambioContrasena
                {
                    Exitoso = true,
                    Mensaje = "Contraseña cambiada exitosamente"
                };

            }
            catch (Exception ex)
            {
                return new ResultadoCambioContrasena
                {
                    Exitoso = false,
                    Mensaje = $"Error al cambiar contraseña: {ex.Message}"
                };
            }
        }

        public Credencial ObtenerCredencial(string legajo)
        {
            return BuscarCredencialPorLegajo(legajo);
        }

        public bool DesbloquearUsuario(string legajo)
        {
            try
            {
                if (EstaUsuarioBloqueado(legajo))
                {
                    _dataBaseUtils.BorrarRegistro(legajo, "usuario_bloqueado.csv");
                    LimpiarIntentosFallidos(legajo); // También limpiamos los intentos para que no se vuelva a bloquear
                    return true;
                }
                return false; // No estaba bloqueado
            }
            catch (Exception)
            {
                return false;
            }
        }

        #region Métodos privados

        private Credencial BuscarCredencialPorUsuario(string nombreUsuario)
        {
            List<string> registros = _dataBaseUtils.BuscarRegistrosPorColumna("credenciales.csv", 1, nombreUsuario);

            if (registros.Count > 0)
            {
                return new Credencial(registros[0]);
            }
            return null;
        }

        private Credencial BuscarCredencialPorLegajo(string legajo)
        {
            string registro = _dataBaseUtils.BuscarRegistroPorId("credenciales.csv", legajo);

            if (registro != null)
            {
                return new Credencial(registro);
            }
            return null;
        }

        private void ActualizarCredencial(Credencial credencial)
        {
            _dataBaseUtils.ActualizarRegistro("credenciales.csv", credencial.Legajo, credencial.ToCsvString());
        }

        private bool EstaUsuarioBloqueado(string legajo)
        {
            return _dataBaseUtils.ExisteRegistro("usuario_bloqueado.csv", legajo);
        }

        private void BloquearUsuario(string legajo)
        {
            if (!EstaUsuarioBloqueado(legajo))
            {
                _dataBaseUtils.AgregarRegistro("usuario_bloqueado.csv", legajo);
            }
        }

        private void RegistrarIntentoFallido(string legajo)
        {
            string registro = $"{legajo};{DateTime.Now:d/M/yyyy}";
            _dataBaseUtils.AgregarRegistro("login_intentos.csv", registro);
        }

        private int ContarIntentosFallidos(string legajo)
        {
            List<string> intentos = _dataBaseUtils.BuscarRegistrosPorColumna("login_intentos.csv", 0, legajo);
            return intentos.Count;
        }

        private void LimpiarIntentosFallidos(string legajo)
        {
            // Eliminar todos los registros de intentos para este legajo
            List<string> todosLosIntentos = _dataBaseUtils.BuscarRegistro("login_intentos.csv");
            List<string> intentosLimpios = new List<string>();

            foreach (string intento in todosLosIntentos)
            {
                string[] campos = intento.Split(';');
                if (campos.Length > 0 && campos[0] != legajo)
                {
                    intentosLimpios.Add(intento);
                }
            }

            // Reescribir archivo sin los intentos del usuario
            System.IO.File.WriteAllLines(
                System.IO.Path.Combine(_dataBaseUtils.GetType().GetField("_rutaBase",
                System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
                ?.GetValue(_dataBaseUtils).ToString(), "login_intentos.csv"),
                intentosLimpios);
        }

        #endregion
    }

    // Clases para manejar resultados
    public class ResultadoLogin
    {
        public bool Exitoso { get; set; }
        public string Mensaje { get; set; }
        public Credencial Credencial { get; set; }
        public bool RequiereCambioContrasena { get; set; }
    }

    public class ResultadoCambioContrasena
    {
        public bool Exitoso { get; set; }
        public string Mensaje { get; set; }
    }
}