namespace TemplateTPCorto
{
    partial class FormModificarPersona
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.lblLegajoBuscar = new System.Windows.Forms.Label();
            this.txtLegajoBuscar = new System.Windows.Forms.TextBox();
            this.btnBuscar = new System.Windows.Forms.Button();
            this.grpDatosPersona = new System.Windows.Forms.GroupBox();
            this.txtLegajo = new System.Windows.Forms.TextBox();
            this.lblLegajo = new System.Windows.Forms.Label();
            this.txtNombre = new System.Windows.Forms.TextBox();
            this.lblNombre = new System.Windows.Forms.Label();
            this.txtApellido = new System.Windows.Forms.TextBox();
            this.lblApellido = new System.Windows.Forms.Label();
            this.txtDNI = new System.Windows.Forms.TextBox();
            this.lblDNI = new System.Windows.Forms.Label();
            this.dtpFechaIngreso = new System.Windows.Forms.DateTimePicker();
            this.lblFechaIngreso = new System.Windows.Forms.Label();
            this.btnGuardar = new System.Windows.Forms.Button();
            this.grpDatosPersona.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblLegajoBuscar
            // 
            this.lblLegajoBuscar.AutoSize = true;
            this.lblLegajoBuscar.Location = new System.Drawing.Point(12, 15);
            this.lblLegajoBuscar.Name = "lblLegajoBuscar";
            this.lblLegajoBuscar.Size = new System.Drawing.Size(42, 13);
            this.lblLegajoBuscar.TabIndex = 0;
            this.lblLegajoBuscar.Text = "Legajo:";
            // 
            // txtLegajoBuscar
            // 
            this.txtLegajoBuscar.Location = new System.Drawing.Point(60, 12);
            this.txtLegajoBuscar.Name = "txtLegajoBuscar";
            this.txtLegajoBuscar.Size = new System.Drawing.Size(100, 20);
            this.txtLegajoBuscar.TabIndex = 1;
            // 
            // btnBuscar
            // 
            this.btnBuscar.Location = new System.Drawing.Point(166, 10);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(75, 23);
            this.btnBuscar.TabIndex = 2;
            this.btnBuscar.Text = "Buscar";
            this.btnBuscar.UseVisualStyleBackColor = true;
            this.btnBuscar.Click += new System.EventHandler(this.btnBuscar_Click);
            // 
            // grpDatosPersona
            // 
            this.grpDatosPersona.Controls.Add(this.btnGuardar);
            this.grpDatosPersona.Controls.Add(this.lblFechaIngreso);
            this.grpDatosPersona.Controls.Add(this.dtpFechaIngreso);
            this.grpDatosPersona.Controls.Add(this.lblDNI);
            this.grpDatosPersona.Controls.Add(this.txtDNI);
            this.grpDatosPersona.Controls.Add(this.lblApellido);
            this.grpDatosPersona.Controls.Add(this.txtApellido);
            this.grpDatosPersona.Controls.Add(this.lblNombre);
            this.grpDatosPersona.Controls.Add(this.txtNombre);
            this.grpDatosPersona.Controls.Add(this.lblLegajo);
            this.grpDatosPersona.Controls.Add(this.txtLegajo);
            this.grpDatosPersona.Location = new System.Drawing.Point(15, 50);
            this.grpDatosPersona.Name = "grpDatosPersona";
            this.grpDatosPersona.Size = new System.Drawing.Size(257, 230);
            this.grpDatosPersona.TabIndex = 3;
            this.grpDatosPersona.TabStop = false;
            this.grpDatosPersona.Text = "Datos de la Persona";
            this.grpDatosPersona.Visible = false;
            // 
            // txtLegajo
            // 
            this.txtLegajo.Location = new System.Drawing.Point(90, 30);
            this.txtLegajo.Name = "txtLegajo";
            this.txtLegajo.ReadOnly = true;
            this.txtLegajo.Size = new System.Drawing.Size(150, 20);
            this.txtLegajo.TabIndex = 1;
            // 
            // lblLegajo
            // 
            this.lblLegajo.AutoSize = true;
            this.lblLegajo.Location = new System.Drawing.Point(20, 33);
            this.lblLegajo.Name = "lblLegajo";
            this.lblLegajo.Size = new System.Drawing.Size(42, 13);
            this.lblLegajo.TabIndex = 0;
            this.lblLegajo.Text = "Legajo:";
            // 
            // txtNombre
            // 
            this.txtNombre.Location = new System.Drawing.Point(90, 60);
            this.txtNombre.Name = "txtNombre";
            this.txtNombre.Size = new System.Drawing.Size(150, 20);
            this.txtNombre.TabIndex = 3;
            // 
            // lblNombre
            // 
            this.lblNombre.AutoSize = true;
            this.lblNombre.Location = new System.Drawing.Point(20, 63);
            this.lblNombre.Name = "lblNombre";
            this.lblNombre.Size = new System.Drawing.Size(47, 13);
            this.lblNombre.TabIndex = 2;
            this.lblNombre.Text = "Nombre:";
            // 
            // txtApellido
            // 
            this.txtApellido.Location = new System.Drawing.Point(90, 90);
            this.txtApellido.Name = "txtApellido";
            this.txtApellido.Size = new System.Drawing.Size(150, 20);
            this.txtApellido.TabIndex = 5;
            // 
            // lblApellido
            // 
            this.lblApellido.AutoSize = true;
            this.lblApellido.Location = new System.Drawing.Point(20, 93);
            this.lblApellido.Name = "lblApellido";
            this.lblApellido.Size = new System.Drawing.Size(47, 13);
            this.lblApellido.TabIndex = 4;
            this.lblApellido.Text = "Apellido:";
            // 
            // txtDNI
            // 
            this.txtDNI.Location = new System.Drawing.Point(90, 120);
            this.txtDNI.Name = "txtDNI";
            this.txtDNI.Size = new System.Drawing.Size(150, 20);
            this.txtDNI.TabIndex = 7;
            // 
            // lblDNI
            // 
            this.lblDNI.AutoSize = true;
            this.lblDNI.Location = new System.Drawing.Point(20, 123);
            this.lblDNI.Name = "lblDNI";
            this.lblDNI.Size = new System.Drawing.Size(29, 13);
            this.lblDNI.TabIndex = 6;
            this.lblDNI.Text = "DNI:";
            // 
            // dtpFechaIngreso
            // 
            this.dtpFechaIngreso.Location = new System.Drawing.Point(90, 150);
            this.dtpFechaIngreso.Name = "dtpFechaIngreso";
            this.dtpFechaIngreso.Size = new System.Drawing.Size(150, 20);
            this.dtpFechaIngreso.TabIndex = 8;
            // 
            // lblFechaIngreso
            // 
            this.lblFechaIngreso.AutoSize = true;
            this.lblFechaIngreso.Location = new System.Drawing.Point(20, 153);
            this.lblFechaIngreso.Name = "lblFechaIngreso";
            this.lblFechaIngreso.Size = new System.Drawing.Size(64, 13);
            this.lblFechaIngreso.TabIndex = 9;
            this.lblFechaIngreso.Text = "Fecha Ing.:";
            // 
            // btnGuardar
            // 
            this.btnGuardar.Location = new System.Drawing.Point(90, 190);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(75, 23);
            this.btnGuardar.TabIndex = 10;
            this.btnGuardar.Text = "Guardar";
            this.btnGuardar.UseVisualStyleBackColor = true;
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            // 
            // FormModificarPersona
            // 
            this.ClientSize = new System.Drawing.Size(284, 291);
            this.Controls.Add(this.grpDatosPersona);
            this.Controls.Add(this.btnBuscar);
            this.Controls.Add(this.txtLegajoBuscar);
            this.Controls.Add(this.lblLegajoBuscar);
            this.Name = "FormModificarPersona";
            this.Load += new System.EventHandler(this.FormModificarPersona_Load);
            this.grpDatosPersona.ResumeLayout(false);
            this.grpDatosPersona.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label lblLegajoBuscar;
        private System.Windows.Forms.TextBox txtLegajoBuscar;
        private System.Windows.Forms.Button btnBuscar;
        private System.Windows.Forms.GroupBox grpDatosPersona;
        private System.Windows.Forms.Button btnGuardar;
        private System.Windows.Forms.Label lblFechaIngreso;
        private System.Windows.Forms.DateTimePicker dtpFechaIngreso;
        private System.Windows.Forms.Label lblDNI;
        private System.Windows.Forms.TextBox txtDNI;
        private System.Windows.Forms.Label lblApellido;
        private System.Windows.Forms.TextBox txtApellido;
        private System.Windows.Forms.Label lblNombre;
        private System.Windows.Forms.TextBox txtNombre;
        private System.Windows.Forms.Label lblLegajo;
        private System.Windows.Forms.TextBox txtLegajo;
    }
} 