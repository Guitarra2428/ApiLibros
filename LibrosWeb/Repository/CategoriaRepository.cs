using LibrosWeb.Models;
using LibrosWeb.Repository.IRepository;
using System.Net.Http;

namespace LibrosWeb.Repository
{
    public class CategoriaRepository : Repository<Categoria>, ICategoriaRepository
    {
        private readonly IHttpClientFactory _clientFactory;

        public CategoriaRepository(IHttpClientFactory clientFactory) : base(clientFactory)
        {
            _clientFactory = clientFactory;
        }
    }
}
