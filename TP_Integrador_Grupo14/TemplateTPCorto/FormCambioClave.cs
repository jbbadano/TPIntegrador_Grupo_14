using System;
using System.Windows.Forms;
using Negocio;

namespace TemplateTPCorto
{
    public partial class FormCambioClave : Form
    {
        private string _legajo;
        private bool _esCambioObligatorio;
        private LoginService _loginService;

        public FormCambioClave(string legajo, bool esCambioObligatorio = false)
        {
            InitializeComponent();
            _legajo = legajo;
            _esCambioObligatorio = esCambioObligatorio;
            _loginService = new LoginService();
        }

        private void FormCambioContrasena_Load(object sender, EventArgs e)
        {
            if (_esCambioObligatorio)
            {
                lblTitulo.Text = "Cambio de Contraseña Obligatorio";
                btnCancelar.Visible = false; // No se puede cancelar si es obligatorio

                // Si es primer login, no pedimos contraseña actual
                var credencial = _loginService.ObtenerCredencial(_legajo);
                if (credencial != null && credencial.EsPrimerLogin())
                {
                    lblContrasenaActual.Visible = false;
                    txtContrasenaActual.Visible = false;
                }
            }
        }

        private void btnCambiar_Click(object sender, EventArgs e)
        {
            try
            {
                // Validaciones básicas
                if (string.IsNullOrWhiteSpace(txtNuevaContrasena.Text))
                {
                    MostrarMensaje("Debe ingresar la nueva contraseña");
                    return;
                }

                if (txtNuevaContrasena.Text != txtConfirmarContrasena.Text)
                {
                    MostrarMensaje("Las contraseñas no coinciden");
                    return;
                }

                if (txtNuevaContrasena.Text.Length < 8)
                {
                    MostrarMensaje("La contraseña debe tener al menos 8 caracteres");
                    return;
                }

                // Si no es primer login, validar contraseña actual
                var credencial = _loginService.ObtenerCredencial(_legajo);
                if (credencial != null && !credencial.EsPrimerLogin())
                {
                    if (string.IsNullOrWhiteSpace(txtContrasenaActual.Text))
                    {
                        MostrarMensaje("Debe ingresar la contraseña actual");
                        return;
                    }

                    if (credencial.Contrasena != txtContrasenaActual.Text)
                    {
                        MostrarMensaje("La contraseña actual es incorrecta");
                        return;
                    }
                }

                // Cambiar contraseña
                var resultado = _loginService.CambiarContrasena(_legajo, txtContrasenaActual.Text, txtNuevaContrasena.Text);

                if (resultado.Exitoso)
                {
                    MessageBox.Show("Contraseña cambiada exitosamente", "Éxito",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    MostrarMensaje(resultado.Mensaje);
                }
            }
            catch (Exception ex)
            {
                MostrarMensaje("Error al cambiar contraseña: " + ex.Message);
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            if (_esCambioObligatorio)
            {
                MostrarMensaje("El cambio de contraseña es obligatorio");
                return;
            }

            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void MostrarMensaje(string mensaje)
        {
            lblMensaje.Text = mensaje;
        }
    }
}