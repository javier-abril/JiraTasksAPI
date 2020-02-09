using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JiraTasksEntidades;
using MySql.Data.MySqlClient;

namespace JiraTasksDatos
{
    public class MySQLManager
    {
        DataSet myDataSet = new DataSet();
        MySqlConnection mySqlConnection;
        private string connectionString = "server=localhost;user id=admin;password=admin;persistsecurityinfo=True;database=tasks";

        public MySQLManager()
        {
            RellenaDataset();
        }

        public MySQLManager(string connectionString)
        {
            this.connectionString = connectionString;
            RellenaDataset();
        }

        private void RellenaDataset()
        {
            mySqlConnection = new MySqlConnection(connectionString);

            mySqlConnection.Open();

            //Relleno usuarios
            String strSQL = "SELECT * FROM Usuarios;";

            MySqlDataAdapter myDataAdapter = new MySqlDataAdapter(strSQL, mySqlConnection);

            myDataAdapter.Fill(myDataSet, "Usuarios");

            //Relleno criticidad
            strSQL = "SELECT * FROM CriticidadTarea;";
            myDataAdapter = new MySqlDataAdapter(strSQL, mySqlConnection);
            myDataAdapter.Fill(myDataSet, "CriticidadTarea");

            //Relleno entornos
            strSQL = "SELECT * FROM Entornos;";
            myDataAdapter = new MySqlDataAdapter(strSQL, mySqlConnection);
            myDataAdapter.Fill(myDataSet, "Entornos");

            //Relleno estadostareas
            strSQL = "SELECT * FROM EstadosTareas;";
            myDataAdapter = new MySqlDataAdapter(strSQL, mySqlConnection);
            myDataAdapter.Fill(myDataSet, "EstadosTareas");

            //Relleno estadotarea
            strSQL = "SELECT * FROM EstadoTarea;";
            myDataAdapter = new MySqlDataAdapter(strSQL, mySqlConnection);
            myDataAdapter.Fill(myDataSet, "EstadoTarea");

            //Relleno estadousuario
            strSQL = "SELECT * FROM EstadoUsuario;";
            myDataAdapter = new MySqlDataAdapter(strSQL, mySqlConnection);
            myDataAdapter.Fill(myDataSet, "EstadoUsuario");

            //Relleno roles
            strSQL = "SELECT * FROM Roles;";
            myDataAdapter = new MySqlDataAdapter(strSQL, mySqlConnection);
            myDataAdapter.Fill(myDataSet, "Roles");

            //Relleno tareas
            strSQL = "SELECT * FROM Tareas;";
            myDataAdapter = new MySqlDataAdapter(strSQL, mySqlConnection);
            myDataAdapter.Fill(myDataSet, "Tareas");

            //Relleno tipotarea
            strSQL = "SELECT * FROM TipoTarea;";
            myDataAdapter = new MySqlDataAdapter(strSQL, mySqlConnection);
            myDataAdapter.Fill(myDataSet, "TipoTarea");

            mySqlConnection.Close();
        }

        public void RefrescaTareas()
        {
            myDataSet.Tables["Tareas"].Clear();
            myDataSet.Tables["EstadosTareas"].Clear();

            mySqlConnection.Open();

            //Relleno tareas
            String strSQL = "SELECT * FROM Tareas;";

            MySqlDataAdapter myDataAdapter = new MySqlDataAdapter(strSQL, mySqlConnection);

            myDataAdapter.Fill(myDataSet, "Tareas");

            //Relleno de estados de tareas
            strSQL = "SELECT * FROM EstadosTareas;";
            myDataAdapter = new MySqlDataAdapter(strSQL, mySqlConnection);
            myDataAdapter.Fill(myDataSet, "EstadosTareas");

            mySqlConnection.Close();
        }

        public IEnumerable<Usuario> GetUsuarios()
        {
            //return (IEnumerable<Usuario>) myDataSet.Tables[myDataSet.Tables.IndexOf("Usuarios")].AsEnumerable();
            List<Usuario> listaUsers = new List<Usuario>();

            foreach (DataRow data in myDataSet.Tables["Usuarios"].Rows)
            {
                Usuario user = new Usuario();
                user.idUsuario = (int)data["id"];
                user.nombre = (string)data["nombre"];
                user.usuario = (string)data["usuario"];
                user.password = (string)data["password"];
                user.rolUsuario = new Rol((int)data["rol"], "");
                user.estadoUsuario = new EstadoUsuario((int)data["estado"], "");

                listaUsers.Add(user);
            }

            return listaUsers;
        }

        public Usuario GetUsuario(int id)
        {
            return (Usuario)GetUsuarios().Where(u => u.idUsuario == id).First();
        }

        public IEnumerable<Tarea> GetTareas()
        {
            List<Tarea> listaTareas = new List<Tarea>();

            DataRowCollection drc = myDataSet.Tables["Tareas"].Rows;

            foreach (DataRow data in drc)
            {
                Tarea tar = new Tarea();
                tar.idTarea = (int)data["id"];
                tar.usuario = GetUsuario((int)data["idusuario"]);
                tar.descripcion = (string)data["descripcion"];
                tar.urlJira = (string)data["urljira"];
                tar.tareaPadre = new Tarea();

                if (data["idtareapadre"].ToString() != "")
                    tar.tareaPadre.idTarea = (int)data["idtareapadre"];

                tar.fecha = (string)data["fecha"];
                tar.criticidad = GetCriticidadTarea((int)data["criticidad"]);
                tar.entorno = GetEntorno((int)data["entorno"]);
                tar.tipo = GetTipoTarea((int)data["tipo"]);
                tar.nombreUsuario = tar.usuario.nombre;
                tar.estados = GetEstadosTareas(tar.idTarea).ToList();
                tar.estadoActual = (EstadoTarea)tar.estados.Last().estado;

                listaTareas.Add(tar);
            }

            return listaTareas;
        }

        public Tarea GetTarea(int idTarea)
        {
            DataRow data = (DataRow)(myDataSet.Tables["Tareas"].Select("id = " + idTarea)).First();

            Tarea tar = new Tarea();
            tar.idTarea = (int)data["id"];
            tar.usuario = GetUsuario((int)data["idusuario"]);
            tar.descripcion = (string)data["descripcion"];
            tar.urlJira = (string)data["urljira"];
            tar.tareaPadre = new Tarea();

            if (data["idtareapadre"].ToString() != "")
                tar.tareaPadre.idTarea = (int)data["idtareapadre"];

            tar.fecha = (string)data["fecha"];
            tar.criticidad = GetCriticidadTarea((int)data["criticidad"]);
            tar.entorno = GetEntorno((int)data["entorno"]);
            tar.tipo = GetTipoTarea((int)data["tipo"]);
            tar.nombreUsuario = tar.usuario.nombre;
            tar.estados = GetEstadosTareas(tar.idTarea).ToList();
            tar.estadoActual = (EstadoTarea)tar.estados.Last().estado;

            return tar;
        }

        public CriticidadTarea GetCriticidadTarea(int id)
        {
            //Cogemos una fila con select y LINQ
            DataRow data = (DataRow)(myDataSet.Tables["CriticidadTarea"].Select("id = " + id)).First();

            CriticidadTarea criticidad = new CriticidadTarea((int)data["id"], (string)data["nombre"]);

            return criticidad;

        }

        public IEnumerable<CriticidadTarea> GetCriticidadesTarea()
        {
            List<CriticidadTarea> retorno = new List<CriticidadTarea>();

            foreach (DataRow data in myDataSet.Tables["CriticidadTarea"].Select())
            {
                CriticidadTarea criticidad = new CriticidadTarea((int)data["id"], (string)data["nombre"]);
                retorno.Add(criticidad);
            }

            return retorno;

        }

        public Entorno GetEntorno(int id)
        {
            //Cogemos una fila con select y LINQ
            DataRow data = (DataRow)(myDataSet.Tables["Entornos"].Select("id = " + id)).First();

            Entorno ent = new Entorno((int)data["id"], (string)data["nombre"]);

            return ent;

        }

        public IEnumerable<Entorno> GetEntornos()
        {
            List<Entorno> retorno = new List<Entorno>();
            //Cogemos una fila con select y LINQ
            foreach (DataRow data in myDataSet.Tables["Entornos"].Select())
            {
                Entorno ent = new Entorno((int)data["id"], (string)data["nombre"]);
                retorno.Add(ent);
            }

            return retorno;

        }

        public EstadoTarea GetEstadoTarea(int id)
        {
            //Cogemos una fila con select y LINQ
            DataRow data = (DataRow)(myDataSet.Tables["EstadoTarea"].Select("id=" + id)).First();

            EstadoTarea est = new EstadoTarea((int)data["id"], (string)data["nombre"]);

            return est;
        }

        public IEnumerable<EstadosTareas> GetEstadosTareas(int idTarea)
        {
            List<EstadosTareas> estadosTareas = new List<EstadosTareas>();

            //Tarea tarTemp= GetTarea(idTarea); Esto entra en bucle ya que llama infinitamente a gettarea

            Tarea tarTemp = new Tarea();

            tarTemp.idTarea = idTarea;

            foreach (DataRow data in myDataSet.Tables["EstadosTareas"].Select("idtarea=" + idTarea))
            {
                EstadosTareas est = new EstadosTareas();
                est.id = (int)data["id"];
                est.tarea = tarTemp;
                est.estado = GetEstadoTarea((int)data["idestadotarea"]);
                est.descripcion = (string)data["descripcion"];
                est.fecha = Convert.ToDateTime((string)data["fecha"]);

                estadosTareas.Add(est);
            }

            return estadosTareas;
        }

        public EstadoUsuario GetEstadoUsuario(int id)
        {
            //Cogemos una fila con select y LINQ
            DataRow data = (DataRow)(myDataSet.Tables["EstadoUsuario"].Select("id=" + id)).First();

            EstadoUsuario est = new EstadoUsuario((int)data["id"], (string)data["nombre"]);

            return est;
        }

        public Rol GetRol(int id)
        {
            //Cogemos una fila con select y LINQ
            DataRow data = (DataRow)(myDataSet.Tables["Roles"].Select("id=" + id)).First();

            Rol r = new Rol((int)data["id"], (string)data["nombre"]);

            return r;
        }

        public TipoTarea GetTipoTarea(int id)
        {
            //Cogemos una fila con select y LINQ
            DataRow data = (DataRow)(myDataSet.Tables["TipoTarea"].Select("id=" + id)).First();

            TipoTarea tipo = new TipoTarea((int)data["id"], (string)data["nombre"]);

            return tipo;
        }

        public IEnumerable<TipoTarea> GetTiposTarea()
        {
            List<TipoTarea> retorno = new List<TipoTarea>();

            foreach (DataRow data in myDataSet.Tables["TipoTarea"].Select())
            {
                TipoTarea tipo = new TipoTarea((int)data["id"], (string)data["nombre"]);
                retorno.Add(tipo);
            }
            return retorno;
        }

        public long CrearTarea(Tarea tar)
        {
            mySqlConnection.Open();
            MySqlCommand comm = mySqlConnection.CreateCommand();

            comm.CommandText = "INSERT INTO Tareas(idusuario, descripcion, urljira, fecha, criticidad, entorno, tipo) VALUES(@idusuario, @descripcion, @urljira, @fecha, @criticidad, @entorno, @tipo)";
            comm.Parameters.AddWithValue("@idusuario", tar.usuario.idUsuario);
            comm.Parameters.AddWithValue("@descripcion", tar.descripcion);
            comm.Parameters.AddWithValue("@urljira", "");
            comm.Parameters.AddWithValue("@fecha", tar.fecha);
            comm.Parameters.AddWithValue("@criticidad", tar.criticidad.id);
            comm.Parameters.AddWithValue("@entorno", tar.entorno.id);
            comm.Parameters.AddWithValue("@tipo", tar.tipo.id);
            comm.ExecuteNonQuery();
            long id = comm.LastInsertedId;

            //Insert de estados
            comm = mySqlConnection.CreateCommand();

            comm.CommandText = "INSERT INTO EstadosTareas(idtarea, idestadotarea, descripcion, fecha) VALUES(@idtarea, @idestadotarea, @descripcion, @fecha)";
            comm.Parameters.AddWithValue("@idtarea", id);
            comm.Parameters.AddWithValue("@idestadotarea", EstadoTarea.Pendiente);
            comm.Parameters.AddWithValue("@descripcion", "Creación de tarea");
            comm.Parameters.AddWithValue("@fecha", tar.fecha);
            comm.ExecuteNonQuery();

            mySqlConnection.Close();

            return id;
        }

        public void CancelarTarea(Tarea tar, string descripcion)
        {
            mySqlConnection.Open();
            MySqlCommand comm = mySqlConnection.CreateCommand();

            comm = mySqlConnection.CreateCommand();

            comm.CommandText = "INSERT INTO EstadosTareas(idtarea, idestadotarea, descripcion, fecha) VALUES(@idtarea, @idestadotarea, @descripcion, @fecha)";
            comm.Parameters.AddWithValue("@idtarea", tar.idTarea);
            comm.Parameters.AddWithValue("@idestadotarea", EstadoTarea.Cancelada);
            comm.Parameters.AddWithValue("@descripcion", descripcion);
            comm.Parameters.AddWithValue("@fecha", tar.fecha);
            comm.ExecuteNonQuery();

            mySqlConnection.Close();
        }

        public void FinalizarTarea(Tarea tar, string descripcion)
        {
            mySqlConnection.Open();
            MySqlCommand comm = mySqlConnection.CreateCommand();

            comm = mySqlConnection.CreateCommand();

            comm.CommandText = "INSERT INTO EstadosTareas(idtarea, idestadotarea, descripcion, fecha) VALUES(@idtarea, @idestadotarea, @descripcion, @fecha)";
            comm.Parameters.AddWithValue("@idtarea", tar.idTarea);
            comm.Parameters.AddWithValue("@idestadotarea", EstadoTarea.Finalizada);
            comm.Parameters.AddWithValue("@descripcion", descripcion);
            comm.Parameters.AddWithValue("@fecha", tar.fecha);
            comm.ExecuteNonQuery();

            comm = mySqlConnection.CreateCommand();

            comm.CommandText = "Update Tareas set urljira = @urljira  where id = @idtarea";
            comm.Parameters.AddWithValue("@urljira", tar.urlJira);
            comm.Parameters.AddWithValue("@idtarea", tar.idTarea);
            comm.ExecuteNonQuery();

            mySqlConnection.Close();
        }
    }
}
