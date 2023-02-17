using SegundaPracticaSaraAlvarez.Models;

namespace SegundaPracticaSaraAlvarez.Repositories
{
    public interface IRepo
    {
        List<Comic> GetComics();
        void Insert(int idcomic, string nombre, string imagen, string descripcion);
    }
}
