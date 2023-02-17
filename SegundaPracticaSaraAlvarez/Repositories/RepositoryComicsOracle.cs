using Microsoft.VisualBasic;
using Oracle.ManagedDataAccess.Client;
using SegundaPracticaSaraAlvarez.Models;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

#region PROCEDURES
//CREATE OR REPLACE PROCEDURE SP_INSERT_COMIC
//      (P_NOMBRE COMICS.NOMBRE%TYPE,
//       P_IMAGEN COMICS.IMAGEN%TYPE, P_DESCRIPCION COMICS.DESCRIPCION%TYPE)
//       AS
//       BEGIN
//       INSERT INTO COMICS VALUES((SELECT MAX(IDCOMIC) +1 FROM COMICS)
//       , P_NOMBRE, P_IMAGEN, P_DESCRIPCION);
//COMMIT;
//END;
#endregion
namespace SegundaPracticaSaraAlvarez.Repositories
{
        public class RepositoryComicsOracle : IRepo
        {
            private OracleConnection cn;
            private OracleCommand com;
            private OracleDataAdapter adapter;
            private DataTable tablaComics;
            public RepositoryComicsOracle()
            {
                string connectionString = "DATA SOURCE=LOCALHOST:1521/XE;PERSIST SECURITY INFO=False;USER ID=system; PASSWORD=oracle;";
                string sql = "SELECT * FROM COMICS";
                this.adapter = new OracleDataAdapter(sql, connectionString);
                this.tablaComics = new DataTable();
                adapter.Fill(this.tablaComics);

                this.cn = new OracleConnection(connectionString);
                this.com = new OracleCommand();
                this.com.Connection = this.cn;
            }

        public List<Comic> GetComics()
        {
            var query = from datos in this.tablaComics.AsEnumerable()
                        select new Comic
                        {
                            IdComic = datos.Field<int>("IDCOMIC"),
                            Nombre = datos.Field<string>("NOMBRE"),
                            Imagen = datos.Field<string>("IMAGEN"),
                            Descripcion = datos.Field<string>("DESCRIPCION")
                        };
            return query.ToList();
        }

        public void Insert(string nombre, string imagen, string descripcion)
        {
            OracleParameter pamnombre = new OracleParameter(":p_nombre", nombre);
            this.com.Parameters.Add(pamnombre);
            OracleParameter pamim = new OracleParameter(":p_imagen", imagen);
            this.com.Parameters.Add(pamim);
            OracleParameter pamdesc = new OracleParameter(":p_descripcion", descripcion);
            this.com.Parameters.Add(pamdesc);
            this.com.CommandType = CommandType.StoredProcedure;
            this.com.CommandText = "SP_INSERT_COMIC";
            this.cn.Open();
            this.com.ExecuteNonQuery();
            this.cn.Close();
            this.com.Parameters.Clear();
        }
    }
}
