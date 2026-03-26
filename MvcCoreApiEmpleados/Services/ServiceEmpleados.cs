using Newtonsoft.Json;
using NuggetApiModels.Models;
using System.Net.Http.Headers;

namespace MvcCoreApiEmpleados.Services
{
    public class ServiceEmpleados
    {
        private string ApiUrl;
        private MediaTypeWithQualityHeaderValue header;

        public ServiceEmpleados(IConfiguration configuration)
        {
            this.ApiUrl = configuration.GetValue<string>("ApiUrls:ApiEmpleado");
            this.header = new MediaTypeWithQualityHeaderValue("application/json");
        }

        /* LO QUE NECESITAMOS EN EL METODO GENERICO ES SIMPLIFICAR
         * EL REQUEST DE LA PETICION */

        public async Task<T> CallApiAsync<T>(string request)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(this.ApiUrl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.header);
                HttpResponseMessage response = await client.GetAsync(request);
                if (response.IsSuccessStatusCode == true)
                {
                    return await response.Content.ReadAsAsync<T>();
                }
                else
                {
                    return default(T);
                }
            }
        }

        public async Task<List<Empleado>> GetEmpleadosAsync()
        {
            using (HttpClient client = new HttpClient())
            {
                string request = "api/empleado";
                return await this.CallApiAsync<List<Empleado>>(request);
            }
        }

        public async Task<List<string>> GetOficiosAsync()
        {
            using (HttpClient client = new HttpClient())
            {
                string request = "api/empleado/todoslosoficios";
                return await this.CallApiAsync<List<string>>(request);
            }
        }

        public async Task<List<Empleado>> GetEmpleadosByOficiosAsync(string oficio)
        {
            using (HttpClient client = new HttpClient())
            {
                string request = "api/empleado/getempleadosbyoficio/" + oficio;
                return await this.CallApiAsync<List<Empleado>>(request);
            }
        }

        public async Task<Empleado> GetEmpleadoByIdAync (int id)
        {
            using (HttpClient client = new HttpClient())
            {
                string request = "api/empleado/" + id;
                return await this.CallApiAsync<Empleado>(request);
            }
        }
    }
}
