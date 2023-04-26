﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.Text;
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



        public async Task<IActionResult> TipoProducto()
        {
            List<TipoProducto> tipoList = new List<TipoProducto>();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://localhost:7024/api/TipoProducto"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    tipoList = JsonConvert.DeserializeObject<List<TipoProducto>>(apiResponse);
                }
            }
            return View(tipoList);
        }

        public async Task<IActionResult> Usuario()
        {
            List<Usuario> usuarioList = new List<Usuario>();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://localhost:7024/api/Usuario"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    usuarioList = JsonConvert.DeserializeObject<List<Usuario>>(apiResponse);
                }
            }
            return View(usuarioList);
        }

        public async Task<IActionResult> Rol()
        {
            List<Rol> rolList = new List<Rol>();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://localhost:7024/api/Rol"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    rolList = JsonConvert.DeserializeObject<List<Rol>>(apiResponse);
                }
            }
            return View(rolList);
        }


        public async Task<IActionResult> AgregarProducto()
        {
            List<TipoProducto> tipos = new List<TipoProducto>();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://localhost:7024/api/TipoProducto"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    tipos = JsonConvert.DeserializeObject<List<TipoProducto>>(apiResponse);
                }
            }

            List<SelectListItem> tiposOps = new List<SelectListItem>();
            foreach (TipoProducto tipo in tipos)
            {
                tiposOps.Add(new SelectListItem
                {
                    Text = tipo.Nombre,
                    Value = tipo.Id.ToString()
                });
            };

            ViewData["Tipos"] = tiposOps;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AgregarProducto(Producto producto)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var json = JsonConvert.SerializeObject(producto);
                    var data = new StringContent(json, Encoding.UTF8, "application/json");
                    using (var httpClient = new HttpClient())
                    {
                        using (var response = await httpClient.PostAsync("https://localhost:7024/api/Producto", data))
                        {
                            if (response.IsSuccessStatusCode)
                            {
                                return RedirectToAction(nameof(Index));
                            }
                        }
                    }
                }
                return RedirectToAction(nameof(AgregarProducto));

            }
            catch
            {
                return RedirectToAction(nameof(AgregarProducto));
            }
        }

        public async Task<IActionResult> EditarProducto(int id)
        {
            Producto producto = new Producto();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://localhost:7024/api/Producto/" + id))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    producto = JsonConvert.DeserializeObject<Producto>(apiResponse);
                }
            }

            List<TipoProducto> tipos = new List<TipoProducto>();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://localhost:7024/api/TipoProducto"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    tipos = JsonConvert.DeserializeObject<List<TipoProducto>>(apiResponse);
                }
            }

            List<SelectListItem> tiposOps = new List<SelectListItem>();
            foreach (TipoProducto tipo in tipos)
            {
                if (tipo.Id == producto.Id)
                {
                    tiposOps.Add(new SelectListItem
                    {
                        Selected = true,
                        Text = tipo.Nombre,
                        Value = tipo.Id.ToString()
                    });
                }
                else
                {
                    tiposOps.Add(new SelectListItem
                    {
                        Text = tipo.Nombre,
                        Value = tipo.Id.ToString()
                    });
                }
            };

            ViewData["Tipos"] = tiposOps;
            return View(producto);
        }

        [HttpPost]
        public async Task<IActionResult> EditarProducto(Producto producto)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var json = JsonConvert.SerializeObject(producto);
                    var data = new StringContent(json, Encoding.UTF8, "application/json");
                    using (var httpClient = new HttpClient())
                    {
                        using (var response = await httpClient.PutAsync("https://localhost:7024/api/Producto", data))
                        {
                            if (response.IsSuccessStatusCode)
                            {
                                return RedirectToAction(nameof(Index));
                            }
                        }
                    }
                }
                return RedirectToAction(nameof(EditarProducto));

            }
            catch
            {
                return RedirectToAction(nameof(EditarProducto));
            }
        }

        //Agregar Promociones

        public async Task<IActionResult> AgregarPromocion()
        {
            List<Promocion> promos = new List<Promocion>();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://localhost:7024/api/Promocion"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    promos = JsonConvert.DeserializeObject<List<Promocion>>(apiResponse);
                }
            }

            List<SelectListItem> promosOps = new List<SelectListItem>();
            foreach (Promocion promo in promos)
            {
                promosOps.Add(new SelectListItem
                {
                    Text = promo.Nombre,
                    Value = promo.Id.ToString()
                });
            };

            ViewData["Promociones"] = promosOps;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AgregarPromocion(Promocion promocion)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var json = JsonConvert.SerializeObject(promocion);
                    var data = new StringContent(json, Encoding.UTF8, "application/json");
                    using (var httpClient = new HttpClient())
                    {
                        using (var response = await httpClient.PostAsync("https://localhost:7024/api/Promocion", data))
                        {
                            if (response.IsSuccessStatusCode)
                            {
                                return RedirectToAction(nameof(Index));
                            }
                        }
                    }
                }
                return RedirectToAction(nameof(AgregarPromocion));

            }
            catch
            {
                return RedirectToAction(nameof(AgregarPromocion));
            }
        }

        //Editar Promociones

    }
}