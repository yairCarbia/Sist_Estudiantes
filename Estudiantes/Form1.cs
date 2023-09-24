using Logica;
using Logica.Library;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Estudiantes
{
    public partial class Form1 : Form
    {
        private LEstudiantes estudiantes;
        private Librarys librarys;

        public Form1()
        {
            InitializeComponent();
              librarys = new Librarys();
            var listTextBox = new List<TextBox>();
            listTextBox.Add(textBoxNid);
            listTextBox.Add(textBoxNombre);
            listTextBox.Add(textBoxApellido);
            listTextBox.Add(textBoxEmail);
            var listLabel = new List<Label>();
            listLabel.Add(labelNid);
            listLabel.Add(labelNombre);
            listLabel.Add(labelApellido);
            listLabel.Add(labelEmail);
            listLabel.Add(labelPaginas);

            Object[] objetos = {pictureBoxImage,
            Properties.Resources.b1, dataGridView1,numericUpDown1};

            estudiantes = new LEstudiantes(listTextBox, listLabel,objetos);


        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            estudiantes.SearchEstudiante(textBoxBuscar.Text);
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            estudiantes.Registrar();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void groupBox4_Enter(object sender, EventArgs e)
        {

        }

        private void buttonEliminar_Click(object sender, EventArgs e)
        {
            estudiantes.Eliminar();
        }
        private void pictureBoxImage_Click(object sender, EventArgs e)
        {
            librarys.uploadimage.CargarImagen(pictureBoxImage);
        }

        private void label5_Click_1(object sender, EventArgs e)
        {

        }

        private void textBoxNid_TextChanged(object sender, EventArgs e)
        {
            if (textBoxNid.Text.Equals(""))
            {
                labelNid.ForeColor = Color.LightSlateGray;
            }
            else
            {
                labelNid.ForeColor = Color.Green;
                labelNid.Text = "DNI";

            }
        }

        private void textBoxNid_KeyPress(object sender, KeyPressEventArgs e)
        {
            librarys.textBoxEvent.numberKeyPress(e);

        }

        private void textBoxApellido_TextChanged(object sender, EventArgs e)
        {
            if (textBoxApellido.Text.Equals(""))
            {
                labelApellido.ForeColor = Color.LightSlateGray;
            }
            else
            {
                labelApellido.ForeColor = Color.Green;
                labelApellido.Text = "APELLIDO";

            }
        }

        private void textBoxApellido_KeyPress(object sender, KeyPressEventArgs e)
        {
            librarys.textBoxEvent.textKeyPress(e);

        }

        private void textBoxEmail_TextChanged(object sender, EventArgs e)
        {
            if (textBoxEmail.Text.Equals(""))
            {
                labelEmail.ForeColor = Color.LightSlateGray;
            }
            else
            {
                labelEmail.ForeColor = Color.Green;
                labelEmail.Text = "EMAIL";

            }
        }

        private void textBoxEmail_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void textBoxNombre_TextChanged(object sender, EventArgs e)
        {
            if (textBoxNombre.Text.Equals(""))
            {
                labelNombre.ForeColor = Color.LightSlateGray;
            }
            else
            {
                labelNombre.ForeColor = Color.Green;
                labelNombre.Text = "NOMBRE";

            }
        }

        private void textBoxNombre_KeyPress(object sender, KeyPressEventArgs e)
        {
            librarys.textBoxEvent.textKeyPress(e);
        }

        private void buttonPrimero_Click(object sender, EventArgs e)
        {
            estudiantes.Paginador("primero");
        }

        private void buttonAnterior_Click(object sender, EventArgs e)
        {
            estudiantes.Paginador("anterior");

        }

        private void buttonSiguiente_Click(object sender, EventArgs e)
        {
            estudiantes.Paginador("siguiente");

        }

        private void buttonUltimo_Click(object sender, EventArgs e)
        {
            estudiantes.Paginador("ultimo");

        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            estudiantes.Registro_Paginas();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.Rows.Count != 0)
                estudiantes.GetEstudiante();
        }

        private void dataGridView1_KeyUp(object sender, KeyEventArgs e)
        {

            if (dataGridView1.Rows.Count != 0)
                estudiantes.GetEstudiante();
        }

        private void buttonCancelar_Click(object sender, EventArgs e)
        {
            estudiantes.Restablecer();
        }
    }
}
