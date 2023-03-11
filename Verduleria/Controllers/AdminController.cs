using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Verduleria.Models;

namespace Verduleria.Controllers
{
    public class AdminController : Controller
    {
        public async Task<IActionResult> Index()
        {
            List<Producto> productoList = new List<Producto>();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://localhost:7024/api/Producto"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    productoList = JsonConvert.DeserializeObject<List<Producto>>(apiResponse);
                }
            }

            return View(productoList);
        }

        public async Task<IActionResult> Promociones()
        {
            List<Promocion> promocionList = new List<Promocion>();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://localhost:7024/api/Promocion"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    promocionList = JsonConvert.DeserializeObject<List<Promocion>>(apiResponse);
                }
            }
            return View(promocionList);
        }
    }
}