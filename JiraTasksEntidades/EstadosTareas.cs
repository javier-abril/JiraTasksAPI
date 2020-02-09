using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JiraTasksEntidades
{
    public class EstadosTareas
    {
        private int _id;
        private Tarea _tarea;
        /* 1-Pendiente 
         * 2-Cancelada
         * 3-Finalizada*/
        private Estado _estado;
        private string _descripcion;
        private DateTime _fecha;

        public int id
        {
            get { return _id; }
            set { _id = value; }
        }

        public Tarea tarea
        {
            get { return _tarea; }
            set { _tarea = value; }
        }

        public Estado estado
        {
            get { return _estado; }
            set { _estado = value; }
        }


        public string descripcion
        {
            get { return _descripcion; }
            set { _descripcion = value; }
        }

        public DateTime fecha
        {
            get { return _fecha; }
            set { _fecha = value; }
        }


    }
}
