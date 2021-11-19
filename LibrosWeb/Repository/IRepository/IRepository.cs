using System.Collections;
using System.Threading.Tasks;

namespace LibrosWeb.Repository.IRepository
{
    public interface IRepository<T> where T : class
    {
        Task<IEnumerable> GetTodosAsync(string Url);
        Task<T> GetTAsync(string Url, int Id);
        Task<bool> CrearAsync(string Url, T crearItem, string token);
        Task<bool> ActualizarAsync(string Url, T actualizarItem, string token);
        Task<bool> BorrarAsync(string Url, int Id, string token);


    }
}
