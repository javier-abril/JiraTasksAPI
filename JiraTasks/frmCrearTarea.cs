using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using JiraTasksNegocio;
using JiraTasksEntidades;

namespace JiraTasks
{
    public partial class frmCrearTarea : Form
    {
        int usuario;
        string dbPath;
        Negocio negocio;
        frmTareas frmTareas;

        public frmCrearTarea()
        {
            InitializeComponent();
        }

        public frmCrearTarea(int usuario, string dbPath, frmTareas frm)
        {
            InitializeComponent();
            this.usuario = usuario;
            this.dbPath = dbPath;
            this.frmTareas = frm;
            negocio = new Negocio(dbPath);
            comboBox1.DataSource = negocio.GetCriticidadesTarea();
            comboBox2.DataSource = negocio.GetEntornos();
            comboBox3.DataSource = negocio.GetTiposTarea();
            comboBox1.SelectedIndex = 0;
            comboBox2.SelectedIndex = 0;
            comboBox3.SelectedIndex = 0;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.TextLength > 0)
            {
                try
                {
                    Tarea tar = new Tarea();

                    tar.descripcion = textBox1.Text;
                    tar.usuario = new Usuario(usuario);
                    tar.nombreUsuario = negocio.GetNombreUsuario(usuario);
                    tar.estadoActual = new EstadoTarea(1,""); //Pendiente;
                    tar.criticidad = new CriticidadTarea((int)comboBox1.SelectedValue,"");
                    tar.entorno = new Entorno((int)comboBox2.SelectedValue,"");
                    tar.tipo = new TipoTarea((int)comboBox3.SelectedValue,"");
                    tar.fecha = DateTime.Now.ToString("dd/MM/yyyy HH:mm");

                    negocio.CrearTarea(tar);
                    frmTareas.actualizarDatasource();
                    this.Close();

                }catch(Exception ex)
                {
                    MessageBox.Show(null, "Se ha producido un error guardando la tarea\n\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
