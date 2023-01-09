using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using AccesoDatos.Models;

namespace AccesoDatos.Repositories
{
    public class RepositoryDepartamentos
    {
        //AQUI DECLARAMOS LOS OBJETOS QUE VAMOS A UTILIZAR
        string connectionString;
        SqlConnection cn;
        SqlCommand com;
        SqlDataReader reader;

        //ESTOS OBJETOS DEBEMOS INSTANCIARLOS/CREARLOS
        //PARA ELLO, SE UTILIZA EL CONSTRUCTOR
        public RepositoryDepartamentos()
        {
            this.connectionString = @"Data Source=LOCALHOST;Initial Catalog=HOSPITAL;Persist Security Info=True;User ID=SA";
            this.cn = new SqlConnection(this.connectionString);
            this.com = new SqlCommand();
            //SOLAMENTE LE DIREMOS, AL COMANDO, UNA VEZ SU CONEXION Y SU TIPO DE CONSULTA
            this.com.Connection = this.cn;
            this.com.CommandType = System.Data.CommandType.Text;
        }

        //CREAMOS NUESTRO METODOS PARA LAS CONSULTAS NECESARIAS
        //COMENZAMOS POR INSERTAR UN DEPARTAMENTO
        public int InsertarDepartamento(int id, string nombre, string localidad)
        {
            SqlParameter pamid = new SqlParameter("@ID", id);
            SqlParameter pamnombre = new SqlParameter("@NOMBRE", nombre);
            SqlParameter pamlocalidad = new SqlParameter("@LOCALIDAD", localidad);
            string sql = "INSERT INTO DEPT VALUES (@ID, @NOMBRE, @LOCALIDAD)";
            this.com.Parameters.Add(pamid);
            this.com.Parameters.Add(pamnombre);
            this.com.Parameters.Add(pamlocalidad);
            this.com.CommandText = sql;
            this.cn.Open();
            int insertados = this.com.ExecuteNonQuery();
            this.cn.Close();
            this.com.Parameters.Clear();
            return insertados;
        }

        public int DeleteDepartamento(int id)
        {
            SqlParameter pamid = new SqlParameter("@ID", id);
            string sql = "DELETE FROM DEPT WHERE DEPT_NO=@ID";
            this.com.Parameters.Add(pamid);
            this.com.CommandText = sql;
            this.cn.Open();
            int eliminados = this.com.ExecuteNonQuery();
            this.cn.Close();
            this.com.Parameters.Clear();
            return eliminados;
        }

        public int UpdateDepartamento(int id, string nombre, string localidad)
        {
            string sql = "UPDATE DEPT SET DNOMBRE=@NOMBRE, LOC=@LOCALIDAD WHERE DEPT_NO=@ID";
            SqlParameter pamid = new SqlParameter("@ID", id);
            SqlParameter pamnombre = new SqlParameter("@NOMBRE", nombre);
            SqlParameter pamlocalidad = new SqlParameter("@LOCALIDAD", localidad);
            this.com.Parameters.Add(pamid);
            this.com.Parameters.Add(pamnombre);
            this.com.Parameters.Add(pamlocalidad);
            this.com.CommandText = sql;
            this.cn.Open();
            int modificados = this.com.ExecuteNonQuery();
            this.cn.Close();
            this.com.Parameters.Clear();
            return modificados;
        }

        //METODO PARA DEVOLVER TODOS LOS DEPARTAMENTOS
        public List<Departamento> GetDepartamentos()
        {
            string sql = "SELECT * FROM DEPT";
            this.com.CommandText = sql;
            this.cn.Open();
            this.reader = this.com.ExecuteReader();
            //CREAMOS LA COLECCION PARA DEVOLVER LOS DATOS
            List<Departamento> lista = new List<Departamento>();
            while (this.reader.Read())
            {
                //POR CADA VUELTA DE BUCLE, CREAMOS UN OBJETO 
                //DEPARTAMENTO
                Departamento dept = new Departamento();
                //ASIGNAMOS SUS PROPIEDADES DESDE EL reader
                dept.IdDepartamento = int.Parse(this.reader["DEPT_NO"].ToString());
                dept.Nombre = this.reader["DNOMBRE"].ToString();
                dept.Localidad = this.reader["LOC"].ToString();
                //AGREGAMOS CADA DEPARTAMENTO A LA COLECCION
                lista.Add(dept);
            }
            this.reader.Close();
            this.cn.Close();
            return lista;
        }
    }
}
