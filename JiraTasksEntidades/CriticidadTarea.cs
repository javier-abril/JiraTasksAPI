using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JiraTasksEntidades
{
    public class CriticidadTarea
    {
        private int _id;
        private string _nombre;

        public CriticidadTarea(int id, string nombre)
        {
            this._id = id;
            this._nombre = nombre;
        }

        public int id
        {
            get { return _id; }
            set { _id = value; }
        }

        public string nombre
        {
            get { return _nombre; }
            set { _nombre = value; }
        }

        public override string ToString()
        {
            return nombre;
        }

    }
}
