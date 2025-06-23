using Datos.Ventas;
using Negocio;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace TemplateTPCorto
{
    public partial class FormVentas : Form
    {
        private VentasNegocio _ventasNegocio;
        private List<ProductoCarrito> _productosEnCarrito;
        private DataGridView dgvProductos;
        private DataGridView dgvCarrito;
        private ComboBox cmbClientes;
        private ComboBox cboCategoriaProductos;
        private Button btnListarProductos;
        private Button btnAgregarAlCarrito;
        private Button btnQuitarDelCarrito;
        private Button btnProcesarVenta;
        private Label lblSubTotal;
        private Label lblTotal;
        private NumericUpDown nudCantidad;
        private string _idUsuario;

        public FormVentas(string idUsuario)
        {
            InitializeComponent();
            _idUsuario = "784c07f2-2b26-4973-9235-4064e94832b5"; // Hardcodeado como GUID requerido por la API
            _ventasNegocio = new VentasNegocio();
            _productosEnCarrito = new List<ProductoCarrito>();
            ConfigurarLayout();
            CargarClientes();
            CargarCategoriasProductos();
            IniciarTotales();
        }

        private void ConfigurarLayout()
        {
            this.Text = "FormVentas";
            this.Size = new Size(900, 600);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Controls.Clear();

            // Combo Clientes
            Label lblClientes = new Label { Text = "Clientes", Location = new Point(20, 20), Size = new Size(60, 20) };
            cmbClientes = new ComboBox { Location = new Point(90, 18), Size = new Size(200, 24), DropDownStyle = ComboBoxStyle.DropDownList };
            this.Controls.Add(lblClientes);
            this.Controls.Add(cmbClientes);

            // Combo Categoría
            Label lblCategoria = new Label { Text = "Categoría Productos", Location = new Point(320, 20), Size = new Size(120, 20) };
            cboCategoriaProductos = new ComboBox { Location = new Point(450, 18), Size = new Size(150, 24), DropDownStyle = ComboBoxStyle.DropDownList };
            this.Controls.Add(lblCategoria);
            this.Controls.Add(cboCategoriaProductos);

            // Botón Listar
            btnListarProductos = new Button { Text = "Listar", Location = new Point(620, 16), Size = new Size(80, 28) };
            btnListarProductos.Click += btnListarProductos_Click;
            this.Controls.Add(btnListarProductos);

            // Grilla de productos
            dgvProductos = new DataGridView
            {
                Location = new Point(20, 60),
                Size = new Size(400, 250),
                ReadOnly = true,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                MultiSelect = false,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
            };
            this.Controls.Add(dgvProductos);

            // Cantidad
            Label lblCantidad = new Label { Text = "Cantidad", Location = new Point(20, 320), Size = new Size(60, 20) };
            nudCantidad = new NumericUpDown { Location = new Point(90, 318), Size = new Size(60, 24), Minimum = 1, Maximum = 100, Value = 1 };
            this.Controls.Add(lblCantidad);
            this.Controls.Add(nudCantidad);

            // Botón agregar al carrito
            btnAgregarAlCarrito = new Button { Text = "Agregar al carrito", Location = new Point(170, 316), Size = new Size(150, 28) };
            btnAgregarAlCarrito.Click += BtnAgregarAlCarrito_Click;
            this.Controls.Add(btnAgregarAlCarrito);

            // Grilla del carrito
            dgvCarrito = new DataGridView
            {
                Location = new Point(450, 60),
                Size = new Size(400, 250),
                ReadOnly = true,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                MultiSelect = false,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
            };
            this.Controls.Add(dgvCarrito);

            // Botón quitar del carrito
            btnQuitarDelCarrito = new Button { Text = "Quitar", Location = new Point(760, 320), Size = new Size(90, 28) };
            btnQuitarDelCarrito.Click += BtnQuitarDelCarrito_Click;
            this.Controls.Add(btnQuitarDelCarrito);

            // Subtotal y total
            Label lblSub = new Label { Text = "Sub Total", Location = new Point(450, 340), Size = new Size(80, 20) };
            lblSubTotal = new Label { Text = "0.00", Location = new Point(530, 340), Size = new Size(80, 20) };
            Label lblTot = new Label { Text = "Total", Location = new Point(450, 370), Size = new Size(80, 20) };
            lblTotal = new Label { Text = "0.00", Location = new Point(530, 370), Size = new Size(80, 20) };
            this.Controls.Add(lblSub);
            this.Controls.Add(lblSubTotal);
            this.Controls.Add(lblTot);
            this.Controls.Add(lblTotal);

            // Botón procesar venta
            btnProcesarVenta = new Button { Text = "Procesar Venta", Location = new Point(450, 410), Size = new Size(150, 32) };
            btnProcesarVenta.Click += BtnProcesarVenta_Click;
            this.Controls.Add(btnProcesarVenta);
        }

        private void IniciarTotales()
        {
            lblSubTotal.Text = "0.00";
            lblTotal.Text = "0.00";
        }

        private void CargarCategoriasProductos()
        {
            var categoriaProductos = _ventasNegocio.obtenerCategoriaProductos();
            cboCategoriaProductos.DataSource = categoriaProductos;
            cboCategoriaProductos.DisplayMember = "Descripcion";
            cboCategoriaProductos.ValueMember = "Id";
        }

        private void CargarClientes()
        {
            var listadoClientes = _ventasNegocio.obtenerClientes();
            cmbClientes.DataSource = listadoClientes;
            cmbClientes.DisplayMember = "NombreCompleto";
            cmbClientes.ValueMember = "Id";
        }

        private void btnListarProductos_Click(object sender, EventArgs e)
        {
            if (cboCategoriaProductos.SelectedItem is CategoriaProductos categoriaSeleccionada)
            {
                var productos = _ventasNegocio.obtenerProductosPorCategoria(categoriaSeleccionada.Id.ToString());
                dgvProductos.DataSource = productos;

                // Ocultar la columna 'Cantidad' que no es relevante aquí
                if (dgvProductos.Columns["Cantidad"] != null)
                {
                    dgvProductos.Columns["Cantidad"].Visible = false;
                }
            }
        }

        private void BtnAgregarAlCarrito_Click(object sender, EventArgs e)
        {
            if (dgvProductos.SelectedRows.Count > 0)
            {
                Producto productoSeleccionado = (Producto)dgvProductos.SelectedRows[0].DataBoundItem;
                int cantidad = (int)nudCantidad.Value;

                // Validar stock antes de agregar al carrito
                if (cantidad > productoSeleccionado.Stock)
                {
                    MessageBox.Show($"No hay stock suficiente para el producto '{productoSeleccionado.Nombre}'. Stock disponible: {productoSeleccionado.Stock}", "Stock Insuficiente", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var existente = _productosEnCarrito.FirstOrDefault(p => p.IdProducto == productoSeleccionado.Id);
                if (existente != null)
                {
                    // Validar stock al aumentar cantidad en el carrito
                    if ((existente.Cantidad + cantidad) > productoSeleccionado.Stock)
                    {
                        MessageBox.Show($"No se puede agregar más cantidad del producto '{productoSeleccionado.Nombre}'. Stock disponible: {productoSeleccionado.Stock}, en carrito: {existente.Cantidad}", "Stock Insuficiente", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    existente.Cantidad += cantidad;
                }
                else
                {
                    _productosEnCarrito.Add(new ProductoCarrito(
                        productoSeleccionado.Id,
                        productoSeleccionado.Nombre,
                        productoSeleccionado.Precio,
                        cantidad));
                }
                ActualizarCarrito();
            }
        }

        private void BtnQuitarDelCarrito_Click(object sender, EventArgs e)
        {
            if (dgvCarrito.SelectedRows.Count > 0)
            {
                ProductoCarrito productoSeleccionado = (ProductoCarrito)dgvCarrito.SelectedRows[0].DataBoundItem;
                _productosEnCarrito.Remove(productoSeleccionado);
                ActualizarCarrito();
            }
        }

        private void ActualizarCarrito()
        {
            dgvCarrito.DataSource = null;
            dgvCarrito.DataSource = _productosEnCarrito.ToList();
            CalcularTotales();
        }

        private void CalcularTotales()
        {
            decimal subtotal = _productosEnCarrito.Sum(p => p.Precio * p.Cantidad);
            decimal total = subtotal;
            if (subtotal > 1000000)
            {
                total *= 0.85m; // 15% de descuento
            }
            lblSubTotal.Text = subtotal.ToString("C");
            lblTotal.Text = total.ToString("C");
        }

        private void BtnProcesarVenta_Click(object sender, EventArgs e)
        {
            if (cmbClientes.SelectedItem == null)
            {
                MessageBox.Show("Por favor, seleccione un cliente.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (_productosEnCarrito.Count == 0)
            {
                MessageBox.Show("El carrito está vacío.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            Guid idClienteSeleccionado = (Guid)cmbClientes.SelectedValue;

            Venta nuevaVenta = new Venta
            {
                IdCliente = idClienteSeleccionado,
                IdUsuario = _idUsuario,
                Productos = _productosEnCarrito.Select(p => new Producto
                {
                    Id = p.IdProducto,
                    Nombre = p.Nombre,
                    Precio = p.Precio,
                    Cantidad = p.Cantidad
                }).ToList(),
                Total = decimal.Parse(lblTotal.Text, System.Globalization.NumberStyles.Currency)
            };
            var errores = _ventasNegocio.procesarVenta(nuevaVenta);
            if (errores == null || errores.Count == 0)
            {
                MessageBox.Show("Venta procesada exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                _productosEnCarrito.Clear();
                ActualizarCarrito();
            }
            else
            {
                MessageBox.Show(string.Join("\n\n", errores), "Error al procesar la venta", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}

