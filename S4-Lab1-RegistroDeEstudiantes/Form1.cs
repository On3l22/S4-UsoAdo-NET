using Microsoft.VisualBasic;
using System.Text.Json;
using System.Text.RegularExpressions;


namespace S4_Lab1_RegistroDeEstudiantes
{
    public partial class Form1 : Form
    {
        //password
        string valor;
        string codigo = "admin123";

        //Inicialización del formulario
        public Form1()
        {
            InitializeComponent();
            cargarListas();
        }

        //Evento que se ejecuta al mostrar el formulario
        private void Form1_Shown(object sender, EventArgs e)
        {
            valor = Interaction.InputBox("Ingrese el codigo de administrador:", "Inicio de sesión");
            if (valor != codigo)
            {
                if (valor == "")
                {
                    MessageBox.Show("Sesión cancelada", "Cerrando programa",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    //Interaction.MsgBox("Acceso Denegado. Código incorrecto.");
                    MessageBox.Show("Advertencia: La clave es incorrecta", "Acceso denegado",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                this.Close();
            }
            else
            {
                Interaction.MsgBox("Acceso Permitido. Bienvenido al sistema de registro.",
                    MsgBoxStyle.OkOnly, "Registro correcto");
            }
        }

        private void cargarListas()
        {
            string json = File.ReadAllText("carrerasUniversitarias.json");

            Carreras data = JsonSerializer.Deserialize<Carreras>(json);
            foreach (var carrera in data.carreras_unificadas)
            {
                cbxCarrera.Items.Add(carrera);
            }

            for (int i = 0; i < 11; i++)
            {
                cbx_Semestre.Items.Add((i + 1).ToString());
            }
        }
        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void verRegistroToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

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
        }

        private void SubMenu_guardar_Click(object sender, EventArgs e)
        {
            // VALIDACION DE CAMPOS VACIO
            if (string.IsNullOrWhiteSpace(tbxNombre.Text) ||
                string.IsNullOrWhiteSpace(tbxCedula.Text) ||
                string.IsNullOrWhiteSpace(tbxUsuario.Text) ||
                string.IsNullOrWhiteSpace(tbxPassword.Text) ||
                string.IsNullOrWhiteSpace(tbxConfirmacion.Text) ||
                cbxCarrera.SelectedIndex == -1 ||
                cbx_Semestre.SelectedIndex == -1 ||
                (rbMatutina.Checked == false && rbVespertina.Checked == false))
            {
                MessageBox.Show("Uno de los campos está vacío.", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            //  VALIDACIÓN NOMBRE SIN SIMBOLOS
            foreach (char c in tbxNombre.Text)
            {
                if (!char.IsLetter(c) && !char.IsWhiteSpace(c))
                {
                    MessageBox.Show("El nombre no puede contener símbolos.", "Dato ilógico",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }
            //CANTIDAD MÁXIMA DE CARACTERES
            if (tbxNombre.Text.Length > 30)
            {
                MessageBox.Show("El nombre excede el límite permitido (30 caracteres).",
                    "Dato ilógico", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            // VALIDACION DEL FORMATO DE LA CEDULA
            string patron = @"^\d+-\d+-\d+$";

            if (!Regex.IsMatch(tbxCedula.Text, patron))
            {
                MessageBox.Show("La cédula debe contener números separados por guiones. Ejemplos válidos:\n2-755-39\n02-0755-000039",
                    "Dato ilógico", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            //VALIDACIÓN PASSWORD 
            if (tbxPassword.Text != tbxConfirmacion.Text)
            {
                MessageBox.Show("La contraseña y la confirmación no coinciden.",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            

            //CARGAR O CREAR LA LISTA
            ListaDeEstudiantes data;

            if (File.Exists("Lista de estudiantes.json"))
            {
                string json = File.ReadAllText("Lista de estudiantes.json");
                data = JsonSerializer.Deserialize<ListaDeEstudiantes>(json);
            }
            else
            {
                data = new ListaDeEstudiantes()
                {
                    estudiantes = new List<Estudiante>()
                };
            }

            // CREAR EL NUEVO OBJETO
            Estudiante nuevo = new Estudiante()
            {
                nombre = tbxNombre.Text,
                cedula = tbxCedula.Text,
                carrera = cbxCarrera.SelectedItem.ToString(),
                semestre = int.Parse(cbx_Semestre.SelectedItem.ToString()),
                jornada = rbMatutina.Checked ? "Matutina" : "Vespertina",
                usuario = tbxUsuario.Text,
                password = tbxPassword.Text
            };

            data.estudiantes.Add(nuevo);

          //SE GUARDA EL ARCHIVO
            var opciones = new JsonSerializerOptions
            {
                WriteIndented = true
            };

            string nuevoJson = JsonSerializer.Serialize(data, opciones);
            File.WriteAllText("Lista de estudiantes.json", nuevoJson);

            MessageBox.Show("Registro guardado correctamente.", "Éxito",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void SubMenu_salir_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
