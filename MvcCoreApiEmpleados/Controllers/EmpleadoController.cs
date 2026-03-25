using Microsoft.AspNetCore.Mvc;
using MvcCoreApiEmpleados.Services;
using NuggetApiModels.Models;
using System.Threading.Tasks;

namespace MvcCoreApiEmpleados.Controllers
{
    public class EmpleadoController : Controller
    {
        private ServiceEmpleados service;

        public EmpleadoController (ServiceEmpleados service)
        {
            this.service = service;
        }


        public async Task<IActionResult> Index()
        {
            List<Empleado> empleados = await this.service.GetEmpleadosAsync();
            List<string> oficios = await this.service.GetOficiosAsync();
            ViewBag.Oficios = oficios;
            return View(empleados);
        }

        [HttpPost]
        public async Task<IActionResult> Index(string oficio)
        {
            List<Empleado> empleados = await this.service.GetEmpleadosByOficiosAsync(oficio);
            List<string> oficios = await this.service.GetOficiosAsync();
            ViewBag.Oficios = oficios;
            return View(empleados);
        }
    }
}
