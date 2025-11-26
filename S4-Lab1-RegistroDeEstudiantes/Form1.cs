using Microsoft.VisualBasic;
using System;
using System.Text.RegularExpressions;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Windows.Forms;

namespace S4_Lab1_RegistroDeEstudiantes
{
    // Clase de Conexión (Asegúrate de que esta exista en tu proyecto)
    /*
    public static class conexionDB
    {
        // 🚨 CAMBIA ESTA CADENA DE CONEXIÓN CON LOS DATOS DE TU SERVIDOR
        public static string connectionString = "Server=TU_SERVIDOR;Database=TU_BD;Trusted_Connection=True;Encrypt=False;";
    }
    */

    // Asumiendo que esta es la clase principal del formulario
    public partial class Form1 : Form
    {
        // Variables de clase
        string valor;
        string codigo = "admin123";
        bool esValidoCierre = false;
        private int alumnoSeleccionadoId = -1; // Usado para saber si estamos editando

        //################ CONSTRUCTOR ################
        public Form1()
        {
            InitializeComponent();
            cargarListas();

            // Activar salto con ENTER y atajos de teclado
            this.KeyPreview = true;
            this.KeyDown += Form1_KeyDown;

            // Conectar eventos para salto con ENTER
            tbxNombre.KeyDown += SaltarConEnter;
            tbxCedula.KeyDown += SaltarConEnter;
            tbxUsuario.KeyDown += SaltarConEnter;
            tbxPassword.KeyDown += SaltarConEnter;
            tbxConfirmacion.KeyDown += SaltarConEnter;
            cbxCarrera.KeyDown += SaltarConEnter;
            cbx_Semestre.KeyDown += SaltarConEnter;

            // Conectar eventos para DataGridView y validación en tiempo real
            dataGridView1.CellClick += dataGridView1_CellClick;
            tbxNombre.KeyPress += tbxNombre_KeyPress;
            tbxCedula.KeyPress += tbxCedula_KeyPress;

            // Conectar eventos para autogenerar Usuario
            tbxNombre.TextChanged += tbxNombre_TextChanged;
            tbxCedula.TextChanged += tbxCedula_TextChanged;
        }
        //################ FIN CONSTRUCTOR ################

        //---------------------------------
        // ⏩ Lógica de navegación y cierre
        //---------------------------------

        private void SaltarConEnter(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                this.SelectNextControl((Control)sender, true, true, true, true);
            }
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            while (true)
            {
                valor = Interaction.InputBox("Ingrese el codigo de administrador:", "Inicio de sesión");

                if (valor != codigo)
                {
                    if (valor == "")
                    {
                        MessageBox.Show("Sesión cancelada", "Cerrando programa",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Advertencia: La clave es incorrecta", "Acceso denegado",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                else
                {
                    Interaction.MsgBox("Acceso Permitido. Bienvenido al sistema de registro.",
                        MsgBoxStyle.OkOnly, "Registro correcto");

                    tbxNombre.Focus();
                    break;
                }
            }
        }

        private void SubMenu_salir_Click(object sender, EventArgs e)
        {
            var salida = Interaction.MsgBox("¿Esta segura que quiere salir del programa?",
                MsgBoxStyle.YesNo, "Cierre del programa");

            if (salida == MsgBoxResult.Yes)
            {
                esValidoCierre = true;
                this.Close();
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!esValidoCierre)
            {
                var salida = Interaction.MsgBox("¿Esta segura que quiere salir del programa?",
                MsgBoxStyle.YesNo, "Cierre del programa");

                if (salida == MsgBoxResult.No)
                {
                    e.Cancel = true;
                }
            }
        }

        //---------------------------------
        // 🔄 Métodos de carga y lectura (READ)
        //---------------------------------

        private void cargarListas()
        {
            // 1. Cargar Carreras desde la BD
            string queryCarreras = "SELECT DISTINCT Carrera FROM Alumnos ORDER BY Carrera";
            using (SqlConnection conn = new SqlConnection(conexionDB.connectionString))
            {
                try
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(queryCarreras, conn);
                    SqlDataReader reader = cmd.ExecuteReader();

                    cbxCarrera.Items.Clear();
                    while (reader.Read())
                    {
                        cbxCarrera.Items.Add(reader["Carrera"].ToString());
                    }
                    reader.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al cargar carreras: {ex.Message}", "Error de BD", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            // 2. Cargar Semestres (rango fijo)
            cbx_Semestre.Items.Clear();
            for (int i = 0; i < 11; i++)
            {
                cbx_Semestre.Items.Add((i + 1).ToString());
            }

            // 3. Carga los registros guardados en el DataGridView
            cargarRegistros();
        }

        private void cargarRegistros()
        {
            string query = "SELECT Id, Nombre, Cedula, Carrera, Semestre, Jornada FROM Alumnos ORDER BY Id DESC";

            using (SqlConnection conn = new SqlConnection(conexionDB.connectionString))
            {
                try
                {
                    conn.Open();
                    SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    dataGridView1.DataSource = dt;

                    // Ocultar columna 'Id'
                    if (dataGridView1.Columns["Id"] != null)
                    {
                        dataGridView1.Columns["Id"].Visible = false;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al cargar registros en DataGridView: {ex.Message}", "Error de BD", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    dataGridView1.DataSource = null;
                }
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                int id = Convert.ToInt32(row.Cells["Id"].Value);
                cargarAlumnoPorId(id);
            }
        }

        private void cargarAlumnoPorId(int id)
        {
            string query = "SELECT Nombre, Cedula, Carrera, Semestre, Jornada, Usuario, Contrasena, RecibirNotificaciones FROM Alumnos WHERE Id = @Id";

            using (SqlConnection conn = new SqlConnection(conexionDB.connectionString))
            {
                try
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@Id", id);
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        tbxNombre.Text = reader["Nombre"].ToString();
                        tbxCedula.Text = reader["Cedula"].ToString();
                        tbxUsuario.Text = reader["Usuario"].ToString();
                        tbxPassword.Text = reader["Contrasena"].ToString();
                        tbxConfirmacion.Text = reader["Contrasena"].ToString();
                        alumnoSeleccionadoId = id;

                        // ComboBox
                        string carrera = reader["Carrera"].ToString();
                        cbxCarrera.SelectedIndex = cbxCarrera.FindStringExact(carrera);

                        string semestre = reader["Semestre"].ToString();
                        cbx_Semestre.SelectedIndex = cbx_Semestre.FindStringExact(semestre);

                        // RadioButton
                        string jornada = reader["Jornada"].ToString();
                        rbMatutina.Checked = (jornada == "Matutina");
                        rbVespertina.Checked = (jornada == "Vespertina");

                        // Checkboxes
                        chbx_Notificaciones.Checked = (bool)reader["RecibirNotificaciones"];
                        chbx_Terminos.Checked = true;
                    }
                    else
                    {
                        MessageBox.Show("No se encontró el registro.", "Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                        alumnoSeleccionadoId = -1;
                    }
                    reader.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al cargar el registro por ID: {ex.Message}", "Error de BD", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void verRegistroToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string query = "SELECT TOP 1 Id FROM Alumnos ORDER BY Id DESC";

            using (SqlConnection conn = new SqlConnection(conexionDB.connectionString))
            {
                try
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(query, conn);
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        cargarAlumnoPorId((int)reader["Id"]);

                        MessageBox.Show("Último registro cargado correctamente.", "Información",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("No hay registros guardados en la base de datos.", "Sin datos",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    reader.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al cargar el último registro: {ex.Message}", "Error de BD", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void buscarAlumnoPorCedula()
        {
            string cedulaBuscar = Interaction.InputBox(
                "Ingrese la cédula del estudiante a buscar (Ej: 02-0755-000039):",
                "Buscar Estudiante por Cédula");

            if (cedulaBuscar == "")
            {
                MessageBox.Show("Búsqueda cancelada.", "Aviso",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (string.IsNullOrWhiteSpace(cedulaBuscar))
            {
                MessageBox.Show("La cédula no puede estar vacía.",
                    "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            const int MAX_LENGTH = 30;
            if (cedulaBuscar.Length > MAX_LENGTH)
            {
                MessageBox.Show($"La cédula no puede tener más de {MAX_LENGTH} caracteres.",
                    "Límite excedido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string patronContenido = @"^[0-9\-]+$";
            if (!Regex.IsMatch(cedulaBuscar, patronContenido))
            {
                MessageBox.Show(
                    "El campo de cédula solo debe contener números y guiones (0-9 y -).",
                    "Dato Inválido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string patronFormato = @"^\d{1,2}-\d{1,4}-\d{1,6}$";
            if (!Regex.IsMatch(cedulaBuscar, patronFormato))
            {
                MessageBox.Show(
                    "La cédula ingresada no cumple con el formato estándar (Ej: 02-0755-000039).",
                    "Formato Incorrecto", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string query = "SELECT Id FROM Alumnos WHERE Cedula = @Cedula";

            using (SqlConnection conn = new SqlConnection(conexionDB.connectionString))
            {
                try
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@Cedula", cedulaBuscar);

                    object result = cmd.ExecuteScalar();

                    if (result != null)
                    {
                        int id = Convert.ToInt32(result);
                        cargarAlumnoPorId(id);
                        MessageBox.Show(
                            $"Alumno con cédula {cedulaBuscar} encontrado.",
                            "Búsqueda Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show(
                            $"No se encontró ningún alumno con la cédula {cedulaBuscar}.",
                            "No Encontrado", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                        SubMenu_nuevo_Click(null, null); // Limpia formulario
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(
                        $"Error al buscar por cédula:\n{ex.Message}",
                        "Error de BD", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }


        //---------------------------------
        // ➕ Guardar, Editar y Limpiar (CREATE, UPDATE, CLEAR)
        //---------------------------------

        private void SubMenu_nuevo_Click(object sender, EventArgs e)
        {
            tbxCedula.Clear();
            tbxNombre.Clear();
            tbxUsuario.Clear();
            tbxPassword.Clear();
            tbxConfirmacion.Clear();

            cbxCarrera.SelectedIndex = -1;
            cbx_Semestre.SelectedIndex = -1;

            rbMatutina.Checked = false;
            rbVespertina.Checked = false;

            chbx_Terminos.Checked = false;
            chbx_Notificaciones.Checked = false;

            alumnoSeleccionadoId = -1; // Importante para indicar que es un nuevo registro

            tbxNombre.Focus();

            try
            {
                // Prueba de conexión a la BD
                using (SqlConnection conn = new SqlConnection(conexionDB.connectionString))
                {
                    conn.Open();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al conectar a la base de datos: " + ex.Message, "Error de Conexión", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SubMenu_guardar_Click(object sender, EventArgs e)
        {
            if (alumnoSeleccionadoId != -1)
            {
                MessageBox.Show("El registro actual está cargado. Limpie el formulario (CTRL+N) para crear un nuevo registro.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // Genera el usuario antes de las validaciones
            ActualizarNombreCompleto();

            // Validaciones comunes
            if (!ValidarCamposComunes())
            {
                return;
            }

            // --- LÓGICA DE BASE DE DATOS (CREATE) ---
            string nombre = tbxNombre.Text;
            string cedula = tbxCedula.Text;
            string usuario = tbxUsuario.Text;
            string password = tbxPassword.Text;
            string carrera = cbxCarrera.SelectedItem.ToString();
            string semestre = cbx_Semestre.SelectedItem.ToString();
            string jornada = rbMatutina.Checked ? "Matutina" : "Vespertina";
            bool recibirNotificaciones = chbx_Notificaciones.Checked;

            using (SqlConnection conn = new SqlConnection(conexionDB.connectionString))
            {
                try
                {
                    conn.Open();

                    // 1. VERIFICAR DUPLICADOS (Cédula o Usuario)
                    string checkQuery = "SELECT COUNT(*) FROM Alumnos WHERE Cedula = @Cedula OR Usuario = @Usuario";
                    SqlCommand checkCmd = new SqlCommand(checkQuery, conn);
                    checkCmd.Parameters.AddWithValue("@Cedula", cedula);
                    checkCmd.Parameters.AddWithValue("@Usuario", usuario);

                    int count = (int)checkCmd.ExecuteScalar();
                    if (count > 0)
                    {
                        string checkCedulaQuery = "SELECT COUNT(*) FROM Alumnos WHERE Cedula = @Cedula";
                        SqlCommand checkCedulaCmd = new SqlCommand(checkCedulaQuery, conn);
                        checkCedulaCmd.Parameters.AddWithValue("@Cedula", cedula);

                        if ((int)checkCedulaCmd.ExecuteScalar() > 0)
                        {
                            MessageBox.Show("La cédula ya está registrada.", "Dato repetido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                        else
                        {
                            MessageBox.Show("El nombre de usuario ya está registrado.", "Dato repetido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                        return;
                    }

                    // 2. INSERTAR NUEVO ALUMNO
                    string insertQuery = @"
                        INSERT INTO Alumnos (Nombre, Cedula, Carrera, Semestre, Jornada, Usuario, Contrasena, RecibirNotificaciones)
                        VALUES (@Nombre, @Cedula, @Carrera, @Semestre, @Jornada, @Usuario, @Contrasena, @Notificaciones)";

                    SqlCommand insertCmd = new SqlCommand(insertQuery, conn);
                    insertCmd.Parameters.AddWithValue("@Nombre", nombre);
                    insertCmd.Parameters.AddWithValue("@Cedula", cedula);
                    insertCmd.Parameters.AddWithValue("@Carrera", carrera);
                    insertCmd.Parameters.AddWithValue("@Semestre", semestre);
                    insertCmd.Parameters.AddWithValue("@Jornada", jornada);
                    insertCmd.Parameters.AddWithValue("@Usuario", usuario);
                    insertCmd.Parameters.AddWithValue("@Contrasena", password);
                    insertCmd.Parameters.AddWithValue("@Notificaciones", recibirNotificaciones);

                    int rowsAffected = insertCmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Registro guardado correctamente en la Base de Datos.", "Éxito",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);

                        SubMenu_nuevo_Click(sender, e);
                    }
                    else
                    {
                        MessageBox.Show("Fallo al insertar el registro. Ninguna fila fue afectada.", "Error de BD", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al guardar el registro en la BD: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    cargarRegistros();
                }
            }
        }

        private void SubMenu_editar_Click(object sender, EventArgs e)
        {
            if (alumnoSeleccionadoId == -1)
            {
                MessageBox.Show("Debe seleccionar un registro de la tabla o buscar uno (CTRL+B) para poder editarlo.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // Genera el usuario antes de las validaciones
            ActualizarNombreCompleto();

            // Validaciones comunes
            if (!ValidarCamposComunes())
            {
                return;
            }

            // --- LÓGICA DE BASE DE DATOS (UPDATE) ---
            string nombre = tbxNombre.Text;
            string cedula = tbxCedula.Text;
            string usuario = tbxUsuario.Text;
            string password = tbxPassword.Text;
            string carrera = cbxCarrera.SelectedItem.ToString();
            string semestre = cbx_Semestre.SelectedItem.ToString();
            string jornada = rbMatutina.Checked ? "Matutina" : "Vespertina";
            bool recibirNotificaciones = chbx_Notificaciones.Checked;

            using (SqlConnection conn = new SqlConnection(conexionDB.connectionString))
            {
                try
                {
                    conn.Open();

                    // 1. VERIFICAR DUPLICADOS (Cédula o Usuario) excluyendo el ID actual
                    string checkQuery = "SELECT COUNT(*) FROM Alumnos WHERE (Cedula = @Cedula OR Usuario = @Usuario) AND Id != @Id";
                    SqlCommand checkCmd = new SqlCommand(checkQuery, conn);
                    checkCmd.Parameters.AddWithValue("@Cedula", cedula);
                    checkCmd.Parameters.AddWithValue("@Usuario", usuario);
                    checkCmd.Parameters.AddWithValue("@Id", alumnoSeleccionadoId);

                    int count = (int)checkCmd.ExecuteScalar();
                    if (count > 0)
                    {
                        MessageBox.Show("La cédula o el nombre de usuario ya está registrado por otro alumno.", "Dato repetido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    // 2. ACTUALIZAR ALUMNO
                    string updateQuery = @"
                        UPDATE Alumnos SET 
                            Nombre = @Nombre, 
                            Cedula = @Cedula, 
                            Carrera = @Carrera, 
                            Semestre = @Semestre, 
                            Jornada = @Jornada, 
                            Usuario = @Usuario, 
                            Contrasena = @Contrasena, 
                            RecibirNotificaciones = @Notificaciones 
                        WHERE Id = @Id";

                    SqlCommand updateCmd = new SqlCommand(updateQuery, conn);
                    updateCmd.Parameters.AddWithValue("@Nombre", nombre);
                    updateCmd.Parameters.AddWithValue("@Cedula", cedula);
                    updateCmd.Parameters.AddWithValue("@Carrera", carrera);
                    updateCmd.Parameters.AddWithValue("@Semestre", semestre);
                    updateCmd.Parameters.AddWithValue("@Jornada", jornada);
                    updateCmd.Parameters.AddWithValue("@Usuario", usuario);
                    updateCmd.Parameters.AddWithValue("@Contrasena", password);
                    updateCmd.Parameters.AddWithValue("@Notificaciones", recibirNotificaciones);
                    updateCmd.Parameters.AddWithValue("@Id", alumnoSeleccionadoId);

                    int rowsAffected = updateCmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Registro editado y guardado correctamente en la Base de Datos.", "Éxito",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);

                        SubMenu_nuevo_Click(sender, e); // Limpia y resetea el ID
                    }
                    else
                    {
                        MessageBox.Show("Fallo al actualizar el registro. Ninguna fila fue afectada (Verifique que los datos hayan cambiado).", "Error de BD", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al editar el registro en la BD: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    cargarRegistros();
                }
            }
        }


        //---------------------------------
        // ✅ Métodos de Validación y Ayuda
        //---------------------------------

        private bool ValidarCamposComunes()
        {
            // ----- Validaciones de campos obligatorios -----------
            if (string.IsNullOrWhiteSpace(tbxNombre.Text) ||
                string.IsNullOrWhiteSpace(tbxCedula.Text) ||
                string.IsNullOrWhiteSpace(tbxUsuario.Text) ||
                string.IsNullOrWhiteSpace(tbxPassword.Text) ||
                string.IsNullOrWhiteSpace(tbxConfirmacion.Text) ||
                cbxCarrera.SelectedIndex == -1 ||
                cbx_Semestre.SelectedIndex == -1 ||
                (!rbMatutina.Checked && !rbVespertina.Checked))
            {
                MessageBox.Show("Uno o más campos obligatorios están vacíos.", "Error de Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            // ----- Validar que el nombre SOLO contenga letras y espacios.
            foreach (char c in tbxNombre.Text)
            {
                if (!char.IsLetter(c) && !char.IsWhiteSpace(c))
                {
                    MessageBox.Show("El nombre no puede contener números ni símbolos.", "Dato ilógico",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
            }

            // ----- Validar longitud máxima de 50 caracteres.
            if (tbxNombre.Text.Length > 50)
            {
                MessageBox.Show("El nombre excede el límite permitido (50 caracteres).",
                    "Dato ilógico", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            // ----- Validar formato de cédula ----------------------
            string patron = @"^\d{1,2}-\d{1,4}-\d{1,6}$";
            if (!Regex.IsMatch(tbxCedula.Text, patron))
            {
                MessageBox.Show("La cédula debe tener formato válido como 2-755-39 o 02-0755-000039",
                    "Dato ilógico", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            // ----- Validar coincidencia de contraseñas -----------
            if (tbxPassword.Text != tbxConfirmacion.Text)
            {
                MessageBox.Show("La contraseña y la confirmación no coinciden.",
                    "Error de Validación", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (!chbx_Terminos.Checked)
            {
                MessageBox.Show("Debes aceptar los términos.", "Aviso",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }

        private void tbxNombre_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetter(e.KeyChar) && !char.IsWhiteSpace(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void tbxCedula_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar) && e.KeyChar != '-')
            {
                e.Handled = true;
            }
        }

        private void ActualizarNombreCompleto()
        {
            if (!string.IsNullOrWhiteSpace(tbxNombre.Text) &&
                !string.IsNullOrWhiteSpace(tbxCedula.Text))
            {
                // Genera el usuario eliminando espacios y guiones
                tbxUsuario.Text = $"{tbxNombre.Text.Replace(" ", "_")}{tbxCedula.Text.Replace("-", "")}";
            }
            else
            {
                tbxUsuario.Text = "";
            }
        }

        // Eventos de TextChanged para la autogeneración de usuario
        private void tbxCedula_TextChanged(object sender, EventArgs e)
        {
            ActualizarNombreCompleto();
        }

        private void tbxNombre_TextChanged(object sender, EventArgs e)
        {
            ActualizarNombreCompleto();
        }

        // Eventos vacíos
        private void tbxConfirmacion_TextChanged(object sender, EventArgs e)
        {
        }

        private void cbxCarrera_SelectedIndexChanged(object sender, EventArgs e)
        {
        }
        private void SubMenu_eliminar_Click(object sender, EventArgs e)
        {
            if (alumnoSeleccionadoId == -1)
            {
                MessageBox.Show("Debe seleccionar un registro para eliminar.",
                                "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var confirmar = MessageBox.Show(
                "¿Seguro que quiere eliminar este registro?",
                "Confirmar eliminación",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (confirmar == DialogResult.No)
                return;

            string query = "DELETE FROM Alumnos WHERE Id = @Id";

            using (SqlConnection conn = new SqlConnection(conexionDB.connectionString))
            {
                try
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@Id", alumnoSeleccionadoId);

                    int rows = cmd.ExecuteNonQuery();

                    if (rows > 0)
                    {
                        MessageBox.Show("Registro eliminado correctamente.",
                                        "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        SubMenu_nuevo_Click(sender, e); // Limpia formulario
                    }
                    else
                    {
                        MessageBox.Show("El registro no pudo ser eliminado.",
                                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al eliminar:\n" + ex.Message,
                                    "Error de BD", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    cargarRegistros();
                }
            }
        }

        //---------------------------------
        // ⌨️ Atajos de Teclado
        //---------------------------------

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            // ===== CTRL + S → Guardar (Nuevo Registro) =====
            if (e.Control && e.KeyCode == Keys.S)
            {
                e.SuppressKeyPress = true;
                SubMenu_guardar_Click(sender, e);
            }
            // ===== CTRL + E → Eliminar Registro =====
            if (e.Control && e.KeyCode == Keys.E)
            {
                e.SuppressKeyPress = true;
                SubMenu_eliminar_Click(sender, e);
            }

            // ===== CTRL + SHIFT + E → Editar Registro =====
            if (e.Control && e.Shift && e.KeyCode == Keys.E)
            {
                e.SuppressKeyPress = true;
                SubMenu_editar_Click(sender, e);
            }

            // ===== CTRL + N → Nuevo Registro (Limpiar) =====
            if (e.Control && e.KeyCode == Keys.N)
            {
                e.SuppressKeyPress = true;
                SubMenu_nuevo_Click(sender, e);
            }

            // ===== CTRL + B → Buscar por Cédula (InputBox) =====
            if (e.Control && e.KeyCode == Keys.B)
            {
                e.SuppressKeyPress = true;
                buscarAlumnoPorCedula();
            }

            // ===== CTRL + L → Ver Último Registro =====
            if (e.Control && e.KeyCode == Keys.L)
            {
                e.SuppressKeyPress = true;
                verRegistroToolStripMenuItem_Click(sender, e);
            }

            // ===== CTRL + Q → Salir =====
            if (e.Control && e.KeyCode == Keys.Q)
            {
                var salida = Interaction.MsgBox("¿Esta segura que quiere salir del programa?",
                    MsgBoxStyle.YesNo, "Cierre del programa");

                if (salida == MsgBoxResult.Yes)
                {
                    e.SuppressKeyPress = true;
                    esValidoCierre = true;
                    this.Close();
                }
            }
        }
    }
}