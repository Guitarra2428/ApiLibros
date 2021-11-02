using LibrosWeb.Models;
using LibrosWeb.Repository.IRepository;
using System.Net.Http;

namespace LibrosWeb.Repository
{
    public class AutorRepository : Repository<Autor>, IAutorRepository
    {
        private readonly IHttpClientFactory _clientFactory;

        public AutorRepository(IHttpClientFactory clientFactory) : base(clientFactory)
        {
            _clientFactory = clientFactory;
        }
    }
}
