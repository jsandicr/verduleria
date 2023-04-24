using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Verduleria.Models;

namespace Verduleria.Controllers
{
    public class VerduleriaController : Controller
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

            List<Promocion> promocionList = new List<Promocion>();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://localhost:7024/api/Promocion"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    promocionList = JsonConvert.DeserializeObject<List<Promocion>>(apiResponse);
                }
            }

            Carrito carrito = new Carrito();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://localhost:7024/api/Carrito/" + 1))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    carrito = JsonConvert.DeserializeObject<Carrito>(apiResponse);
                }
            }

            var total = 0;
            if(carrito.DetalleCarrito != null)
            {
                foreach (DetalleCarrito detalle in carrito.DetalleCarrito)
                {
                    total += (int)detalle.Costo;
                }
                ViewData["DetalleCarrito"] = carrito.DetalleCarrito;
            }
            else
            {
                ViewData["DetalleCarrito"] = new List<DetalleCarrito>();
            }

            ViewData["Productos"] = productoList;
            ViewData["Promociones"] = promocionList;
            ViewData["TotalCarrito"] = total;
            return View();
        }

        public async Task<IActionResult> Checkout()
        {
            Carrito carrito = new Carrito();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://localhost:7024/api/Carrito/" + 1))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    carrito = JsonConvert.DeserializeObject<Carrito>(apiResponse);
                }
            }

            var total = 0;
            if(carrito.DetalleCarrito != null)
            {
                foreach (DetalleCarrito detalle in carrito.DetalleCarrito)
                {
                    total += (int)detalle.Costo;
                }
            }
            else
            {
                carrito = new Carrito();
                carrito.DetalleCarrito = new List<DetalleCarrito>();
            }
            ViewData["TotalCarrito"] = total;

            return View(carrito);
        }

        public async Task<IActionResult> PostCarrito(string IdProducto)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using (var httpClient = new HttpClient())
                    {
                        using (var response = await httpClient.GetAsync("https://localhost:7024/api/Carrito?IdUsuario=" + 1 + "&IdProducto=" + IdProducto + "&cantidad=" + 1))
                        {
                            string apiResponse = await response.Content.ReadAsStringAsync();
                            if (response.IsSuccessStatusCode)
                            {
                                return RedirectToAction(nameof(Index));
                            }
                        }
                    }
                }
                return RedirectToAction(nameof(Index));

            }
            catch
            {
                return RedirectToAction(nameof(Index));
            }
        }

        public async Task<IActionResult> RestaCarrito(string IdProducto)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using (var httpClient = new HttpClient())
                    {
                        using (var response = await httpClient.GetAsync("https://localhost:7024/api/Carrito/RestaCarrito?IdUsuario=" + 1 + "&IdProducto=" + IdProducto + "&cantidad=" + 1))
                        {
                            string apiResponse = await response.Content.ReadAsStringAsync();
                            if (response.IsSuccessStatusCode)
                            {
                                return RedirectToAction(nameof(Index));
                            }
                        }
                    }
                }
                return RedirectToAction(nameof(Index));

            }
            catch
            {
                return RedirectToAction(nameof(Index));
            }
        }

        public async Task<IActionResult> CompraPromocion(string IdPromocion)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using (var httpClient = new HttpClient())
                    {
                        using (var response = await httpClient.GetAsync("https://localhost:7024/api/Promocion/CompraPromocion?IdPromocion=" + IdPromocion + "&IdUsuario=1"))
                        {
                            string apiResponse = await response.Content.ReadAsStringAsync();
                            if (response.IsSuccessStatusCode)
                            {
                                return RedirectToAction(nameof(Index));
                            }
                        }
                    }
                }
                return View();

            }
            catch
            {
                return View();
            }
        }

        public async Task<IActionResult> Compra()
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using (var httpClient = new HttpClient())
                    {
                        using (var response = await httpClient.GetAsync("https://localhost:7024/api/Compra?IdUsuario=" + 1))
                        {
                            string apiResponse = await response.Content.ReadAsStringAsync();
                            if (response.IsSuccessStatusCode)
                            {
                                return RedirectToAction(nameof(Index));
                            }
                        }
                    }
                }
                return RedirectToAction(nameof(Checkout));

            }
            catch
            {
                return RedirectToAction(nameof(Checkout));
            }
        }
    }
}