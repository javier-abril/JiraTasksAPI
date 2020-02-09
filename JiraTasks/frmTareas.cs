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
    public partial class frmTareas : Form
    {
        private int usuario;
        private string rutaDB;
        Negocio negocio;
        IEnumerable<Tarea> listaTareas;
        ContextMenu contextMenuNotify = new ContextMenu();
        MenuItem menuNotify = new MenuItem();

        public frmTareas()
        {
            InitializeComponent();
        }

        public frmTareas(int usuario, string rutaDB)
        {
            InitializeComponent();
            this.usuario = usuario;
            this.rutaDB = rutaDB;
            this.negocio = new Negocio(rutaDB);
            comboBox1.SelectedIndexChanged += ComboBox1_SelectedIndexChanged;
            dataGridView1.CellDoubleClick += DataGridView1_CellDoubleClick;
            this.FormClosing += FrmTareas_FormClosing;
            
            try
            {
                listaTareas = negocio.GetTareas();

                configurarIconNotif();

                tareaBindingSource.DataSource = listaTareas;

                dataGridView1.DataSource = tareaBindingSource;

                comboBox1.SelectedIndex = 0;

                notifyIcon1.DoubleClick += NotifyIcon1_DoubleClick;

            }
            catch(Exception ex)
            {
                MessageBox.Show(null, "Se ha producido un error leyendo tareas de DB\n\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void NotifyIcon1_DoubleClick(object sender, EventArgs e)
        {
            if (!this.Visible)
            {
                this.Show();
                this.menuNotify.Text = "Ocultar";
                this.WindowState = FormWindowState.Normal;
            }
            else
            {
                this.Hide();
                menuNotify.Text = "Mostrar";
            }
        }

        private void configurarIconNotif()
        {
            // Asignamos el  menuNotify al contextMenu
            this.contextMenuNotify.MenuItems.AddRange(
                        new MenuItem[] { this.menuNotify });

            // Propiedades y eventos de menu
            this.menuNotify.Index = 0;
            this.menuNotify.Text = "Ocultar";
            this.menuNotify.Click += MenuNotify_Click;

            //Asignamos el contextmenu
            notifyIcon1.ContextMenu = contextMenuNotify;

            //Mostramos el icono en la bandeja de notificación
            notifyIcon1.Visible = true;
        }

        private void MenuNotify_Click(object sender, EventArgs e)
        {
            if (menuNotify.Text == "Ocultar")
            {
                menuNotify.Text = "Mostrar";
                this.Hide();
            }
            else
            {
                menuNotify.Text = "Ocultar";
                this.Show();
                this.WindowState = FormWindowState.Normal;
            }
        }

        /// <summary>
        /// Método que modifica el texto del mensaje del icono de la barra de tareas.
        /// </summary>
        /// <param name="texto"></param>
        private void SetBalloonTip(string texto)
        {
            notifyIcon1.BalloonTipTitle = "Cambios en sus tareas";
            notifyIcon1.BalloonTipText = texto;
            notifyIcon1.BalloonTipIcon = ToolTipIcon.Info;
            notifyIcon1.ShowBalloonTip(15);
        }

        private void DataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                IEnumerable<Tarea> listaT = (IEnumerable<Tarea>)tareaBindingSource.DataSource;

                Tarea tareaSel = listaT.ToList()[e.RowIndex];

                if (!(tareaSel.estadoActual.id == EstadoTarea.Finalizada || 
                        tareaSel.estadoActual.id == EstadoTarea.Cancelada) 
                        && e.ColumnIndex != 8)
                {
                    frmFinalizar frm = new frmFinalizar(this.usuario, this.rutaDB, tareaSel.idTarea, this);
                    frm.Show();
                }
            }
            catch(Exception ex)
            {

            }
        }

        private void ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            filtrarLista();
        }


        private void button1_Click(object sender, EventArgs e)
        {
            frmCrearTarea frm = new frmCrearTarea(this.usuario, this.rutaDB,this);

            frm.Show();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            actualizarDatasource();
        }

        private void filtrarLista()
        {
            dataGridView1.DataSource = null;

            switch (comboBox1.SelectedIndex)
            {
                /*VerVer mis tareas pendientes
                    Ver mis tareas finalizadas
                    Ver mis tareas canceladas
                    Ver todas las tareas
                    Ver todas las pendientes
                    Ver todas las finalizadas*/
                case 0:
                    tareaBindingSource.DataSource = from t in listaTareas
                                                    where t.usuario.idUsuario == usuario &&
                                                          t.estadoActual.id == EstadoTarea.Pendiente
                                                    select t;
                    break;
                case 1:
                    tareaBindingSource.DataSource = from t in listaTareas
                                                    where t.usuario.idUsuario == usuario &&
                                                          t.estadoActual.id == EstadoTarea.Finalizada
                                                    select t;
                    break;
                case 2:
                    tareaBindingSource.DataSource = from t in listaTareas
                                                    where t.usuario.idUsuario == usuario &&
                                                          t.estadoActual.id == EstadoTarea.Cancelada
                                                    select t;
                    break;
                case 3:
                    tareaBindingSource.DataSource = listaTareas;
                    break;

                case 4:
                    tareaBindingSource.DataSource = from t in listaTareas
                                                    where t.estadoActual.id == EstadoTarea.Pendiente
                                                    select t;
                    break;
                case 5:
                    tareaBindingSource.DataSource = from t in listaTareas
                                                    where t.estadoActual.id == EstadoTarea.Finalizada
                                                    select t;
                    break;
                default:
                    break;

            }

            dataGridView1.DataSource = tareaBindingSource;
        }

        public void actualizarDatasource()
        {
            //Cogemos el valor de antes
            IEnumerable<Tarea> listaTemp = (IEnumerable<Tarea>)tareaBindingSource.DataSource;
            IEnumerable<Tarea> listaTemp2;
            List<Tarea> listaDifs = new List<Tarea>();
 
            negocio.RefrescarTareas();

            listaTareas = negocio.GetTareas();

            tareaBindingSource.DataSource = listaTareas;

            dataGridView1.DataSource = tareaBindingSource;

            filtrarLista();

            //Cogemos el valor después
            listaTemp2 = (IEnumerable<Tarea>)tareaBindingSource.DataSource;

            if (listaTemp2.Count() > listaTemp.Count()) {
                SetBalloonTip("Se ha añadido una tarea nueva");
            }
            else {
                foreach (Tarea tar1 in listaTemp)
                {
                    bool encontrado = false;

                    foreach (Tarea tar2 in listaTemp2)
                    {
                        if (tar1.idTarea == tar2.idTarea)
                            encontrado = true;
                    }

                    if (!encontrado)
                        listaDifs.Add(tar1);
                }

                if (listaDifs.Count() > 0)
                {
                    string texto = "";

                    foreach (Tarea tar in listaDifs)
                    {
                        texto = texto + tar.descripcion + "\n";
                    }

                    SetBalloonTip(texto);
                }
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == 8 &&
                    dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString() != "")
                {
                    System.Diagnostics.Process.Start(dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString());
                }
            }
            catch (Exception ex) { }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                this.actualizarDatasource();
            }
            catch(Exception ex)
            {

            }
        }

        private void frmTareas_Resize(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Minimized)
            {
                menuNotify.Text = "Mostrar";
                this.Hide();
            }
        }

        private void FrmTareas_FormClosing(object sender, FormClosingEventArgs e)
        {
                
        }

    }
}
