using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace JiraTasks
{
    static class Program
    {
        /// <summary>
        /// Punto de entrada principal para la aplicación.
        /// </summary>
        [STAThread]
        static void Main()
        {

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            string rutaDB;

            if (File.Exists("db.cfg"))
            {
                using (StreamReader sr = new StreamReader("db.cfg"))
                {
                    rutaDB = sr.ReadLine();
                }

                if (File.Exists("user.cfg"))
                {
                    try
                    {
                        using (StreamReader sr = new StreamReader("user.cfg"))
                        {
                            string usuario = sr.ReadLine();

                            Application.Run(new frmTareas(Convert.ToInt32(usuario), rutaDB));
                        }
                    }
                    catch (IOException e)
                    {
                        MessageBox.Show(null, "No se puede leer fichero de configuración user.cfg", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                }
                else
                    Application.Run(new frmConfig(rutaDB));

            }
            else
                MessageBox.Show(null, "No se puede leer fichero de configuración db.cfg", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);



        }

    }
}
