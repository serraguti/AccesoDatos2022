using System.Data.SqlClient;

namespace AccesoDatos
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //LeerRegistrosDept();
            //InsertarDatosDept();
            //EliminarDeptParameters();
            //LeerRegistrosDept();
            MostrarEmpleadosDepartamento();
        }

        static void MostrarEmpleadosDepartamento()
        {
            string connectionString = @"Data Source=LOCALHOST;Initial Catalog=HOSPITAL;Persist Security Info=True;User ID=SA";
            SqlConnection cn = new SqlConnection(connectionString);
            SqlCommand com = new SqlCommand();
            SqlDataReader reader;
            string sql = "select APELLIDO, OFICIO, SALARIO from EMP where DEPT_NO=@DEPTNO";
            Console.WriteLine("Buscador de empleados");
            Console.WriteLine("Introduzca ID departamento a buscar");
            int idDepartamento = int.Parse(Console.ReadLine());
            SqlParameter pamid = new SqlParameter("@DEPTNO", idDepartamento);
            com.Parameters.Add(pamid);
            com.Connection = cn;
            com.CommandType = System.Data.CommandType.Text;
            com.CommandText = sql;
            cn.Open();
            reader = com.ExecuteReader();
            while (reader.Read())
            {
                string apellido = reader["APELLIDO"].ToString();
                string oficio = reader["OFICIO"].ToString();
                string salario = reader["SALARIO"].ToString();
                Console.WriteLine(apellido + " - " + oficio + " - " + salario);
            }
            reader.Close();
            cn.Close();
            com.Parameters.Clear();
            Console.WriteLine("Fin de programa");
        }

        static void EliminarDeptParameters()
        {
            string connectionString = @"Data Source=LOCALHOST;Initial Catalog=HOSPITAL;Persist Security Info=True;User ID=SA";
            //DECLARAMOS LOS OBJETOS DE ACCESO A DATOS
            SqlConnection cn = new SqlConnection(connectionString);
            SqlCommand com = new SqlCommand();
            //PEDIMOS AL USUARIO EL VALOR DEL NUMERO DEPARTAMENTO A ELIMINAR
            Console.WriteLine("Introduzca ID Departamento a eliminar:");
            int numero = int.Parse(Console.ReadLine());
            //ESCRIBIR LA CONSULTA CON EL PARAMETRO
            string sql = "delete from DEPT where DEPT_NO=@IDDEPARTAMENTO";
            //CREAMOS UN NUEVO PARAMETRO LLAMADO @IDDEPARTAMENTO
            SqlParameter pamid = new SqlParameter("@IDDEPARTAMENTO", numero);
            //AÑADIMOS AL COMANDO EL NUEVO PARAMETRO
            com.Parameters.Add(pamid);
            //CONFIGURAMOS EL COMANDO NORMALMENTE
            com.Connection = cn;
            com.CommandType = System.Data.CommandType.Text;
            com.CommandText = sql;
            //ENTRAR Y SALIR
            cn.Open();
            int eliminados = com.ExecuteNonQuery();
            cn.Close();
            //CUANDO TENEMOS PARAMETROS, SIEMPRE LOS LIMPIAMOS
            com.Parameters.Clear();
            Console.WriteLine("Departamentos eliminados: " + eliminados);
        }

        static void InsertarDatosDept()
        {
            //NECESITAMOS NUESTRA CADENA DE CONEXION
            string connectionString = @"Data Source=LOCALHOST;Initial Catalog=HOSPITAL;Persist Security Info=True;User ID=SA";
            //DECLARAMOS NUESTROS OBJETOS DE ACCESO A DATOS
            //EXCEPTO DataReader PORQUE NO VAMOS A LEER NINGUN REGISTRO
            SqlConnection cn = new SqlConnection(connectionString);
            SqlCommand com = new SqlCommand();
            Console.WriteLine("Número de departamento");
            string num = Console.ReadLine();
            //REALIZAMOS NUESTRA CONSULTA
            string sql = "insert into DEPT values (" + num + ", 'INFORMATICA', 'OVIEDO')";
            //CONFIGURAMOS NUESTRO COMANDO
            //INDICAMOS CONEXION
            com.Connection = cn;
            //TIPO DE CONSULTA QUE VAMOS A REALIZAR
            com.CommandType = System.Data.CommandType.Text;
            //LA CONSULTA SQL
            com.CommandText = sql;
            //ENTRAMOS Y SALIMOS
            cn.Open();
            //EJECUTAMOS LA CONSULTA CON EL METODO ExecuteNonQuery()
            //QUE NOS DEVUELVE UN ENTERO CON EL NUMERO DE REGISTROS AFECTADOS
            int insertados = com.ExecuteNonQuery();
            //SALIMOS
            cn.Close();
            Console.WriteLine("Registros insertados: " + insertados);
        }

        static void LeerRegistrosDept()
        {
            //DECLARAMOS NUESTRA CADENA DE CONEXION A SQL SERVER
            //SI LA CADENA TIENE CARACTERES ESPECIALES, COMO POR EJEMPLO
            //LA CONTRABARRA, DEBEMOS INCLUIR UNA @ FUERA DE LA CADENA
            //PARA QUE RECUPERE LOS LITERALES
            //SI LA CADENA TIENE PASSWORD, DEBEMOS ESCRIBIR EL PASSWORD
            //DE FORMA MANUAL
            string connectionString = @"Data Source=LOCALHOST;Initial Catalog=HOSPITAL;Persist Security Info=True;User ID=SA";
            //DECLARAMOS LOS OBJETOS A UTILIZAR
            //EL OBJETO CONNECTION SIEMPRE DEBE LLEVAR LA CADENA
            //DE CONEXION EN SU CONSTRUCTOR
            SqlConnection cn = new SqlConnection(connectionString);
            //INSTANCIAMOS EL COMANDO, QUE ES EL ENCARGADO DE LAS CONSULTAS
            SqlCommand com = new SqlCommand();
            //COMO VAMOS A LEER DATOS, DECLARAMOS UN CURSOR
            //UN CURSOR NUNCA SE INSTANCIA, SOLAMENTE PODEMOS CREARLO
            //A PARTIR DE NUESTRA CONSULTA DE SELECCION Y UN COMANDO
            SqlDataReader lector;
            //DECLARAMOS NUESTRA CONSULTA
            string sql = "select * from DEPT";
            //CONFIGURAMOS NUESTRO COMMAND
            //INDICAMOS AL COMANDO LA CONEXION A UTILIZAR
            com.Connection = cn;
            //DEBEMOS INDICAR EL TIPO DE CONSULTA
            com.CommandType = System.Data.CommandType.Text;
            //INDICAMOS LA PROPIA CONSULTA
            com.CommandText = sql;
            //UNA VEZ FINALIZADA LA CONFIGURACION, DEBEMOS EJECUTAR LA 
            //CONSULTA, PARA ELLO, ABRIMOS LA CONEXION
            //SIEMPRE SERA ENTRAR Y SALIR
            cn.Open();
            //EJECUTAMOS LA CONSULTA MEDIANTE EL COMANDO.
            //AL SER UNA CONSULTA DE SELECCION, SE UTILIZA EL METODO
            //ExecuteReader() QUE NOS DEVUELVE UN DataReader (cursor/lector)
            lector = com.ExecuteReader();
            //PARA LEER EL READER CONTIENE UN METODO Read()
            //QUE LEE UNA FILA Y DEVUELVE true/false SI HA PODIDO LEER
            //CADA VEZ QUE EJECUTAMOS Read() SE MUEVE UNA FILA
            //SOLAMENTE PODEMOS IR HACIA ADELANTE
            //lector.Read();
            //PARA LEER LOS DATOS DE UNA COLUMNA SE UTILIZA
            // lector["COLUMNA"], TAMBIEN PODEMOS UTILIZAR lector[indice]
            //RECUPERAMOS EL NOMBRE
            //string nombre = lector["DNOMBRE"].ToString();
            //Console.WriteLine(nombre);
            //READ() ES BOOLEAN, POR LO QUE SI DESEAMOS RECORRER TODOS
            //LOS REGISTROS, DEBEMOS UTILIZAR UN BUCLE CONDICIONAL
            //LEEMOS TODOS LOS REGISTROS
            while (lector.Read())
            {
                //AQUI IREMOS RECUPERANDO COLUMNA A COLUMNA CADA DATO
                //DE CADA FILA
                string nombre = lector["DNOMBRE"].ToString();
                string localidad = lector["LOC"].ToString();
                Console.WriteLine(nombre + " - " + localidad);
            }
            //UNA VEZ QUE HEMOS LEIDO, SIEMPRE SE CIERRA EL CURSOR
            //Y LA CONEXION
            lector.Close();
            cn.Close();
            Console.WriteLine("Fin de programa");
        }
    }
}