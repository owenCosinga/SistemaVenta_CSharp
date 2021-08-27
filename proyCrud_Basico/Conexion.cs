using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace CapaDatos
{
    public class Conexion
    {
        //declaramos variables de conexion
        private string Base;
        private string Servidor;
        private string Usuario;
        private string Clave;
        private bool Seguridad;
        //declaramos un objeto del tipo conexion de esta clase
        private static Conexion Con = null;


        private Conexion() //se utiliza este modificador privado para que esta clase no pueda ser instanciada dentro de otra clase
        {
            //asignamos valores para los parametros de conexion
            this.Base = "dbsistema";
            this.Servidor = "localhost";
            this.Usuario = "owensa";
            this.Clave = "zeim4545";
            this.Seguridad = true; //si asigna true significa que usara la seguridad de windows
        }
        public SqlConnection CrearConexion()
        {
            //declaramos una variable sqlconnection
            SqlConnection Cadena = new SqlConnection();
            try
            {
                Cadena.ConnectionString = "Server=" + this.Servidor + "; Database=" + this.Base + ";";

                if (this.Seguridad)
                {
                    //windows authentication
                    Cadena.ConnectionString = Cadena.ConnectionString + "Integrated Security = SSPI";
                }
                else
                {
                    //sql server authentication
                    Cadena.ConnectionString = Cadena.ConnectionString + "User Id=" + this.Usuario + ";Password=" + this.Clave;
                }
            }
            catch (Exception ex)
            {
                Cadena = null;
                throw ex; //se muestra la excepcion
            }
            return Cadena;
        }

        public static Conexion getInstancia() //crear instancia
        {
            if (Con == null)
            {
                Con = new Conexion(); //se crea una instancia
            }
            return Con;
        }
    }
}
