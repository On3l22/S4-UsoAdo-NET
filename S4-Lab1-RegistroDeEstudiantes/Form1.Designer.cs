namespace S4_Lab1_RegistroDeEstudiantes
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            tbxCedula = new TextBox();
            lblNombre = new Label();
            groupBox1 = new GroupBox();
            cbx_Semestre = new ComboBox();
            cbxCarrera = new ComboBox();
            rbVespertina = new RadioButton();
            rbMatutina = new RadioButton();
            tbxNombre = new TextBox();
            lblJornada = new Label();
            lblSemestre = new Label();
            lblCarrera = new Label();
            lblCedula = new Label();
            groupBox2 = new GroupBox();
            label8 = new Label();
            label7 = new Label();
            label6 = new Label();
            tbxConfirmacion = new TextBox();
            tbxPassword = new TextBox();
            tbxUsuario = new TextBox();
            menuStrip1 = new MenuStrip();
            Menu = new ToolStripMenuItem();
            SubMenu_nuevo = new ToolStripMenuItem();
            SubMenu_guardar = new ToolStripMenuItem();
            SubMenu_salir = new ToolStripMenuItem();
            MenuRegistros = new ToolStripMenuItem();
            SubMenuEstudiante = new ToolStripMenuItem();
            MenuAyuda = new ToolStripMenuItem();
            SubMenuAcerca = new ToolStripMenuItem();
            autoresOnelMOliverGToolStripMenuItem = new ToolStripMenuItem();
            version11aToolStripMenuItem = new ToolStripMenuItem();
            chbx_Terminos = new CheckBox();
            chbx_Notificaciones = new CheckBox();
            groupBox3 = new GroupBox();
            dataGridView1 = new DataGridView();
            groupBox1.SuspendLayout();
            groupBox2.SuspendLayout();
            menuStrip1.SuspendLayout();
            groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // tbxCedula
            // 
            tbxCedula.Location = new Point(80, 64);
            tbxCedula.Margin = new Padding(2, 2, 2, 2);
            tbxCedula.MaxLength = 11;
            tbxCedula.Name = "tbxCedula";
            tbxCedula.Size = new Size(336, 23);
            tbxCedula.TabIndex = 0;
            tbxCedula.TextChanged += tbxCedula_TextChanged;
            tbxCedula.KeyPress += tbxCedula_KeyPress;
            // 
            // lblNombre
            // 
            lblNombre.AutoSize = true;
            lblNombre.Location = new Point(19, 39);
            lblNombre.Margin = new Padding(2, 0, 2, 0);
            lblNombre.Name = "lblNombre";
            lblNombre.Size = new Size(54, 15);
            lblNombre.TabIndex = 1;
            lblNombre.Text = "Nombre:";
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(cbx_Semestre);
            groupBox1.Controls.Add(cbxCarrera);
            groupBox1.Controls.Add(rbVespertina);
            groupBox1.Controls.Add(rbMatutina);
            groupBox1.Controls.Add(tbxNombre);
            groupBox1.Controls.Add(lblJornada);
            groupBox1.Controls.Add(tbxCedula);
            groupBox1.Controls.Add(lblSemestre);
            groupBox1.Controls.Add(lblCarrera);
            groupBox1.Controls.Add(lblCedula);
            groupBox1.Controls.Add(lblNombre);
            groupBox1.Location = new Point(24, 39);
            groupBox1.Margin = new Padding(2, 2, 2, 2);
            groupBox1.Name = "groupBox1";
            groupBox1.Padding = new Padding(2, 2, 2, 2);
            groupBox1.Size = new Size(435, 185);
            groupBox1.TabIndex = 2;
            groupBox1.TabStop = false;
            groupBox1.Text = "Datos del alumno";
            // 
            // cbx_Semestre
            // 
            cbx_Semestre.DropDownStyle = ComboBoxStyle.DropDownList;
            cbx_Semestre.FormattingEnabled = true;
            cbx_Semestre.Location = new Point(80, 115);
            cbx_Semestre.Margin = new Padding(2, 2, 2, 2);
            cbx_Semestre.Name = "cbx_Semestre";
            cbx_Semestre.Size = new Size(336, 23);
            cbx_Semestre.TabIndex = 11;
            // 
            // cbxCarrera
            // 
            cbxCarrera.DropDownStyle = ComboBoxStyle.DropDownList;
            cbxCarrera.FormattingEnabled = true;
            cbxCarrera.Location = new Point(80, 89);
            cbxCarrera.Margin = new Padding(2, 2, 2, 2);
            cbxCarrera.Name = "cbxCarrera";
            cbxCarrera.Size = new Size(336, 23);
            cbxCarrera.TabIndex = 10;
            cbxCarrera.SelectedIndexChanged += cbxCarrera_SelectedIndexChanged;
            // 
            // rbVespertina
            // 
            rbVespertina.AutoSize = true;
            rbVespertina.Location = new Point(166, 146);
            rbVespertina.Margin = new Padding(2, 2, 2, 2);
            rbVespertina.Name = "rbVespertina";
            rbVespertina.Size = new Size(78, 19);
            rbVespertina.TabIndex = 9;
            rbVespertina.TabStop = true;
            rbVespertina.Text = "Verpertina";
            rbVespertina.UseVisualStyleBackColor = true;
            // 
            // rbMatutina
            // 
            rbMatutina.AutoSize = true;
            rbMatutina.Location = new Point(80, 146);
            rbMatutina.Margin = new Padding(2, 2, 2, 2);
            rbMatutina.Name = "rbMatutina";
            rbMatutina.Size = new Size(73, 19);
            rbMatutina.TabIndex = 8;
            rbMatutina.TabStop = true;
            rbMatutina.Text = "Matutina";
            rbMatutina.UseVisualStyleBackColor = true;
            // 
            // tbxNombre
            // 
            tbxNombre.Location = new Point(80, 35);
            tbxNombre.Margin = new Padding(2, 2, 2, 2);
            tbxNombre.MaxLength = 30;
            tbxNombre.Name = "tbxNombre";
            tbxNombre.Size = new Size(336, 23);
            tbxNombre.TabIndex = 2;
            tbxNombre.TextChanged += tbxNombre_TextChanged;
            // 
            // lblJornada
            // 
            lblJornada.AutoSize = true;
            lblJornada.Location = new Point(22, 149);
            lblJornada.Margin = new Padding(2, 0, 2, 0);
            lblJornada.Name = "lblJornada";
            lblJornada.Size = new Size(51, 15);
            lblJornada.TabIndex = 5;
            lblJornada.Text = "Jornada:";
            // 
            // lblSemestre
            // 
            lblSemestre.AutoSize = true;
            lblSemestre.Location = new Point(14, 120);
            lblSemestre.Margin = new Padding(2, 0, 2, 0);
            lblSemestre.Name = "lblSemestre";
            lblSemestre.Size = new Size(58, 15);
            lblSemestre.TabIndex = 4;
            lblSemestre.Text = "Semestre:";
            // 
            // lblCarrera
            // 
            lblCarrera.AutoSize = true;
            lblCarrera.Location = new Point(26, 94);
            lblCarrera.Margin = new Padding(2, 0, 2, 0);
            lblCarrera.Name = "lblCarrera";
            lblCarrera.Size = new Size(48, 15);
            lblCarrera.TabIndex = 3;
            lblCarrera.Text = "Carrera:";
            // 
            // lblCedula
            // 
            lblCedula.AutoSize = true;
            lblCedula.Location = new Point(27, 65);
            lblCedula.Margin = new Padding(2, 0, 2, 0);
            lblCedula.Name = "lblCedula";
            lblCedula.Size = new Size(47, 15);
            lblCedula.TabIndex = 2;
            lblCedula.Text = "Cédula:";
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(label8);
            groupBox2.Controls.Add(label7);
            groupBox2.Controls.Add(label6);
            groupBox2.Controls.Add(tbxConfirmacion);
            groupBox2.Controls.Add(tbxPassword);
            groupBox2.Controls.Add(tbxUsuario);
            groupBox2.Location = new Point(476, 39);
            groupBox2.Margin = new Padding(2, 2, 2, 2);
            groupBox2.Name = "groupBox2";
            groupBox2.Padding = new Padding(2, 2, 2, 2);
            groupBox2.Size = new Size(345, 127);
            groupBox2.TabIndex = 3;
            groupBox2.TabStop = false;
            groupBox2.Text = "Credenciales";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(24, 85);
            label8.Margin = new Padding(2, 0, 2, 0);
            label8.Name = "label8";
            label8.Size = new Size(83, 15);
            label8.TabIndex = 5;
            label8.Text = "Confirmación:";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(36, 61);
            label7.Margin = new Padding(2, 0, 2, 0);
            label7.Name = "label7";
            label7.Size = new Size(70, 15);
            label7.TabIndex = 4;
            label7.Text = "Contraseña:";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(56, 37);
            label6.Margin = new Padding(2, 0, 2, 0);
            label6.Name = "label6";
            label6.Size = new Size(50, 15);
            label6.TabIndex = 3;
            label6.Text = "Usuario:";
            // 
            // tbxConfirmacion
            // 
            tbxConfirmacion.Location = new Point(113, 80);
            tbxConfirmacion.Margin = new Padding(2, 2, 2, 2);
            tbxConfirmacion.Name = "tbxConfirmacion";
            tbxConfirmacion.PasswordChar = '*';
            tbxConfirmacion.Size = new Size(210, 23);
            tbxConfirmacion.TabIndex = 2;
            tbxConfirmacion.TextChanged += tbxConfirmacion_TextChanged;
            // 
            // tbxPassword
            // 
            tbxPassword.Location = new Point(113, 58);
            tbxPassword.Margin = new Padding(2, 2, 2, 2);
            tbxPassword.MaxLength = 12;
            tbxPassword.Name = "tbxPassword";
            tbxPassword.PasswordChar = '*';
            tbxPassword.Size = new Size(210, 23);
            tbxPassword.TabIndex = 1;
            // 
            // tbxUsuario
            // 
            tbxUsuario.Location = new Point(113, 33);
            tbxUsuario.Margin = new Padding(2, 2, 2, 2);
            tbxUsuario.MaxLength = 30;
            tbxUsuario.Name = "tbxUsuario";
            tbxUsuario.ReadOnly = true;
            tbxUsuario.Size = new Size(210, 23);
            tbxUsuario.TabIndex = 0;
            // 
            // menuStrip1
            // 
            menuStrip1.ImageScalingSize = new Size(24, 24);
            menuStrip1.Items.AddRange(new ToolStripItem[] { Menu, MenuRegistros, MenuAyuda });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Padding = new Padding(4, 1, 0, 1);
            menuStrip1.Size = new Size(885, 24);
            menuStrip1.TabIndex = 4;
            menuStrip1.Text = "menuStrip1";
            // 
            // Menu
            // 
            Menu.DropDownItems.AddRange(new ToolStripItem[] { SubMenu_nuevo, SubMenu_guardar, SubMenu_salir });
            Menu.Name = "Menu";
            Menu.Size = new Size(60, 22);
            Menu.Text = "Archivo";
            // 
            // SubMenu_nuevo
            // 
            SubMenu_nuevo.Name = "SubMenu_nuevo";
            SubMenu_nuevo.Size = new Size(199, 22);
            SubMenu_nuevo.Text = "Nuevo                Ctrl + N";
            SubMenu_nuevo.Click += SubMenu_nuevo_Click;
            // 
            // SubMenu_guardar
            // 
            SubMenu_guardar.Name = "SubMenu_guardar";
            SubMenu_guardar.Size = new Size(199, 22);
            SubMenu_guardar.Text = "Guardar              Ctrl + S";
            SubMenu_guardar.Click += SubMenu_guardar_Click;
            // 
            // SubMenu_salir
            // 
            SubMenu_salir.Name = "SubMenu_salir";
            SubMenu_salir.Size = new Size(199, 22);
            SubMenu_salir.Text = "Salir                    Ctrl + Q";
            SubMenu_salir.Click += SubMenu_salir_Click;
            // 
            // MenuRegistros
            // 
            MenuRegistros.DropDownItems.AddRange(new ToolStripItem[] { SubMenuEstudiante });
            MenuRegistros.Name = "MenuRegistros";
            MenuRegistros.Size = new Size(67, 22);
            MenuRegistros.Text = "Registros";
            // 
            // SubMenuEstudiante
            // 
            SubMenuEstudiante.Name = "SubMenuEstudiante";
            SubMenuEstudiante.Size = new Size(134, 22);
            SubMenuEstudiante.Text = "Estudiantes";
            SubMenuEstudiante.Click += verRegistroToolStripMenuItem_Click;
            // 
            // MenuAyuda
            // 
            MenuAyuda.DropDownItems.AddRange(new ToolStripItem[] { SubMenuAcerca });
            MenuAyuda.Name = "MenuAyuda";
            MenuAyuda.Size = new Size(53, 22);
            MenuAyuda.Text = "Ayuda";
            // 
            // SubMenuAcerca
            // 
            SubMenuAcerca.DropDownItems.AddRange(new ToolStripItem[] { autoresOnelMOliverGToolStripMenuItem, version11aToolStripMenuItem });
            SubMenuAcerca.Name = "SubMenuAcerca";
            SubMenuAcerca.Size = new Size(135, 22);
            SubMenuAcerca.Text = "Acerca de...";
            // 
            // autoresOnelMOliverGToolStripMenuItem
            // 
            autoresOnelMOliverGToolStripMenuItem.Name = "autoresOnelMOliverGToolStripMenuItem";
            autoresOnelMOliverGToolStripMenuItem.Size = new Size(224, 22);
            autoresOnelMOliverGToolStripMenuItem.Text = "Autores: Onel M. && Oliver G.";
            // 
            // version11aToolStripMenuItem
            // 
            version11aToolStripMenuItem.Name = "version11aToolStripMenuItem";
            version11aToolStripMenuItem.Size = new Size(224, 22);
            version11aToolStripMenuItem.Text = "Version 1.1a";
            // 
            // chbx_Terminos
            // 
            chbx_Terminos.AutoSize = true;
            chbx_Terminos.Location = new Point(476, 185);
            chbx_Terminos.Margin = new Padding(2, 2, 2, 2);
            chbx_Terminos.Name = "chbx_Terminos";
            chbx_Terminos.Size = new Size(292, 19);
            chbx_Terminos.TabIndex = 5;
            chbx_Terminos.Text = "Acepta los terminos y condiciones de la aplicación";
            chbx_Terminos.UseVisualStyleBackColor = true;
            // 
            // chbx_Notificaciones
            // 
            chbx_Notificaciones.AutoSize = true;
            chbx_Notificaciones.Location = new Point(476, 206);
            chbx_Notificaciones.Margin = new Padding(2, 2, 2, 2);
            chbx_Notificaciones.Name = "chbx_Notificaciones";
            chbx_Notificaciones.Size = new Size(139, 19);
            chbx_Notificaciones.TabIndex = 6;
            chbx_Notificaciones.Text = "Recibir notificaciones";
            chbx_Notificaciones.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            groupBox3.Controls.Add(dataGridView1);
            groupBox3.Location = new Point(24, 250);
            groupBox3.Margin = new Padding(2, 2, 2, 2);
            groupBox3.Name = "groupBox3";
            groupBox3.Padding = new Padding(2, 2, 2, 2);
            groupBox3.Size = new Size(797, 187);
            groupBox3.TabIndex = 7;
            groupBox3.TabStop = false;
            groupBox3.Text = "Tabla de Registros";
            // 
            // dataGridView1
            // 
            dataGridView1.ColumnHeadersHeight = 34;
            dataGridView1.Location = new Point(0, 18);
            dataGridView1.Margin = new Padding(2, 2, 2, 2);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.ReadOnly = true;
            dataGridView1.RowHeadersWidth = 62;
            dataGridView1.Size = new Size(797, 169);
            dataGridView1.TabIndex = 0;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.Control;
            ClientSize = new Size(885, 449);
            Controls.Add(groupBox3);
            Controls.Add(chbx_Notificaciones);
            Controls.Add(chbx_Terminos);
            Controls.Add(groupBox2);
            Controls.Add(groupBox1);
            Controls.Add(menuStrip1);
            MainMenuStrip = menuStrip1;
            Margin = new Padding(2, 2, 2, 2);
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Formulario Principal";
            FormClosing += Form1_FormClosing;
            Shown += Form1_Shown;
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            groupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox tbxCedula;
        private Label lblNombre;
        private GroupBox groupBox1;
        private GroupBox groupBox2;
        private TextBox tbxNombre;
        private Label lblJornada;
        private Label lblSemestre;
        private Label lblCarrera;
        private Label lblCedula;
        private Label label8;
        private Label label7;
        private Label label6;
        private TextBox tbxConfirmacion;
        private TextBox tbxPassword;
        private TextBox tbxUsuario;
        private ComboBox cbxCarrera;
        private RadioButton rbVespertina;
        private RadioButton rbMatutina;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem Menu;
        private ToolStripMenuItem SubMenu_nuevo;
        private ToolStripMenuItem SubMenu_guardar;
        private ToolStripMenuItem SubMenu_salir;
        private ToolStripMenuItem MenuAyuda;
        private ToolStripMenuItem SubMenuAcerca;
        private CheckBox chbx_Terminos;
        private CheckBox chbx_Notificaciones;
        private ToolStripMenuItem MenuRegistros;
        private ToolStripMenuItem SubMenuEstudiante;
        private ComboBox cbx_Semestre;
        private GroupBox groupBox3;
        private DataGridView dataGridView1;
        private ToolStripMenuItem autoresOnelMOliverGToolStripMenuItem;
        private ToolStripMenuItem version11aToolStripMenuItem;
    }
}
