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

namespace JiraTasks
{
    public partial class frmConfig : Form
    {
        private string rutaDB;
        Negocio negocio;

        public frmConfig()
        {
            InitializeComponent();
        }

        public frmConfig(string rutaDB)
        {
            InitializeComponent();
            this.rutaDB = rutaDB;

            try
            {
                negocio = new Negocio(rutaDB);

                usuarioBindingSource.DataSource = negocio.GetUsuarios();

                comboBox1.DataSource = usuarioBindingSource;
            }
            catch(Exception ex)
            {
                MessageBox.Show(null, "Se ha producido un error conectando la BD\n\n"+ ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                using (System.IO.StreamWriter file =
                new System.IO.StreamWriter("user.cfg", true))
                {
                    file.WriteLine(comboBox1.SelectedValue.ToString());
                }

                frmTareas frm = new frmTareas((int)comboBox1.SelectedValue, rutaDB);
                frm.Show();
                this.Hide();
            }
            catch(Exception ex)
            {
                MessageBox.Show(null, "Se ha producido un error guardando usuario\n\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
