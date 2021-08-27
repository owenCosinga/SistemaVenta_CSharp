using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using CapaEntidad;
using CapaDatos;

namespace CapaNegocio
{
   public class CategoriaCN
    {

        public static DataTable listar()
        {

            CategoriaCD categoria = new CategoriaCD();

            return categoria.listar();

        }

        public static DataTable buscar(string valor)
        {

            CategoriaCD categoria = new CategoriaCD();

            return categoria.buscar(valor);

        }

        public static string insertar(string nombre, string descripcion)
        {

            CategoriaCD categoriaCD = new CategoriaCD();

            string existe = categoriaCD.Existe(nombre);

            if (existe.Equals("1"))
            {
                return "la categoria ya existe";
            }
            else
            {

                CategoriaCE categoria = new CategoriaCE();
                
                categoria.nombre = nombre;
                categoria.descripcion = descripcion;

                return categoriaCD.insertar(categoria);

            }

        }

        public static string actualizar(int Id, string NombreAnt, string Nombre, string Descripcion)
        {
            //instanciamos un objeto de la clase categoria
            CategoriaCD Datos = new CategoriaCD();
            //instanciamos un objeto de la clase entidad
            CategoriaCE Obj = new CategoriaCE();

            if (NombreAnt.Equals(Nombre))
            {
                //le asignamos a las propiedades los parametros de este metodo
                Obj.idCategoria = Id;
                Obj.nombre = Nombre;
                Obj.descripcion = Descripcion;
                //retornamos el metodo
                return Datos.actualizar(Obj);
            }
            else
            {
                //declaramos y asignamos en la variable existe la clase datos y el metodo existe ysu parametro nombre
                string Existe = Datos.Existe(Nombre);

                if (Existe.Equals("1"))
                {
                    return "La categoría ya existe";
                }
                else
                {
                    //le asignamos a las propiedades los parametros de este metodo
                    Obj.idCategoria = Id;
                    Obj.nombre = Nombre;
                    Obj.descripcion = Descripcion;
                    //retornamos el metodo
                    return Datos.actualizar(Obj);
                }
            }

        }

        public static string eliminar(int id)
        {

            CategoriaCD categoria = new CategoriaCD();

            return categoria.eliminar(id);

        }

        public static string activar(int id)
        {

            CategoriaCD categoria = new CategoriaCD();

           return categoria.activar(id);

        }

        public static string desactivar(int id)
        {

            CategoriaCD categoria = new CategoriaCD();

            return categoria.desactivar(id);

        }

    }
}
