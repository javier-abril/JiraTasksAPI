using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JiraTasksEntidades
{
    public class EstadoUsuario : Estado
    {
        public EstadoUsuario(int id, string nombre) : base(id, nombre)
        {

        }
    }
}
