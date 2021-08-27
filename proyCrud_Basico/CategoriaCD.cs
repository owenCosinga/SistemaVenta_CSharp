using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using CapaEntidad;

namespace CapaDatos
{
   public class CategoriaCD
    {

        public DataTable listar()
        {
            //proporciona leer una secuencia de filas en una base de datos
            //instanciamos un objeto dataReader, solo se usa para tipo datatable por ejem listar-buscar          
            SqlDataReader Resultado;
            //instnaciamos un objeto dataTable     //representa una tabla en la memoria
            DataTable Tabla = new DataTable();
            //instanciamos un objeto para la coneccion
            SqlConnection SqlCon = new SqlConnection();
            try
            {
                //asignamos la clase conexion en la variable, seguido llamos al metodo getinstancia(porque el constructor es privado),
                //por ultimo llamamos al metodo crear conexion para enlazar la conexion y devolver la variable sqlconection
                SqlCon = Conexion.getInstancia().CrearConexion();
                //sql comand representa a una instruccion transact sql o a un SP store_procedure
                SqlCommand Comando = new SqlCommand("categoria_listar", SqlCon);
                //hacemos referencia a la instruccion SP
                Comando.CommandType = CommandType.StoredProcedure;
                //abrimos la conexion
                SqlCon.Open();
                //ejecutamos el comando
                Resultado = Comando.ExecuteReader();
                //rellenamos el  datatable con su metodo load y con el valor obtenido del resultado
                Tabla.Load(Resultado);
                return Tabla;
            }
            catch (Exception ex)
            {
                //nos muestra la excepcion
                throw ex;
            }
            finally
            {
                //si la conexion esta abierta se cierra la conexion
                if (SqlCon.State == ConnectionState.Open) SqlCon.Close();
            }
        }

        public DataTable buscar(string valor)
        {
            //proporciona leer una secuencia de filas en una base de datos
            //instanciamos un objeto dataReader, solo se usa para tipo datatable por ejem listar-buscar          
            SqlDataReader Resultado;
            //instnaciamos un objeto dataTable     //representa una tabla en la memoria
            DataTable Tabla = new DataTable();
            //instanciamos un objeto para la coneccion
            SqlConnection SqlCon = new SqlConnection();
            try
            {
                //asignamos la clase conexion en la variable, seguido llamos al metodo getinstancia(porque el constructor es privado),
                //por ultimo llamamos al metodo crear conexion para enlazar la conexion y devolver la variable sqlconection
                SqlCon = Conexion.getInstancia().CrearConexion();
                //sql comand representa a una instruccion transact sql o a un SP store_procedure
                SqlCommand Comando = new SqlCommand("categoria_buscar", SqlCon);
                //asignamos el valor para el envio del parametro
                Comando.Parameters.Add("@valor", SqlDbType.VarChar).Value = valor;
                //hacemos referencia a la instruccion SP
                Comando.CommandType = CommandType.StoredProcedure;
                //abrimos la conexion
                SqlCon.Open();
                //ejecutamos el comando
                Resultado = Comando.ExecuteReader();
                //rellenamos el  datatable con su metodo load y con el valor obtenido del resultado
                Tabla.Load(Resultado);
                return Tabla;
            }
            catch (Exception ex)
            {
                //nos muestra la excepcion
                throw ex;
            }
            finally
            {
                //si la conexion esta abierta se cierra la conexion
                if (SqlCon.State == ConnectionState.Open) SqlCon.Close();
            }
        }

        public string Existe(string Valor)
        {
            string Rpta = "";
            //instanciamos un objeto para la conexion
            SqlConnection SqlCon = new SqlConnection();
            try
            {
                //asignamos la clase conexion en la variable, seguido llamos al metodo getinstancia(porque el constructor es privado),
                //por ultimo llamamos al metodo crear conexion para enlazar la conexion y devolver la variable sqlconection
                SqlCon = Conexion.getInstancia().CrearConexion();                //sql comand representa a una instruccion transact sql o a un SP store_procedure
                SqlCommand Comando = new SqlCommand("categoria_existe", SqlCon);
                //hacemos referencia a la instruccion sp
                Comando.CommandType = CommandType.StoredProcedure;
                //asignamos valores a los parametros y su tipo de dato, y que sera igual a una propiedad o un parametro de este metodo
                Comando.Parameters.Add("@valor", SqlDbType.VarChar).Value = Valor;
                //instanciamos un objeto de la clase parameter y consiste en hacer referencia a los parametros del objeto(SP) de la base de datos
                SqlParameter ParExiste = new SqlParameter();
                //le asignamos valor a la instancia
                ParExiste.ParameterName = "@existe";
                //le declaramos el tipo de dato de la variable de arriba
                ParExiste.SqlDbType = SqlDbType.Int;
                //le asignamos que es un tipo de parametro de salida
                ParExiste.Direction = ParameterDirection.Output;
                //agregamos la variable al comando.parametro
                Comando.Parameters.Add(ParExiste);
                //abrimos la cnx
                SqlCon.Open();
                //ejecutamos el comando
                Comando.ExecuteNonQuery();
                //le asignamos a la variable rpta la variable parexiste y lo convertimos al valor de la rpta
                Rpta = Convert.ToString(ParExiste.Value);
            }
            catch (Exception ex)
            {
                Rpta = ex.Message;
            }
            finally
            {
                if (SqlCon.State == ConnectionState.Open) SqlCon.Close();
            }
            return Rpta;
        }

        public string insertar(CategoriaCE obj)
        {
            string Rpta = "";
            //instanciamos un objeto para la variable conexion
            SqlConnection SqlCon = new SqlConnection();
            try
            {
                //asignamos la clase conexion en la variable, seguido llamos al metodo getinstancia(porque el constructor es privado),
                //por ultimo llamamos al metodo crear conexion para enlazar la conexion y devolver la variable sqlconection
                SqlCon = Conexion.getInstancia().CrearConexion();                //sql comand representa a una instruccion transact sql o a un SP store_procedure
                SqlCommand Comando = new SqlCommand("categoria_insertar", SqlCon);
                //hacemos referencia a la instruccion SP
                Comando.CommandType = CommandType.StoredProcedure;
                //asignamos valor a los parametros y con su tipo de valor que seran referenciados con las propiedades deLa clase entidad
                Comando.Parameters.Add("@nombre", SqlDbType.VarChar).Value = obj.nombre;
                Comando.Parameters.Add("@descripcion", SqlDbType.VarChar).Value = obj.descripcion;
                //abrimos la conexion
                SqlCon.Open();
                //en la variable se ejecutara el comando si cumple satisfactoriamente(saldra ok) sino saldra el segundo mensaje
                Rpta = Comando.ExecuteNonQuery() == 1 ? "OK" : "No se pudo ingresar el registro";
            }
            catch (Exception ex)
            {
                Rpta = ex.Message;
            }
            finally
            {
                if (SqlCon.State == ConnectionState.Open) SqlCon.Close();
            }
            return Rpta;
        }

        public string actualizar(CategoriaCE obj)
        {
            string Rpta = "";
            //instanciamos un objeto para la variable conexion
            SqlConnection SqlCon = new SqlConnection();
            try
            {
                //asignamos la clase conexion en la variable, seguido llamos al metodo getinstancia(porque el constructor es privado),
                //por ultimo llamamos al metodo crear conexion para enlazar la conexion y devolver la variable sqlconection
                SqlCon = Conexion.getInstancia().CrearConexion();                //sql comand representa a una instruccion transact sql o a un SP store_procedure
                SqlCommand Comando = new SqlCommand("categoria_actualizar", SqlCon);
                //hacemos referencia a la instruccion SP
                Comando.CommandType = CommandType.StoredProcedure;
                //asignamos valor a los parametros y con su tipo de valor que seran referenciados con las propiedades deLa clase entidad
                Comando.Parameters.Add("@idcategoria", SqlDbType.Int).Value = obj.idCategoria;
                Comando.Parameters.Add("@nombre", SqlDbType.VarChar).Value = obj.nombre;
                Comando.Parameters.Add("@descripcion", SqlDbType.VarChar).Value = obj.descripcion;
                //abrimos la conexion
                SqlCon.Open();
                //en la variable se ejecutara el comando si cumple satisfactoriamente(saldra ok) sino saldra el segundo mensaje
                Rpta = Comando.ExecuteNonQuery() == 1 ? "OK" : "No se pudo actualizar el registro";
            }
            catch (Exception ex)
            {
                Rpta = ex.Message;
            }
            finally
            {
                if (SqlCon.State == ConnectionState.Open) SqlCon.Close();
            }
            return Rpta;
        }

        public string eliminar(int id)
        {
            string Rpta = "";
            //instanciamos un objeto para la variable conexion
            SqlConnection SqlCon = new SqlConnection();
            try
            {
                //asignamos la clase conexion en la variable, seguido llamos al metodo getinstancia(porque el constructor es privado),
                //por ultimo llamamos al metodo crear conexion para enlazar la conexion y devolver la variable sqlconection
                SqlCon = Conexion.getInstancia().CrearConexion();                //sql comand representa a una instruccion transact sql o a un SP store_procedure
                SqlCommand Comando = new SqlCommand("categoria_eliminar", SqlCon);
                //hacemos referencia a la instruccion SP
                Comando.CommandType = CommandType.StoredProcedure;
                //asignamos valor a los parametros y con su tipo de valor que seran referenciados con las propiedades deLa clase entidad
                Comando.Parameters.Add("@idcategoria", SqlDbType.Int).Value = id;
                //abrimos la conexion
                SqlCon.Open();
                //en la variable se ejecutara el comando si cumple satisfactoriamente(saldra ok) sino saldra el segundo mensaje
                Rpta = Comando.ExecuteNonQuery() == 1 ? "OK" : "No se pudo eliminar el registro";
            }
            catch (Exception ex)
            {
                Rpta = ex.Message;
            }
            finally
            {
                if (SqlCon.State == ConnectionState.Open) SqlCon.Close();
            }
            return Rpta;
        }

        public string activar(int id)
        {
            string Rpta = "";
            //instanciamos un objeto para la variable conexion
            SqlConnection SqlCon = new SqlConnection();
            try
            {
                //asignamos la clase conexion en la variable, seguido llamos al metodo getinstancia(porque el constructor es privado),
                //por ultimo llamamos al metodo crear conexion para enlazar la conexion y devolver la variable sqlconection
                SqlCon = Conexion.getInstancia().CrearConexion();                //sql comand representa a una instruccion transact sql o a un SP store_procedure
                SqlCommand Comando = new SqlCommand("categoria_activar", SqlCon);
                //hacemos referencia a la instruccion SP
                Comando.CommandType = CommandType.StoredProcedure;
                //asignamos valor a los parametros y con su tipo de valor que seran referenciados con las propiedades deLa clase entidad
                Comando.Parameters.Add("@idcategoria", SqlDbType.Int).Value = id;
                //abrimos la conexion
                SqlCon.Open();
                //en la variable se ejecutara el comando si cumple satisfactoriamente(saldra ok) sino saldra el segundo mensaje
                Rpta = Comando.ExecuteNonQuery() == 1 ? "OK" : "No se pudo activar el registro";
            }
            catch (Exception ex)
            {
                Rpta = ex.Message;
            }
            finally
            {
                if (SqlCon.State == ConnectionState.Open) SqlCon.Close();
            }
            return Rpta;
        }

        public string desactivar(int id)
        {
            string Rpta = "";
            //instanciamos un objeto para la variable conexion
            SqlConnection SqlCon = new SqlConnection();
            try
            {
                //asignamos la clase conexion en la variable, seguido llamos al metodo getinstancia(porque el constructor es privado),
                //por ultimo llamamos al metodo crear conexion para enlazar la conexion y devolver la variable sqlconection
                SqlCon = Conexion.getInstancia().CrearConexion();                //sql comand representa a una instruccion transact sql o a un SP store_procedure
                SqlCommand Comando = new SqlCommand("categoria_desactivar", SqlCon);
                //hacemos referencia a la instruccion SP
                Comando.CommandType = CommandType.StoredProcedure;
                //asignamos valor a los parametros y con su tipo de valor que seran referenciados con las propiedades deLa clase entidad
                Comando.Parameters.Add("@idcategoria", SqlDbType.Int).Value = id;
                //abrimos la conexion
                SqlCon.Open();
                //en la variable se ejecutara el comando si cumple satisfactoriamente(saldra ok) sino saldra el segundo mensaje
                Rpta = Comando.ExecuteNonQuery() == 1 ? "OK" : "No se pudo desactivar el registro";
            }
            catch (Exception ex)
            {
                Rpta = ex.Message;
            }
            finally
            {
                if (SqlCon.State == ConnectionState.Open) SqlCon.Close();
            }
            return Rpta;
        }

    }
}
