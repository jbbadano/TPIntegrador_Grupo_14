using System;
using System.Windows.Forms;
using Negocio;
using System.Data;
using System.Drawing;

namespace TemplateTPCorto
{
    public partial class FormAdministrador : Form
    {
        private string _legajoUsuario;
        private AdminNegocio _adminNegocio;
        private DataGridView dgvAutorizaciones;

        public FormAdministrador(string legajoUsuario = "")
        {
            InitializeComponent();
            _legajoUsuario = legajoUsuario;
            _adminNegocio = new AdminNegocio();
            ConfigurarFormulario();
            CargarAutorizaciones();
        }

        private void ConfigurarFormulario()
        {
            this.Text = "Panel de Administrador";
            this.Size = new Size(800, 600);
            this.StartPosition = FormStartPosition.CenterScreen;

            // Título
            Label lblTitulo = new Label { Text = "Autorizaciones Pendientes", Font = new Font("Arial", 16, FontStyle.Bold), Location = new Point(20, 20), Size = new Size(400, 30) };
            this.Controls.Add(lblTitulo);

            // Botón Aprobar
            Button btnAprobar = new Button { Text = "Aprobar", Location = new Point(20, 70), Size = new Size(120, 30) };
            btnAprobar.Click += BtnAprobar_Click;
            this.Controls.Add(btnAprobar);

            // Botón Rechazar
            Button btnRechazar = new Button { Text = "Rechazar", Location = new Point(150, 70), Size = new Size(120, 30) };
            btnRechazar.Click += BtnRechazar_Click;
            this.Controls.Add(btnRechazar);

            // Grilla
            dgvAutorizaciones = new DataGridView
            {
                Name = "dgvAutorizaciones",
                Location = new Point(20, 120),
                Size = new Size(750, 420),
                ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                MultiSelect = false,
                ReadOnly = true,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
            };
            this.Controls.Add(dgvAutorizaciones);
            
            // Botón Cerrar Sesión
            Button btnCerrarSesion = new Button { Text = "Cerrar Sesión", Location = new Point(650, 70), Size = new Size(120, 30) };
            btnCerrarSesion.Click += BtnCerrarSesion_Click;
            this.Controls.Add(btnCerrarSesion);
        }

        private void CargarAutorizaciones()
        {
            try
            {
                dgvAutorizaciones.DataSource = null;
                dgvAutorizaciones.DataSource = _adminNegocio.ObtenerSolicitudesPendientes();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar autorizaciones: {ex.Message}");
            }
        }

        private void BtnAprobar_Click(object sender, EventArgs e)
        {
            if (dgvAutorizaciones.SelectedRows.Count > 0)
            {
                var solicitud = dgvAutorizaciones.SelectedRows[0].DataBoundItem;
                string idOperacion = (string)solicitud.GetType().GetProperty("IdOperacion").GetValue(solicitud, null);

                if (_adminNegocio.AprobarSolicitud(idOperacion))
                {
                    MessageBox.Show("Operación aprobada.", "Éxito");
                    CargarAutorizaciones();
                }
                else
                {
                    MessageBox.Show("Error al aprobar la operación.", "Error");
                }
            }
        }

        private void BtnRechazar_Click(object sender, EventArgs e)
        {
            if (dgvAutorizaciones.SelectedRows.Count > 0)
            {
                var solicitud = dgvAutorizaciones.SelectedRows[0].DataBoundItem;
                string idOperacion = (string)solicitud.GetType().GetProperty("IdOperacion").GetValue(solicitud, null);

                if (_adminNegocio.RechazarSolicitud(idOperacion))
                {
                    MessageBox.Show("Operación rechazada.", "Éxito");
                    CargarAutorizaciones();
                }
                else
                {
                    MessageBox.Show("Error al rechazar la operación.", "Error");
                }
            }
        }
        
        private void BtnCerrarSesion_Click(object sender, EventArgs e)
        {
            this.Close();
            new FormLogin().Show();
        }
    }
} 