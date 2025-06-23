using System;
using System.Windows.Forms;
using Negocio;

namespace TemplateTPCorto
{
    public partial class FormSupervisor : Form
    {
        private string _legajoUsuario;
        private TextBox txtLegajoDesbloquear;
        // private Button btnBuscarBloqueado; // Eliminado porque nunca se usa

        public FormSupervisor(string legajoUsuario = "")
        {
            InitializeComponent();
            _legajoUsuario = legajoUsuario;
            ConfigurarFormulario();
        }

        private void ConfigurarFormulario()
        {
            this.Text = "Panel de Supervisor";
            this.Size = new System.Drawing.Size(800, 600);
            this.StartPosition = FormStartPosition.CenterScreen;

            // Crear controles
            Label lblTitulo = new Label
            {
                Text = "Panel de Supervisor",
                Font = new System.Drawing.Font("Arial", 16, System.Drawing.FontStyle.Bold),
                Location = new System.Drawing.Point(20, 20),
                Size = new System.Drawing.Size(300, 30)
            };

            Button btnModificarPersona = new Button
            {
                Text = "Modificar Persona",
                Location = new System.Drawing.Point(20, 80),
                Size = new System.Drawing.Size(200, 40),
                Font = new System.Drawing.Font("Arial", 10)
            };
            btnModificarPersona.Click += BtnModificarPersona_Click;

            Button btnDesbloquearCredencial = new Button
            {
                Text = "Desbloquear Credencial",
                Location = new System.Drawing.Point(20, 140),
                Size = new System.Drawing.Size(200, 40),
                Font = new System.Drawing.Font("Arial", 10)
            };
            btnDesbloquearCredencial.Click += BtnDesbloquearCredencial_Click;

            // Controles para desbloquear usuario
            Label lblLegajo = new Label 
            {
                Text = "Legajo a Desbloquear:",
                Location = new System.Drawing.Point(230, 145),
                Size = new System.Drawing.Size(120, 20)
            };

            txtLegajoDesbloquear = new TextBox 
            {
                Location = new System.Drawing.Point(360, 145),
                Size = new System.Drawing.Size(100, 20)
            };

            Button btnCerrarSesion = new Button
            {
                Text = "Cerrar Sesión",
                Location = new System.Drawing.Point(20, 200),
                Size = new System.Drawing.Size(200, 40),
                Font = new System.Drawing.Font("Arial", 10)
            };
            btnCerrarSesion.Click += BtnCerrarSesion_Click;

            // Agregar controles al formulario
            this.Controls.Add(lblTitulo);
            this.Controls.Add(btnModificarPersona);
            this.Controls.Add(btnDesbloquearCredencial);
            this.Controls.Add(lblLegajo);
            this.Controls.Add(txtLegajoDesbloquear);
            this.Controls.Add(btnCerrarSesion);
        }

        private void BtnModificarPersona_Click(object sender, EventArgs e)
        {
            FormModificarPersona formModificar = new FormModificarPersona(_legajoUsuario);
            formModificar.ShowDialog();
        }

        private void BtnDesbloquearCredencial_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtLegajoDesbloquear.Text))
            {
                MessageBox.Show("Por favor, ingrese un legajo.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            LoginService loginService = new LoginService();
            bool exito = loginService.DesbloquearUsuario(txtLegajoDesbloquear.Text);

            if (exito)
            {
                MessageBox.Show("Usuario desbloqueado exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("No se pudo desbloquear el usuario. Verifique el legajo o puede que no esté bloqueado.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnCerrarSesion_Click(object sender, EventArgs e)
        {
            this.Close();
            new FormLogin().Show();
        }
    }
} 