using LibrosWeb.Repository.IRepository;
using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace LibrosWeb.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly IHttpClientFactory _clientFactory;

        public Repository(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        public async Task<bool> ActualizarAsync(string Url, T actualizarItem, string token)
        {
            var peticion = new HttpRequestMessage(HttpMethod.Patch, Url);
            if (actualizarItem != null)
            {
                peticion.Content = new StringContent(
                    JsonConvert.SerializeObject(actualizarItem), Encoding.UTF8, "application/json"
                    );
            }
            else
            {
                return false;
            }

            var cliente = _clientFactory.CreateClient();

            if (token!=null && token.Length>0)
            {
                
                cliente.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            }

            HttpResponseMessage respuesta = await cliente.SendAsync(peticion);
            if (respuesta.StatusCode == System.Net.HttpStatusCode.NoContent)
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        public async Task<bool> BorrarAsync(string Url, int Id, string token)
        {
            var peticion = new HttpRequestMessage(HttpMethod.Delete, Url + Id);


            var cliente = _clientFactory.CreateClient();

            if (token != null && token.Length > 0)
            {

                cliente.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            }
            HttpResponseMessage respuesta = await cliente.SendAsync(peticion);

            if (respuesta.StatusCode == System.Net.HttpStatusCode.NoContent)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<bool> CrearAsync(string Url, T crearItem, string token)
        {
            var peticion = new HttpRequestMessage(HttpMethod.Post, Url);
            if (crearItem != null)
            {
                peticion.Content = new StringContent(
                      JsonConvert.SerializeObject(crearItem), Encoding.UTF8, "application/json"
                      );
            }
            else
            {
                return false;
            }

            var cliente = _clientFactory.CreateClient();

            if (token != null && token.Length > 0)
            {

                cliente.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            }
            HttpResponseMessage respuesta = await cliente.SendAsync(peticion);
            if (respuesta.StatusCode == System.Net.HttpStatusCode.Created)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<T> GetTAsync(string Url, int Id)
        {
            var peticion = new HttpRequestMessage(HttpMethod.Get, Url + Id);


            var cliente = _clientFactory.CreateClient();
            HttpResponseMessage respuesta = await cliente.SendAsync(peticion);

            if (respuesta.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var jsonstrin = await respuesta.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<T>(jsonstrin);

            }
            else
            {
                return null;
            }
        }

        public async Task<IEnumerable> GetTodosAsync(string Url)
        {
            var peticion = new HttpRequestMessage(HttpMethod.Get, Url);


            var cliente = _clientFactory.CreateClient();
            HttpResponseMessage respuesta = await cliente.SendAsync(peticion);

            if (respuesta.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var jsonstrin = await respuesta.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<IEnumerable<T>>(jsonstrin);

            }
            else
            {
                return null;
            }
        }
    }
}
