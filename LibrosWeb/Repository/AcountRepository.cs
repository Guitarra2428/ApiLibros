using LibrosWeb.Models;
using LibrosWeb.Repository.IRepository;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace LibrosWeb.Repository
{
    public class AcountRepository : Repository<UsuarioM>, IAcounRepository
    {
        private readonly IHttpClientFactory _clientFactory;

        public AcountRepository(IHttpClientFactory clientFactory) : base(clientFactory)
        {
            _clientFactory = clientFactory;
        }

        public async Task<UsuarioM> LoginAsync(string url, UsuarioM usuario)
        {
            var Peticion = new HttpRequestMessage(HttpMethod.Post, url);
            if (usuario != null)
            {
                Peticion.Content = new StringContent(
                    JsonConvert.SerializeObject(usuario), Encoding.UTF8, "application/json"
                    );
            }
            else
            {
                return new UsuarioM();
            }
            var Cliente = _clientFactory.CreateClient();
            HttpResponseMessage respuesta = await Cliente.SendAsync(Peticion);

            if (respuesta.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var jsonstrin = await respuesta.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<UsuarioM>(jsonstrin);

            }
            else
            {
                return new UsuarioM();

            }
        }

        public async Task<bool> RegistroAsync(string url, UsuarioM usuario)
        {
            var Peticion = new HttpRequestMessage(HttpMethod.Post, url);
            if (usuario != null)
            {
                Peticion.Content = new StringContent(
                    JsonConvert.SerializeObject(usuario), Encoding.UTF8, "application/json"
                    );
            }
            else
            {
                return false; ;
            }
            var Cliente = _clientFactory.CreateClient();
            HttpResponseMessage respuesta = await Cliente.SendAsync(Peticion);

            if (respuesta.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return true;

            }
            else
            {
                return false;
            }
        }
    }
}
