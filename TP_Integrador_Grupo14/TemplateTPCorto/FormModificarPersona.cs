using Datos.Ventas;
using Negocio;
using System;
using System.Windows.Forms;

namespace TemplateTPCorto
{
    public partial class FormModificarPersona : Form
    {
        private string _legajoSupervisor;
        private PersonaNegocio _personaNegocio;
        private Persona _personaActual;

        public FormModificarPersona(string legajoSupervisor)
        {
            InitializeComponent();
            _legajoSupervisor = legajoSupervisor;
            _personaNegocio = new PersonaNegocio();
            this.Text = "Modificar Datos de Persona";
        }

        private void FormModificarPersona_Load(object sender, EventArgs e)
        {
            // Opcional: Cargar algo al inicio si fuera necesario.
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            _personaActual = _personaNegocio.BuscarPorLegajo(txtLegajoBuscar.Text);

            if (_personaActual != null)
            {
                MostrarDatosPersona();
            }
            else
            {
                MessageBox.Show("Persona no encontrada.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                grpDatosPersona.Visible = false;
            }
        }

        private void MostrarDatosPersona()
        {
            txtLegajo.Text = _personaActual.Legajo;
            txtNombre.Text = _personaActual.Nombre;
            txtApellido.Text = _personaActual.Apellido;
            txtDNI.Text = _personaActual.DNI;
            dtpFechaIngreso.Value = _personaActual.FechaIngreso;
            grpDatosPersona.Visible = true;
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            // Creamos un nuevo objeto Persona con los datos del formulario
            // para no modificar el original hasta que se apruebe.
            Persona personaModificada = new Persona(_personaActual.ToCsv()) // Clonamos
            {
                Nombre = txtNombre.Text,
                Apellido = txtApellido.Text,
                DNI = txtDNI.Text,
                FechaIngreso = dtpFechaIngreso.Value
            };

            bool exito = _personaNegocio.SolicitarCambio(personaModificada, _legajoSupervisor);

            if (exito)
            {
                MessageBox.Show("Solicitud de cambio enviada para aprobación.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            else
            {
                MessageBox.Show("Error al enviar la solicitud de cambio.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
} 