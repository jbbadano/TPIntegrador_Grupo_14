using Datos.Ventas;
using System;

namespace Negocio
{
    public class PersonaNegocio
    {
        public PersonaNegocio()
        {
        }

        public Persona BuscarPorLegajo(string legajo)
        {
            return null;
        }

        public bool SolicitarCambio(Persona personaModificada, string legajoSupervisor)
        {
            try
            {
                if (personaModificada == null) return false;

                string idOperacion = Guid.NewGuid().ToString();
                string registroOperacion = $"{idOperacion};{legajoSupervisor};{DateTime.Now:d/M/yyyy};MOD_PERSONA";
                
                new Persistencia.DataBase.DataBaseUtils().AgregarRegistro("operaciones.csv", registroOperacion);

                string registroCambio = $"{idOperacion};{personaModificada.Legajo};{personaModificada.Nombre};{personaModificada.Apellido};{personaModificada.DNI};{personaModificada.FechaIngreso:d/M/yyyy}";

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
} 