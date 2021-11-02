using LibrosWeb.Models;
using LibrosWeb.Repository.IRepository;
using System.Net.Http;

namespace LibrosWeb.Repository
{
    public class LibroRepository : Repository<Libro>, ILibroRepository
    {
        private readonly IHttpClientFactory _clientFactory;

        public LibroRepository(IHttpClientFactory clientFactory) : base(clientFactory)
        {
            _clientFactory = clientFactory;
        }
    }
}
