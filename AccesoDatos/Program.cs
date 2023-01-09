using System.Data.SqlClient;
using AccesoDatos.Models;
using AccesoDatos.Repositories;

namespace AccesoDatos
{
    internal class Program
    {
        //CREAMOS LA CLASE REPOSITORY
        //LO HACEMOS A NIVEL DE CLASE PARA UTILIZAR EL OBJETO EN 
        //DISTINTOS METODOS, COMO POR EJEMPLO, INSERTAR O ELIMINAR
        static RepositoryDepartamentos repo = new RepositoryDepartamentos();

        static void Main(string[] args)
        {
            //LeerRegistrosDept();
            //InsertarDatosDept();
            //EliminarDeptParameters();
            //LeerRegistrosDept();
            //MostrarEmpleadosDepartamento();
            //ModificarSalas();
            //MostrarEnfermos();
            //EliminarEnfermo();
            //MostrarEnfermos();
            //InsertDepartamentoRepo();
            //UpdateDepartamento();
            //DeleteDepartamento();
            //MostrarDepartamentos();
            AppCrudDepartamentos();
        }

        static void AppCrudDepartamentos()
        {
            int opcion = -1;
            while (opcion != 4)
            {
                Console.WriteLine("------CRUD DEPARTAMENTOS------");
                MostrarDepartamentos();
                Console.WriteLine("---------------------");
                Console.WriteLine("1.- Insertar Departamento");
                Console.WriteLine("2.- Modificar departamento");
                Console.WriteLine("3.- Eliminar departamento");
                Console.WriteLine("4.- Salir de App");
                Console.WriteLine("Seleccione una opción");
                opcion = int.Parse(Console.ReadLine());
                if (opcion == 1)
                {
                    InsertDepartamentoRepo();
                }else if (opcion == 2)
                {
                    UpdateDepartamento();
                }else if (opcion == 3)
                {
                    DeleteDepartamento();
                }else if (opcion == 4)
                {
                    Console.WriteLine("Cerrando Aplicación");
                }
                else
                {
                    Console.WriteLine("Opción incorrecta");
                }
            }
        }

        static void MostrarDepartamentos()
        {
            List<Departamento> departamentos = repo.GetDepartamentos();
            foreach (Departamento dept in departamentos)
            {
                Console.WriteLine(dept.IdDepartamento + " - " + dept.Nombre + " - " + dept.Localidad);
            }
        }

        static void UpdateDepartamento()
        {
            Console.WriteLine("Introduzca ID de departamento a modificar");
            int iddept = int.Parse(Console.ReadLine());
            Console.WriteLine("Introduzca nuevo NOMBRE de departamento");
            string nombre = Console.ReadLine();
            Console.WriteLine("Introduzca nueva LOCALIDAD");
            string localidad = Console.ReadLine();
            int modificados = repo.UpdateDepartamento(iddept, nombre, localidad);
            Console.WriteLine("Departamentos modificados: " + modificados);
        }

        static void DeleteDepartamento()
        {
            Console.WriteLine("Introduzca ID de departamento para eliminar");
            int iddept = int.Parse(Console.ReadLine());
            int eliminados = repo.DeleteDepartamento(iddept);
            Console.WriteLine("Departamentos eliminados: " + eliminados);
        }

        static void InsertDepartamentoRepo()
        {
            Console.WriteLine("Introduzca ID departamento");
            int iddept = int.Parse(Console.ReadLine());
            Console.WriteLine("Introduzca NOMBRE departamento");
            string nombre = Console.ReadLine();
            Console.WriteLine("Introduzca LOCALIDAD");
            string localidad = Console.ReadLine();
            int insertados = repo.InsertarDepartamento(iddept, nombre, localidad);
            Console.WriteLine("Departamentos insertados: " + insertados);
        }

        static void EliminarEnfermo()
        {
            string connectionString = @"Data Source=LOCALHOST;Initial Catalog=HOSPITAL;Persist Security Info=True;User ID=SA";
            SqlConnection cn = new SqlConnection(connectionString);
            SqlCommand com = new SqlCommand();
            Console.WriteLine("Introduzca INSCRIPCION para eliminar enfermo");
            int inscripcion = int.Parse(Console.ReadLine());
            string sql = "DELETE FROM ENFERMO WHERE INSCRIPCION=@INSCRIPCION";
            SqlParameter paminscripcion = new SqlParameter("@INSCRIPCION", inscripcion);
            com.Parameters.Add(paminscripcion);
            com.Connection = cn;
            com.CommandType = System.Data.CommandType.Text;
            com.CommandText = sql;
            cn.Open();
            int afectados = com.ExecuteNonQuery();
            cn.Close();
            com.Parameters.Clear();
            Console.WriteLine("Enfermos eliminados: " + afectados);
        }

        static void MostrarEnfermos()
        {
            string connectionString = @"Data Source=LOCALHOST;Initial Catalog=HOSPITAL;Persist Security Info=True;User ID=SA";
            SqlConnection cn = new SqlConnection(connectionString);
            SqlCommand com = new SqlCommand();
            SqlDataReader reader;
            string sql = "SELECT INSCRIPCION, APELLIDO, DIRECCION FROM ENFERMO";
            com.Connection = cn;
            com.CommandType = System.Data.CommandType.Text;
            com.CommandText = sql;
            cn.Open();
            reader = com.ExecuteReader();
            while (reader.Read())
            {
                string inscripcion = reader["INSCRIPCION"].ToString();
                string apellido = reader["APELLIDO"].ToString();
                string direccion = reader["DIRECCION"].ToString();
                Console.WriteLine(inscripcion + " - " + apellido + ", Dirección: " + direccion);
            }
            reader.Close();
            cn.Close();
        }


        static void ModificarSalas()
        {
            string connectionString = @"Data Source=LOCALHOST;Initial Catalog=HOSPITAL;Persist Security Info=True;User ID=SA";
            SqlConnection cn = new SqlConnection(connectionString);
            SqlCommand com = new SqlCommand();
            SqlDataReader reader;
            string sql = "select DISTINCT sala_cod, nombre from sala";
            com.Connection = cn;
            com.CommandType = System.Data.CommandType.Text;
            com.CommandText = sql;
            cn.Open();
            reader = com.ExecuteReader();
            while (reader.Read())
            {
                string salacod = reader["SALA_COD"].ToString();
                string nombre = reader["NOMBRE"].ToString();
                Console.WriteLine(salacod + " - " + nombre);
            }
            reader.Close();
            //NO CERRAMOS LA CONEXION PORQUE SEGUIMOS HACIENDO CONSULTAS
            Console.WriteLine("Seleccione un ID de sala para modificar");
            int idsala = int.Parse(Console.ReadLine());
            Console.WriteLine("Introduzca el nuevo nombre de sala");
            string newname = Console.ReadLine();
            sql = "UPDATE SALA SET NOMBRE=@NEWNAME WHERE SALA_COD=@SALACOD";
            SqlParameter pamid = new SqlParameter("@SALACOD", idsala);
            SqlParameter pamnombre = new SqlParameter("@NEWNAME", newname);
            com.Parameters.Add(pamnombre);
            com.Parameters.Add(pamid);
            com.CommandText = sql;
            //LA CONEXION ESTA ABIERTA, POR LO QUE SIMPLEMENTE VAMOS A EJECUTAR
            //LA CONSULTA
            int afectados = com.ExecuteNonQuery();
            cn.Close();
            com.Parameters.Clear();
            Console.WriteLine("Registros modificados: " + afectados);
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