using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccesoDatos.Models
{
    public class Departamento
    {
        //ESTA CLASE TENDRA TODAS LAS COLUMNAS DE LA TABLA
        //CON SUS TIPOS DE DATO
        public int IdDepartamento { get; set; }
        public string Nombre { get; set; }
        public string Localidad { get; set; }
    }
}
