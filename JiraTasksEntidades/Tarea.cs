using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JiraTasksEntidades
{

    public class Tarea
    {
        private int _idTarea;
        private Usuario _usuario;
        private string _descripcion;
        private Tarea _tareaPadre;
        private EstadoTarea _estadoActual;
        private string _descripcionEstado;
        private string _nombreUsuario;
        private string _urlJira;
        private string _fecha;
        private CriticidadTarea _criticidad;
        private Entorno _entorno;
        private TipoTarea _tipo;
        private List<EstadosTareas> _estados;


        public int idTarea
        {
            get { return _idTarea; }
            set { _idTarea = value; }
        }


        public Usuario usuario
        {
            get { return _usuario; }
            set { _usuario = value; }
        }


        public string descripcion
        {
            get { return _descripcion; }
            set { _descripcion = value; }
        }


        public string urlJira
        {
            get { return _urlJira; }
            set { _urlJira = value; }
        }


        public Tarea tareaPadre
        {
            get { return _tareaPadre; }
            set { _tareaPadre = value; }
        }


        public CriticidadTarea criticidad
        {
            get { return _criticidad; }
            set { _criticidad = value; }
        }



        public Entorno entorno
        {
            get { return _entorno; }
            set { _entorno = value; }
        }


        public TipoTarea tipo
        {
            get { return _tipo; }
            set { _tipo = value; }
        }

        public EstadoTarea estadoActual
        {
            get { return _estadoActual; }
            set { _estadoActual = value; }
        }

        public List<EstadosTareas> estados
        {
            get { return _estados; }
            set { _estados = value; }
        }


        //Usado para pintar la descripcion de los estados(anotaciones) 
        //para pintar en el datagrid
        public string descripcionEstado
        {
            get { return _descripcionEstado; }
            set { _descripcionEstado = value; }
        }

        //Para pintar el nombre en el datagrid
        public string nombreUsuario
        {
            get { return _nombreUsuario; }
            set { _nombreUsuario = value; }
        }

        //Para pintar fecha en el datagrid
        public string fecha
        {
            get { return _fecha; }
            set { _fecha = value; }
        }
    }
}
