using LibrosWeb.Models;
using System.Threading.Tasks;

namespace LibrosWeb.Repository.IRepository
{
    public interface IAcounRepository : IRepository<UsuarioM>
    {
        Task<UsuarioM> LoginAsync(string url, UsuarioM usuario);
        Task<bool> RegistroAsync(string url, UsuarioM usuario);

    }
}
