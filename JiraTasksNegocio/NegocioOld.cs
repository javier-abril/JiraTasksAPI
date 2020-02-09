using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JiraTasksDatos;
using JiraTasksEntidades;

namespace JiraTasksNegocio
{
    public class NegocioOld
    {
        /*SQLiteManager sqlmanager;
        string dbPath;

        public NegocioOld(string dbPath)
        {
            this.dbPath = dbPath;
            sqlmanager = new SQLiteManager(dbPath);
        }

        public IEnumerable<Usuario> GetUsuarios()
        {
            return sqlmanager.GetUsuarios();
        }


        public string GetNombreUsuario(int idUsuario)
        {
            Usuario usu = sqlmanager.GetUsuario(idUsuario);

            return usu.nombre;
        }

        public int GetRolUsuario(int idUsuario)
        {
            Usuario usu = sqlmanager.GetUsuario(idUsuario);

            return usu.idRolUsuario;
        }

        public IEnumerable<Tarea> GetTareas()
        {
            return sqlmanager.GetTareasConEstados();
        }

        public Tarea GetTarea(int idTarea)
        {
            return sqlmanager.GetTarea(idTarea);
        }

        public void CrearTarea(Tarea tar)
        {
            sqlmanager.AddTarea(tar);
        }

        public void FinalizarTarea(string text1, string text2, int idTarea, int idUsuario)
        {
            //Si no es usuario con permisos
            if (this.GetRolUsuario(idUsuario) > 2)
                throw new Exception("Su usuario no tiene permisos para finalizar una tarea");

            EstadosTareas est = new EstadosTareas();
            est.descripcion = text1;
            est.fecha = DateTime.Now;
            est.idEstado = 2; //Finalizada
            est.idTarea = idTarea;

            sqlmanager.AddEstadosTareas(est);

            Tarea tar = sqlmanager.GetTarea(idTarea);
            tar.estadoActual = "Finalizada";
            tar.urlJira = text2;

            sqlmanager.ModificaTarea(tar);
        }

        public void CancelarTarea(int idTarea)
        {
            EstadosTareas est = new EstadosTareas();
            est.descripcion = "";
            est.fecha = DateTime.Now;
            est.idEstado = 3; //Cancelada
            est.idTarea = idTarea;

            sqlmanager.AddEstadosTareas(est);

            Tarea tar = sqlmanager.GetTarea(idTarea);
            tar.estadoActual = "Cancelada";
 
            sqlmanager.ModificaTarea(tar);
        }*/
        
    }
}
