using System.Collections;
using System.Threading.Tasks;

namespace LibrosWeb.Repository.IRepository
{
    public interface IRepository<T> where T : class
    {
        Task<IEnumerable> GetTodosAsync(string Url);
        Task<T> GetTAsync(string Url, int Id);
        Task<bool> CrearAsync(string Url, T crearItem);
        Task<bool> ActualizarAsync(string Url, T actualizarItem);
        Task<bool> BorrarAsync(string Url, int Id);


    }
}
