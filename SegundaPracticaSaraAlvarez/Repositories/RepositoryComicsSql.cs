using System.Data.SqlClient;
using System.Data;
using SegundaPracticaSaraAlvarez.Models;

namespace SegundaPracticaSaraAlvarez.Repositories
{
    #region PROCEDURES
    //CREATE PROCEDURE SP_INSERT_COMIC
    //(@IDCOMIC INT, @NOMBRE NVARCHAR(150), @IMAGEN NVARCHAR(600), @DESCRIPCION NVARCHAR(500))
    //AS
    //    INSERT INTO COMICS VALUES(@IDCOMIC, @NOMBRE, @IMAGEN, @DESCRIPCION)
    //GO
    #endregion
    public class RepositoryComicsSql: IRepo
    {
        private DataTable tablaComics;
        private SqlConnection cn;
        private SqlDataAdapter adapter;
        private SqlCommand com;
        public RepositoryComicsSql()
        {
            string connectionString = @"Data Source=LOCALHOST\DESARROLLO;Initial Catalog=HOSPITAL;Persist Security Info=True;User ID=SA;Password=MCSD2022;";
            string sql = "SELECT * FROM COMICS";
            //SqlDataAdapter adapter = new SqlDataAdapter(sql, connectionString);
            this.adapter = new SqlDataAdapter(sql, connectionString);
            this.tablaComics = new DataTable();
            adapter.Fill(this.tablaComics);

            this.cn = new SqlConnection(connectionString);
            this.com = new SqlCommand();
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

        public void Insert(int idcomic, string nombre, string imagen, string descripcion)
        {
            SqlParameter pamid = new SqlParameter("@idcomic", idcomic);
            this.com.Parameters.Add(pamid);
            SqlParameter pamnombre = new SqlParameter("@nombre", nombre);
            this.com.Parameters.Add(pamnombre);
            SqlParameter pamdirec = new SqlParameter("@imagen", imagen);
            this.com.Parameters.Add(pamdirec);
            SqlParameter pamtlf = new SqlParameter("@descripcion", descripcion);
            this.com.Parameters.Add(pamtlf);
            this.com.CommandType = CommandType.StoredProcedure;
            this.com.CommandText = "SP_INSERT_COMIC";
            this.cn.Open();
            this.com.ExecuteNonQuery();
            this.cn.Close();
            this.com.Parameters.Clear();
        }
    }
}
