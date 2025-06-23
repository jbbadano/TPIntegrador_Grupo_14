using Persistencia.DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class PerfilService
    {
        private DataBaseUtils _dataBaseUtils;

        public PerfilService()
        {
            _dataBaseUtils = new DataBaseUtils();
        }

        public PerfilUsuario ObtenerPerfilUsuario(string legajo)
        {
            try
            {
                // Buscar el perfil del usuario
                List<string> usuariosPerfiles = _dataBaseUtils.BuscarRegistrosPorColumna("usuario_perfil.csv", 0, legajo);

                if (usuariosPerfiles.Count == 0)
                {
                    return new PerfilUsuario { Legajo = legajo, TienePerfil = false };
                }

                string[] datos = usuariosPerfiles[0].Split(';');
                int idPerfil = int.Parse(datos[1]);

                // Obtener información del perfil
                Perfil perfil = ObtenerPerfil(idPerfil);
                if (perfil == null)
                {
                    return new PerfilUsuario { Legajo = legajo, TienePerfil = false };
                }

                // Obtener roles del perfil
                List<Rol> roles = ObtenerRolesPorPerfil(idPerfil);

                return new PerfilUsuario
                {
                    Legajo = legajo,
                    TienePerfil = true,
                    Perfil = perfil,
                    Roles = roles
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener perfil de usuario {legajo}: {ex.Message}");
                return new PerfilUsuario { Legajo = legajo, TienePerfil = false };
            }
        }

        public bool UsuarioTienePermiso(string legajo, string nombreRol)
        {
            PerfilUsuario perfilUsuario = ObtenerPerfilUsuario(legajo);

            if (!perfilUsuario.TienePerfil)
                return false;

            return perfilUsuario.Roles.Any(r => r.Descripcion.Equals(nombreRol, StringComparison.OrdinalIgnoreCase));
        }

        public TipoPantalla DeterminarPantallaPorPerfil(string legajo)
        {
            PerfilUsuario perfilUsuario = ObtenerPerfilUsuario(legajo);

            if (!perfilUsuario.TienePerfil)
                return TipoPantalla.SinPermiso;

            switch (perfilUsuario.Perfil.Descripcion.ToLower())
            {
                case "operador":
                    return TipoPantalla.Ventas;
                case "supervisor":
                    return TipoPantalla.Supervisor;
                case "administrador":
                    return TipoPantalla.Administrador;
                default:
                    return TipoPantalla.SinPermiso;
            }
        }

        #region Métodos privados

        private Perfil ObtenerPerfil(int idPerfil)
        {
            string registro = _dataBaseUtils.BuscarRegistroPorId("perfil.csv", idPerfil.ToString());

            if (registro != null)
            {
                string[] datos = registro.Split(';');
                return new Perfil
                {
                    Id = int.Parse(datos[0]),
                    Descripcion = datos[1]
                };
            }
            return null;
        }

        private List<Rol> ObtenerRolesPorPerfil(int idPerfil)
        {
            List<Rol> roles = new List<Rol>();

            // Buscar relaciones perfil-rol
            List<string> relacionesPerfilRol = _dataBaseUtils.BuscarRegistrosPorColumna("perfil_rol.csv", 0, idPerfil.ToString());

            foreach (string relacion in relacionesPerfilRol)
            {
                string[] datos = relacion.Split(';');
                int idRol = int.Parse(datos[1]);

                // Obtener información del rol
                string registroRol = _dataBaseUtils.BuscarRegistroPorId("rol.csv", idRol.ToString());
                if (registroRol != null)
                {
                    string[] datosRol = registroRol.Split(';');
                    roles.Add(new Rol
                    {
                        Id = int.Parse(datosRol[0]),
                        Descripcion = datosRol[1]
                    });
                }
            }

            return roles;
        }

        #endregion
    }

    #region Modelos para Perfiles y Roles

    public class PerfilUsuario
    {
        public string Legajo { get; set; }
        public bool TienePerfil { get; set; }
        public Perfil Perfil { get; set; }
        public List<Rol> Roles { get; set; } = new List<Rol>();

        public bool EsOperador => Perfil?.Descripcion?.ToLower() == "operador";
        public bool EsSupervisor => Perfil?.Descripcion?.ToLower() == "supervisor";
        public bool EsAdministrador => Perfil?.Descripcion?.ToLower() == "administrador";
    }

    public class Perfil
    {
        public int Id { get; set; }
        public string Descripcion { get; set; }
    }

    public class Rol
    {
        public int Id { get; set; }
        public string Descripcion { get; set; }
    }

    public enum TipoPantalla
    {
        SinPermiso,
        Ventas,
        Supervisor,
        Administrador
    }

    #endregion
}