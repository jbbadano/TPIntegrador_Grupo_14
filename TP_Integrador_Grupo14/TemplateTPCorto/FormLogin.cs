using Datos;
using Negocio;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TemplateTPCorto
{
    public partial class FormLogin : Form
    {
        public FormLogin()
        {
            InitializeComponent();
        }

        private void btnIngresar_Click(object sender, EventArgs e)
        {
            LoginService loginService = new LoginService();
            ResultadoLogin resultado = loginService.Autenticar(txtUsuario.Text, txtPassword.Text);

            if (!resultado.Exitoso)
            {
                MessageBox.Show(resultado.Mensaje);
                return;
            }

            if (resultado.RequiereCambioContrasena)
            {
                // Abrir form de cambio de contraseña
                FormCambioClave formCambio = new FormCambioClave(resultado.Credencial.Legajo);
                formCambio.ShowDialog();
            }
            else
            {
                // Determinar qué pantalla mostrar
                PerfilService perfilService = new PerfilService();
                TipoPantalla pantalla = perfilService.DeterminarPantallaPorPerfil(resultado.Credencial.Legajo);

                switch (pantalla)
                {
                    case TipoPantalla.Ventas:
                        new FormVentas(resultado.Credencial.Legajo).Show();
                        break;
                    case TipoPantalla.Supervisor:
                        new FormSupervisor().Show();
                        break;
                    case TipoPantalla.Administrador:
                        new FormAdministrador().Show();
                        break;
                }
                this.Hide();
            }
        }

        private void chkMostrarContrasena_CheckedChanged(object sender, EventArgs e)
        {
            // Si el checkbox está marcado, no usar caracter de contraseña.
            // Si no, usar el asterisco.
            txtPassword.PasswordChar = chkMostrarContrasena.Checked ? '\0' : '*';
        }
    }
}
