using LibrosWeb.Models;
using LibrosWeb.Repository.IRepository;
using System.Net.Http;

namespace LibrosWeb.Repository
{
    public class UsuarioRepository : Repository<UsuarioU>, IUsuarioRepository
    {
        private readonly IHttpClientFactory _clientFactory;

        public UsuarioRepository(IHttpClientFactory clientFactory) : base(clientFactory)
        {
            _clientFactory = clientFactory;
        }
    }
}
