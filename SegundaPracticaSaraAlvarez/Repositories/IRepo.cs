using SegundaPracticaSaraAlvarez.Models;

namespace SegundaPracticaSaraAlvarez.Repositories
{
    public interface IRepo
    {
        List<Comic> GetComics();
        void Insert(string nombre, string imagen, string descripcion);
    }
}
