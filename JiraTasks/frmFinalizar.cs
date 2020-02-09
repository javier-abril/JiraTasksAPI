using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using JiraTasksEntidades;
using JiraTasksNegocio;

namespace JiraTasks
{
    public partial class frmFinalizar : Form
    {
        int idUsuario;
        string rutaDB;
        int idTarea;
        Negocio negocio;
        frmTareas frmTareas;
        Tarea tarea;

        public frmFinalizar()
        {
            InitializeComponent();
        }

        public frmFinalizar(int idUsuario, string rutaDB, int idTarea, frmTareas frmTareas)
        {
            InitializeComponent();
            this.idUsuario = idUsuario;
            this.rutaDB = rutaDB;
            this.idTarea = idTarea;
            this.frmTareas = frmTareas;
            negocio = new Negocio(rutaDB);

            this.tarea = negocio.GetTarea(idTarea);

            //Si no es el usuario que la ha creado no la puede cancelar
            if (tarea.usuario.idUsuario != idUsuario)
                button2.Visible = false;

            textBox1.Text = tarea.nombreUsuario;
            textBox2.Text = tarea.estadoActual.nombre;
            textBox3.Text = tarea.descripcion;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(txtAnotacion.TextLength > 0)
            {
                try
                {
                    negocio.FinalizarTarea(txtAnotacion.Text,  txtURL.Text, idTarea);
                    frmTareas.actualizarDatasource();
                    this.Close();
                }
                catch(Exception ex)
                {
                    MessageBox.Show(null, "Se ha producido un error finalizando tarea\n\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show(null, "Debe añadir la anotación\n\n", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult dg = MessageBox.Show(null, "¿Está seguro de cancelar la tarea?", "Atención", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (dg == DialogResult.Yes)
            {

                try
                {
                    negocio.CancelarTarea(idTarea, "Cancelada por usuario");
                    frmTareas.actualizarDatasource();
                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(null, "Se ha producido un error cancelando tarea\n\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
