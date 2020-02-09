using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JiraTasksEntidades
{

    public class Rol
    {
        public int id;
        private int _idRol;
        private string _nombre;

        public Rol(int idRol, string nombre)
        {
            this._idRol = idRol;
            this._nombre = nombre;
        }


        public int idRol
        {
            get { return _idRol; }
            set { _idRol = value; }
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
