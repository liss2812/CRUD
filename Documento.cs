using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace simpleCRUD
{
    class Documento
    {
        //propiedades
        public int _documentoId { get; set; }
        public string _titulo { get; set; }
        public string _contenido { get; set; }
        public string _fechapublicacion { get; set; }
        public string _descripcion{ get; set; }

        //instancia a la clase Crud
        private Crud crud = new Crud();

        //metodo para retornar los registros de la tabla Documento
        public MySqlDataReader getAllDocumento()
        {
            string query = "SELECT documentoId,titulo, contenido,fechapublicacion,descripcion FROM documento";

            //llamado al metodo select de la clase Crud
            return crud.select(query);
        }

        //metodo para insertar o editar un registro
        public Boolean newEditDocumento(string action)
        {
            if (action == "new")
            {
                string query = "INSERT INTO documento(titulo, contenido, fechapublicacion, descripcion)" +
                    "VALUES ('" + _titulo + "', '" + _contenido+ "', '" + _fechapublicacion + "', '" + _descripcion + "')";
                crud.executeQuery(query); //llamato al metodo executeQuery de la clase Crud
                return true;
            }
            else if (action == "edit")
            {
                string query = "UPDATE documneto SET "
                    + "titulo='" + _titulo+ "' ,"
                    + "contenido='" + _contenido + "',"
                    + "fechapublicacion='" + _fechapublicacion+ "',"
                    + "descripcion='" + _descripcion + "'"
                    + "WHERE "
                    + "bookId='" + _documentoId + "'";
                crud.executeQuery(query);
                return true;
            }

            return false;
        }

        internal MySqlDataReader getAllDocumentos()
        {
            throw new NotImplementedException();
        }

        //metodo para eliminar
        public Boolean deleteDocuemnto()
        {
            string query = "DELETE FROM documento WHERE documnetoId='" + _documentoId + "'";
            crud.executeQuery(query);
            return true;
        }
    }
}