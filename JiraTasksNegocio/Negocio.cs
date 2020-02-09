using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JiraTasksDatos;
using JiraTasksEntidades;

namespace JiraTasksNegocio
{
    public class Negocio
    {
        string connectionString;
        MySQLManager mySQLManager;

        public Negocio(string connectionString)
        {
            this.connectionString = connectionString;
            mySQLManager = new MySQLManager(connectionString);
        }

        public IEnumerable<Tarea> GetTareas()
        {
            return mySQLManager.GetTareas();
        }

        /// <summary>
        /// Refresca el dataset con los cambios en la tabla Tareas y EstadosTareas
        /// </summary>
        public void RefrescarTareas()
        {
            mySQLManager.RefrescaTareas();
        }

        public Tarea GetTarea(int idTarea)
        {
            return mySQLManager.GetTarea(idTarea);
        }
      

        public IEnumerable<Usuario> GetUsuarios()
        {
            return mySQLManager.GetUsuarios();
        }
       

        public string GetNombreUsuario(int usuario)
        {
            return mySQLManager.GetUsuario(usuario).nombre;
        }

        public Usuario GetUsuario(int id)
        {
            return mySQLManager.GetUsuario(id);
        }

        public IEnumerable<CriticidadTarea> GetCriticidadesTarea()
        {
            return mySQLManager.GetCriticidadesTarea();
        }

        public IEnumerable<TipoTarea> GetTiposTarea()
        {
            return mySQLManager.GetTiposTarea();
        }

        public IEnumerable<Entorno> GetEntornos()
        {
            return mySQLManager.GetEntornos();
        }

        public void CrearTarea(Tarea tar)
        {
            mySQLManager.CrearTarea(tar);
        }

        public void CancelarTarea(int idTarea, string descripcion)
        {
            mySQLManager.CancelarTarea(mySQLManager.GetTarea(idTarea), descripcion);
        }

        public void FinalizarTarea(string descripcion, string urlJira, int idTarea)
        {
            Tarea tartemp = mySQLManager.GetTarea(idTarea);

            tartemp.urlJira = urlJira;

            mySQLManager.FinalizarTarea(tartemp, descripcion);
        }
    }
}
