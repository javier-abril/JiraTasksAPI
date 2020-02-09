///<author> Francisco Javier Abril Lopez</author>
///
/*
using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
using System.IO;
using JiraTasksEntidades;

namespace JiraTasksDatos
{

    ///<author> Francisco Javier Abril López</author>

    public class SQLiteManager
    {
        string dbPath;
        SQLiteConnection db;

        public SQLiteManager(string dbPath)
        {

            if (!File.Exists(dbPath))
            {
                //Creamos DB
                db = new SQLiteConnection(dbPath);
                db.CreateTable<Estado>();
                db.CreateTable<EstadosTareas>();
                db.CreateTable<Rol>();
                db.CreateTable<Tarea>();
                db.CreateTable<Usuario>();
            }
            else
                db = new SQLiteConnection(dbPath);

        }

        ~SQLiteManager()
        {
            db.Close();
        }

        public int AddUsuario(Usuario usuario)
        {
            try
            {
                db.Insert(usuario);
                //devolvemos el id del ultimo registro insertado
                return db.ExecuteScalar<int>("SELECT last_insert_rowid()");

            }
            catch(Exception ex)
            {
                return -1;
            }
        }

        public void RemoveUsuario(Usuario usuario)
        {
            db.Delete(usuario);
        }

        public void ModificaUsuario(Usuario usuario)
        {

             db.Update(usuario);

        }
        
        public int AddEstadosTareas(EstadosTareas estadosTareas)
        {
            try
            {
                db.Insert(estadosTareas);
                //devolvemos el id del ultimo registro insertado
                return db.ExecuteScalar<int>("SELECT last_insert_rowid()");

            }
            catch (Exception ex)
            {
                return -1;
            }
        }

        public int AddTarea(Tarea tarea)
        {
            try
            {
                db.Insert(tarea);
                //devolvemos el id del ultimo registro insertado
                int idTarea = db.ExecuteScalar<int>("SELECT last_insert_rowid()");
                EstadosTareas estTar = new EstadosTareas();
                estTar.idEstado = 1; //pendiente
                estTar.idTarea = idTarea;
                estTar.fecha = DateTime.Now;

                this.AddEstadosTareas(estTar);

                return idTarea;
            }
            catch (Exception ex)
            {
                return -1;
            }
        }

        public void ModificaTarea(Tarea tarea)
        {
            db.Update(tarea);
        }
        
        public string GetEstado(int id)
        {
            Estado est = db.Table<Estado>().Where(e => e.idEstado == id).First();
            return est.nombre;
        }

        public IEnumerable<Tarea> GetTareas()
        {
            return db.Table<Tarea>();
        }

        public IEnumerable<Tarea> GetTareasConEstados()
        {
            IEnumerable<Tarea> tareas = this.GetTareas();
            List<Tarea> tareasRet = new List<Tarea>();

            foreach (Tarea t in tareas)
            {
                EstadosTareas est = this.GetEstadoActualTarea(t.idTarea);
                //recuperamos el texto de estado con el id de estadostareas
                t.estadoActual = this.GetEstado(est.idEstado);
                t.descripcionEstado = est.descripcion;
                t.fecha = est.fecha.ToShortDateString() + " " + est.fecha.ToShortTimeString();
                tareasRet.Add(t);
            }

            return tareasRet;
        }

        public Tarea GetTarea(int idTarea)
        {
            return db.Table<Tarea>().Where(t=>t.idTarea == idTarea).First();
        }

        public IEnumerable<Usuario> GetUsuarios()
        {
            return db.Table<Usuario>();
        }

        public Usuario GetUsuario(int idUsuario)
        {
            return db.Table<Usuario>().Where(u => u.idUsuario == idUsuario).First();
        }

        public IEnumerable<EstadosTareas> GetEstadosTareas()
        {
            return db.Table<EstadosTareas>();
        }

        public EstadosTareas GetEstadoActualTarea(int idTarea)
        {
            return db.Table<EstadosTareas>().Where(t => t.idTarea == idTarea)
                                            .OrderByDescending(t => t.id)
                                            .First();
        }

        

        public void CerrarConexion()
        {
            db.Close();
        }

        public void IniciarConexion()
        {
            db = new SQLiteConnection(dbPath);
        }

        public string CalculateMD5Hash(string input)
        {
            // step 1, calculate MD5 hash from input
            System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create();
            byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
            byte[] hash = md5.ComputeHash(inputBytes);

            // step 2, convert byte array to hex string

            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < hash.Length; i++)
            {
                sb.Append(hash[i].ToString("x2"));
            }
            return sb.ToString();
        }

    }
}
*/