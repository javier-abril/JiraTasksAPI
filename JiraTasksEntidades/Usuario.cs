using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace JiraTasksEntidades
{
    public class Usuario
    {
        private int _idUsuario;
        private string _usuario;
        private string _password;
        private string _email;
        private string _nombre;
        private string _apellidos;
        private Rol _rolUsuario;
        private EstadoUsuario _estadoUsuario;

        public Usuario()
        {
        }

        public Usuario(int idUsuario)
        {
            this._idUsuario = idUsuario;
        }

        public int idUsuario
        {
            get { return _idUsuario; }
            set { _idUsuario = value; }
        }

        public string usuario
        {
            get { return _usuario; }
            set { _usuario = value; }
        }

        public string password
        {
            get { return _password; }
            set { _password = value; }
        }

        public string email
        {
            get { return _email; }
            set { _email = value; }
        }

        public string nombre
        {
            get { return _nombre; }
            set { _nombre = value; }
        }

        public string apellidos
        {
            get { return _apellidos; }
            set { _apellidos = value; }
        }

        public Rol rolUsuario
        {
            get { return _rolUsuario; }
            set { _rolUsuario = value; }
        }


        public EstadoUsuario estadoUsuario
        {
            get { return _estadoUsuario; }
            set { _estadoUsuario = value; }
        }

    }
}
