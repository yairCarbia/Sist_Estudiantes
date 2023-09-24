using Data;
using LinqToDB;
using Logica.Library;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Logica
{
    public class LEstudiantes : Librarys
    {
        private List<TextBox> listTextBox;
        private List<Label> listLabel;
        private PictureBox image;
        private Librarys librarys;
        private Bitmap _imgBitmap;
        private DataGridView _dataGridView;
        private NumericUpDown _numericUpDown;
        private Paginador<Estudiante> _paginador;
        private string _accion = "insert";
        public LEstudiantes(List<TextBox> listTextBox, List<Label> listLabel, object[] objetos)
        {
            this.listTextBox = listTextBox;
            this.listLabel = listLabel;
            librarys = new Librarys();
            image = (PictureBox)objetos[0];
            _imgBitmap = (Bitmap)objetos[1];
            _dataGridView = (DataGridView)objetos[2];
            _numericUpDown = (NumericUpDown)objetos[3];
            Restablecer();
        }

        public void Registrar()
        {

            for (int i = 0; i < listTextBox.Count; i++)
            {
                if (listTextBox[i].Text.Equals(""))
                {
                    listLabel[i].Text = "Campo requerido";
                    listLabel[i].ForeColor = Color.Red;
                    listLabel[i].Focus();
                    return;
                }

            }
            if (librarys.textBoxEvent.comprobarFormatoEmail(listTextBox[3].Text))
            {
                var user = _Estudiante.Where(e => e.email.Equals(listTextBox[3].Text)).ToList();
                if (user.Count.Equals(0))
                {
                    Save();
                }
                else
                {
                    if (user[0].id.Equals(_idEstudiante))
                    {
                        Save();
                    }
                    else
                    {      
                    listLabel[3].Text = "Email ya registrado";
                    listLabel[3].ForeColor = Color.Red;
                    listLabel[3].Focus();
                    }
             
                }

            }
            else
            {
                listLabel[3].Text = "Email invalido";
                listLabel[3].ForeColor = Color.Red;
                listLabel[3].Focus();
            }
        }
        public void Save()
        {
            BeginTransactionAsync();
            try
            {
                var arrImage = librarys.uploadimage.ImageToByte(image.Image);
                switch (_accion)
                {
                    case "insert":
                       _Estudiante.Value(e => e.nid, listTextBox[0].Text).
                                        Value(e => e.nombre, listTextBox[1].Text).
                                        Value(e => e.apellido, listTextBox[2].Text).
                                        Value(e => e.email, listTextBox[3].Text).
                                        Value(e => e.image, arrImage).Insert();
                   
                        break;
                    case "update":
                        _Estudiante.Where(u => u.id.Equals(_idEstudiante))
                            .Set(e => e.nid, listTextBox[0].Text)
                            .Set(e => e.nombre, listTextBox[1].Text)
                            .Set(e => e.apellido, listTextBox[2].Text)
                            .Set(e => e.email, listTextBox[3].Text)
                            .Set(e => e.image, arrImage).Update();

                        break;
                }
             
                CommitTransaction();
                Restablecer();
            }
            catch (Exception ex)
            {
                RollbackTransaction();
            }
        }
        private int _reg_por_pag = 2, _num_pag = 1;
        public void SearchEstudiante(string campo)
        {

            List<Estudiante> query = new List<Estudiante>();
            int inicio = (_num_pag - 1) * _reg_por_pag;
            if (campo.Equals(""))
            {
                query = _Estudiante.ToList();
            }
            else
            {
                query = _Estudiante.Where(c => c.nid.StartsWith(campo) ||
                c.nombre.StartsWith(campo) || c.apellido.StartsWith(campo) || c.email.StartsWith(campo)).ToList();
            }
            if (query.Count > 0)
            {
                _dataGridView.DataSource = query.Select(c => new
                {
                    c.id,
                    c.nid,
                    c.nombre,
                    c.apellido,
                    c.email,
                    c.image
                }).Skip(inicio).Take(_reg_por_pag).ToList();
                _dataGridView.Columns[0].Visible = false;
                _dataGridView.Columns[5].Visible = false;

                _dataGridView.Columns[1].DefaultCellStyle.BackColor = Color.WhiteSmoke;
                _dataGridView.Columns[3].DefaultCellStyle.BackColor = Color.WhiteSmoke;

            }else
            {
                _dataGridView.DataSource = query.Select(c => new
                {
                    c.id,
                    c.nid,
                    c.nombre,
                    c.apellido,
                    c.email
                }).ToList();
            }
        }
       private int _idEstudiante = 0;
        public void GetEstudiante()
        {
            _accion = "update";
            _idEstudiante = Convert.ToInt16(_dataGridView.CurrentRow.Cells[0].Value);
            listTextBox[0].Text = Convert.ToString(_dataGridView.CurrentRow.Cells[1].Value);
            listTextBox[1].Text = Convert.ToString(_dataGridView.CurrentRow.Cells[2].Value);
            listTextBox[2].Text = Convert.ToString(_dataGridView.CurrentRow.Cells[3].Value);
            listTextBox[3].Text = Convert.ToString(_dataGridView.CurrentRow.Cells[4].Value);
            try
            {
                byte[] arrImage = (byte[])_dataGridView.CurrentRow.Cells[5].Value;
                image.Image = uploadimage.byteToImage(arrImage);
            }
            catch
            {
                image.Image = _imgBitmap;
            }

        }
        private List<Estudiante> listestudiante;
        public void Paginador(string metodo)
        {
            switch (metodo)
            {
                case "primero":
                    _num_pag = _paginador.primero();
                 break;
                case "anterior":
                    _num_pag = _paginador.anterior();
                    break;
                case "siguiente":
                    _num_pag = _paginador.siguiente();
                    break;
                case "ultimo":
                    _num_pag = _paginador.ultimo();
                    break;
            }
            SearchEstudiante("");
        }
        public void Registro_Paginas()
        {
            _num_pag = 1;
         _reg_por_pag = (int)_numericUpDown.Value;
            var list = _Estudiante.ToList();
            if (0 < list.Count())
            {
                _paginador = new Paginador<Estudiante>(listestudiante, listLabel[4], _reg_por_pag);
                SearchEstudiante("");

            }
        }

        public void Eliminar()
        {
            if (_idEstudiante == 0)
            {

                MessageBox.Show("Seleccione un estudiante");
            }else
            {
                if (MessageBox.Show("Estas seguro de eliminar el estudiante?", 
                    "Eliminar estudiante", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    _Estudiante.Where(c => c.id.Equals(_idEstudiante)).Delete();
                }
            }
        }
        public void Restablecer()
        {
    _accion = "insert";
            _num_pag = 1;
            _idEstudiante = 0;
            image.Image = _imgBitmap;
            listLabel[0].Text = "DNI";
            listLabel[1].Text = "Nombre";
            listLabel[2].Text = "Apellido";
            listLabel[3].Text = "Email";
            listTextBox[0].Text = "";
            listTextBox[1].Text = "";
            listTextBox[2].Text = "";
            listTextBox[3].Text = "";
            listestudiante = _Estudiante.ToList();
            if (0 < listestudiante.Count())
            {
                _paginador = new Paginador<Estudiante>(listestudiante, listLabel[4], _reg_por_pag);
            }
            SearchEstudiante("");
        }
    }
 
       
}

