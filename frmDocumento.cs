using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace simpleCRUD
{
    public partial class frmDocumento : Form
    {
        private string action = "";
        public frmDocumento()
        {
            InitializeComponent();
        }
      

        private void Form1_Load(object sender, EventArgs e)
        {
            //deja un tab 
            tabs.TabPages.Remove(tabForm);

            //carga los datos en el datagridView
            //deshabilita controles
            fillDataGridView();
            controlsDisable();

        }

        //utilizado para mostrar los registros en el datagridview
        public void fillDataGridView()
        {
            //instancia de la clase documento| Documento
            Documento documento = new Documento();

            clearDataGridView();

            dtgDocuments.Columns.Add("documentoId", "DOCUMENTO ID");
            dtgDocuments.Columns.Add("titulo", "TITULO");
            dtgDocuments.Columns.Add("contenido", "CONTENIDO");
            dtgDocuments.Columns.Add("fechapublicacion", "FECHAPUBLICACION");
            dtgDocuments.Columns.Add("descripcion",  "DESCRIPCION");

            //llamado al medoto getDocumento() de la clase Documento

            //leer el resultado y mostrarlo en el datagridview
            while (documento.getAllDocumento().Read())
            {
                dtgDocuments.Rows.Add(
                        documento.getAllDocumento().GetValue(0),
                        documento.getAllDocumento().GetValue(1),
                        documento.getAllDocumento().GetValue(2),
                        documento.getAllDocumento().GetValue(3),
                        documento.getAllDocumento().GetValue(4)
                       );
            }
        }

        public void clearDataGridView()
        {
            dtgDocuments.Columns.Clear();
            dtgDocuments.Rows.Clear();
        }
        //deshabilita los controles de formulario
        public void controlsDisable()
        {
            txtId.Enabled = false;
            txtTitulo.Enabled = false;
            txtContenido.Enabled = false;
            txtDescripcion.Enabled = false;
            dtFechapublicacion.Enabled = false;
            btnGuardar.Enabled = false;
            btnCancelar.Enabled = false;
        }
        //habilitar los controles de formulario
        public void controlsEnable()
        {
            txtId.Enabled = false;
            txtTitulo.Enabled = true;
            txtContenido.Enabled = true;
            txtDescripcion.Enabled = true;
            dtFechapublicacion.Enabled = true;
            btnGuardar.Enabled = true;
            btnCancelar.Enabled = true;
        }
        //limpiar el contenido de los controles
        public void clearControls()
        {
            txtId.Text = "";
            txtTitulo.Text = "";
            txtContenido.Text = "";
            txtDescripcion.Text = "";
            dtFechapublicacion.Text = "";
        }

          
       
        private void dtgBooks_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {
            tabs.TabPages.Remove(tabData);//ocultar el tab de el datagridview
            tabs.TabPages.Add(tabForm); //mostrar el formulario para los datos
            tabs.TabPages[0].Text = "EDIT DOCUMENTO";

            action = "edit";
            controlsEnable();

            txtId.Visible = true;
            txtId.ReadOnly = true;
            lblId.Visible = true;

            //cargar datos en controles
        }

        private void btnSave_Click_1(object sender, EventArgs e)
        {
            //mediante este boton se programara para guardar y editar
        }

      

        private void btnExit_Click(object sender, EventArgs e)
        {
            //codigo del boton salir
            string mensaje = "¿Está seguró que desea salir?";
            if (MetroFramework.MetroMessageBox.Show(this, mensaje, "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Stop) == DialogResult.Yes)
            {
                this.Close();
            }

        }

        private void lNew_Click(object sender, EventArgs e)
        {
            tabs.TabPages.Add(tabForm);//mostrar el formulario
            tabs.TabPages.Remove(tabData); //ocultar el  tab del dataagridview
            tabs.TabPages[0].Text = "NEW DOCUMENTO"; //agregar texto al tab

            txtId.Visible = false;
            lblId.Visible = false;
            txtTitulo.Focus(); //enviar enfoque al titulo
            action = "new";
            controlsEnable();
            clearControls();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            string mensaje = "¿Está seguró que desea cancelar?";
            if (MetroFramework.MetroMessageBox.Show(this, mensaje, "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                clearControls();
                controlsDisable();


                tabs.TabPages.Remove(tabForm);
                tabs.TabPages.Add(tabData);
                tabs.TabPages[0].Text = "DOCUMENTO LISTA";
            }
        }

        private void tabForm_Click(object sender, EventArgs e)
        {

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (txtTitulo.Text == "")
            {
                MetroFramework.MetroMessageBox.Show(this, "Debe escribir el titulo", "VALIDACION",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTitulo.Focus(); //enviamos el enfoque a la caja de texto
                
            }
            else
            {

                Documento documento = new Documento(); //instancia de la clase Libro
                                        //evaluar la accion
                if (action == "edit")
                {
                    documento._documentoId = Convert.ToInt32(txtId.Text);
                }


                documento._titulo = txtTitulo.Text;
                documento._contenido = txtContenido.Text;
                documento._fechapublicacion = dtFechapublicacion.Text;
                documento._descripcion = txtDescripcion.Text;

                string mensaje = "Esta seguro que desea guardar el registro?";
                if (MetroFramework.MetroMessageBox.Show(this, mensaje, "CONFIRMACION",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    //LLAMAR AL METODO PARA GUARDAR
                    if (documento.newEditDocumento(action))
                    {
                        MetroFramework.MetroMessageBox.Show(this, "Los datos se han guardado exitosamente!",
                            "CONFIRMACION", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MetroFramework.MetroMessageBox.Show(this, "Los datos  no se han guardado!",
                            "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                    clearControls();
                    controlsDisable();
                    fillDataGridView();
                    tabs.TabPages.Remove(tabForm);
                    tabs.TabPages.Add(tabData);
                    tabs.TabPages[0].Text = "DOCUMENTO LIST";
                }
            }
        }

        private void edit_Click(object sender, EventArgs e)
        {
            tabs.TabPages.Remove(tabData);
            tabs.TabPages.Add(tabForm);
            tabs.TabPages[0].Text = "EDIT DOCUMENTO";
            controlsEnable();

            txtId.Visible = true;
            txtId.ReadOnly = true;
            lblId.Visible = true;

            //pasar los valores del datagridview hacia los texbox
            txtId.Text = dtgDocuments.CurrentRow.Cells[0].Value.ToString();
            txtTitulo.Text = dtgDocuments.CurrentRow.Cells[1].Value.ToString();
            txtContenido.Text = dtgDocuments.CurrentRow.Cells[2].Value.ToString();
            txtDescripcion.Text = dtgDocuments.CurrentRow.Cells[3].Value.ToString();
            dtFechapublicacion.Text = dtgDocuments.CurrentRow.Cells[4].Value.ToString();

            //enviar aaccion
            action = "edit";
        }

        private void delete_Click(object sender, EventArgs e)
        {
            string mensaje = "Esta seguro que desea eliminar el registro?";
            if (MetroFramework.MetroMessageBox.Show(this, mensaje, "CONFIRMACION",
               MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                Documento book = new Documento();
                book._documentoId = Convert.ToInt32(dtgDocuments.CurrentRow.Cells[0].Value);

                //llamado al metodo deleteBook() de la clase Book
                if (book.deleteDocuemnto())
                {
                    MetroFramework.MetroMessageBox.Show(this, "Los datos se han eliminado exitosamente!",
                        "CONFIRMACION", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    //actualizar datagridview
                    fillDataGridView();

                }
                else
                {
                    MetroFramework.MetroMessageBox.Show(this, "Los datos  no se han podido eliminar",
                        "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void lblId_Click(object sender, EventArgs e)
        {

        }

        private void metroLabel2_Click(object sender, EventArgs e)
        {

        }
    }

}
