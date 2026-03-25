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

        public async Task<List<Empleado>> GetEmpleadosAsync()
        {
            using (HttpClient client = new HttpClient())
            {
                string request = "api/empleado";
                client.BaseAddress = new Uri(this.ApiUrl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.header);
                HttpResponseMessage response = await client.GetAsync(request);
                if(response.IsSuccessStatusCode == true)
                {
                    List<Empleado> empleados = await response.Content.ReadAsAsync<List<Empleado>>();
                    return empleados;
                }else
                {
                    return null;
                }
            }
        }

        public async Task<List<string>> GetOficiosAsync()
        {
            using (HttpClient client = new HttpClient())
            {
                string request = "api/empleado/todoslosoficios";
                client.BaseAddress = new Uri(this.ApiUrl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.header);
                HttpResponseMessage response = await client.GetAsync(request);
                if(response.IsSuccessStatusCode == true)
                {
                    List<string> oficios = await response.Content.ReadAsAsync<List<string>>();
                    return oficios;
                }else
                {
                    return null;
                }
            }
        }

        public async Task<List<Empleado>> GetEmpleadosByOficiosAsync(string oficio)
        {
            using (HttpClient client = new HttpClient())
            {
                string request = "api/empleado/getempleadosbyoficio/" + oficio;
                client.BaseAddress = new Uri(this.ApiUrl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.header);
                HttpResponseMessage response = await client.GetAsync(request);
                if (response.IsSuccessStatusCode == true)
                {
                    List<Empleado> empleados = await response.Content.ReadAsAsync<List<Empleado>>();
                    return empleados;
                }
                else
                {
                    return null;
                }
            }
        }
    }
}
