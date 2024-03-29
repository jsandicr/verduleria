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

        //Arreglar
        public async Task<IActionResult> ProductoPromocion()
        {

            List<ProductoPromocion> promocionList = new List<ProductoPromocion>();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://localhost:7024/api/ProductoPromocion"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    promocionList = JsonConvert.DeserializeObject<List<ProductoPromocion>>(apiResponse);
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

        public async Task<IActionResult> Compra()
        {
            List<Compra> compraList = new List<Compra>();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://localhost:7024/api/Compra"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    compraList = JsonConvert.DeserializeObject<List<Compra>>(apiResponse);
                }
            }
            return View(compraList);
        }

        //Agregar Producto

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

        //Editar Producto

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
        //Eliminar Producto


        //Agregar Promociones

        public async Task<IActionResult> AgregarPromocion()
        {         
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
                                return RedirectToAction(nameof(Promociones));
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

        public async Task<IActionResult> EditarPromocion(int id)
        {
            Promocion promocion = new Promocion();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://localhost:7024/api/Promocion/" + id))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    promocion = JsonConvert.DeserializeObject<Promocion>(apiResponse);
                }
            }

            return View(promocion);
        }

        [HttpPost]
        public async Task<IActionResult> EditarPromocion(Promocion promocion)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var json = JsonConvert.SerializeObject(promocion);
                    var data = new StringContent(json, Encoding.UTF8, "application/json");
                    using (var httpClient = new HttpClient())
                    {
                        using (var response = await httpClient.PutAsync("https://localhost:7024/api/Promocion", data))
                        {
                            if (response.IsSuccessStatusCode)
                            {
                                return RedirectToAction(nameof(Promociones));
                            }
                        }
                    }
                }
                return RedirectToAction(nameof(EditarPromocion));

            }
            catch
            {
                return RedirectToAction(nameof(EditarPromocion));
            }
        }

        //Agregar ProductoPromocion

        public async Task<IActionResult> AgregarProductoPromocion()
        {
            List<Producto> productos = new List<Producto>();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://localhost:7024/api/Producto"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    productos = JsonConvert.DeserializeObject<List<Producto>>(apiResponse);
                }
            }

            List<SelectListItem> productosOps = new List<SelectListItem>();
            foreach (Producto producto in productos)
            {
                productosOps.Add(new SelectListItem
                {
                    Text = producto.Nombre,
                    Value = producto.Id.ToString()
                });
            };

            ViewData["Productos"] = productosOps;

            List<Promocion> promociones = new List<Promocion>();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://localhost:7024/api/Promocion"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    promociones = JsonConvert.DeserializeObject<List<Promocion>>(apiResponse);
                }
            }

            List<SelectListItem> promocionesOps = new List<SelectListItem>();
            foreach (Promocion promocion in promociones)
            {
                promocionesOps.Add(new SelectListItem
                {
                    Text = promocion.Nombre,
                    Value = promocion.Id.ToString()
                });
            };

            ViewData["Promociones"] = promocionesOps;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AgregarProductoPromocion(ProductoPromocion productopromocion)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var json = JsonConvert.SerializeObject(productopromocion);
                    var data = new StringContent(json, Encoding.UTF8, "application/json");
                    using (var httpClient = new HttpClient())
                    {
                        using (var response = await httpClient.PostAsync("https://localhost:7024/api/ProductoPromocion", data))
                        {
                            if (response.IsSuccessStatusCode)
                            {
                                return RedirectToAction(nameof(ProductoPromocion));
                            }
                        }
                    }
                }
                return RedirectToAction(nameof(AgregarProductoPromocion));
            }
            catch
            {
                return RedirectToAction(nameof(AgregarProductoPromocion));
            }
        }

        //Editar ProductoPromocion
        public async Task<IActionResult> EditarProductoPromocion(int id)
        {
            ProductoPromocion productoPromocion = new ProductoPromocion();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://localhost:7024/api/ProductoPromocion/"+id))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        productoPromocion = JsonConvert.DeserializeObject<ProductoPromocion>(apiResponse);
                    }
                }
            }

            List<Producto> productos = new List<Producto>();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://localhost:7024/api/Producto"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    productos = JsonConvert.DeserializeObject<List<Producto>>(apiResponse);
                }
            }

            List<SelectListItem> productosOps = new List<SelectListItem>();
            foreach (Producto producto in productos)
            {
                if(producto.Id == productoPromocion.IdProducto)
                {
                    productosOps.Add(new SelectListItem
                    {
                        Selected = true,
                        Text = producto.Nombre,
                        Value = producto.Id.ToString()
                    });
                }
                else
                {
                    productosOps.Add(new SelectListItem
                    {
                        Text = producto.Nombre,
                        Value = producto.Id.ToString()
                    });
                }
            };

            ViewData["Productos"] = productosOps;

            List<Promocion> promociones = new List<Promocion>();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://localhost:7024/api/Promocion"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    promociones = JsonConvert.DeserializeObject<List<Promocion>>(apiResponse);
                }
            }

            List<SelectListItem> promocionesOps = new List<SelectListItem>();
            foreach (Promocion promocion in promociones)
            {
                if(promocion.Id == productoPromocion.IdPromocion)
                {
                    promocionesOps.Add(new SelectListItem
                    {
                        Selected = true,
                        Text = promocion.Nombre,
                        Value = promocion.Id.ToString()
                    }) ;
                }
                else
                {
                    promocionesOps.Add(new SelectListItem
                    {
                        Text = promocion.Nombre,
                        Value = promocion.Id.ToString()
                    });
                }
            };

            ViewData["Promociones"] = promocionesOps;
            return View(productoPromocion);
        }

        [HttpPost]
        public async Task<IActionResult> EditarProductoPromocion(ProductoPromocion productoPromocion)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var json = JsonConvert.SerializeObject(productoPromocion);
                    var data = new StringContent(json, Encoding.UTF8, "application/json");
                    using (var httpClient = new HttpClient())
                    {
                        using (var response = await httpClient.PutAsync("https://localhost:7024/api/ProductoPromocion", data))
                        {
                            if (response.IsSuccessStatusCode)
                            {
                                return RedirectToAction(nameof(ProductoPromocion));
                            }
                        }
                    }
                }
                return RedirectToAction(nameof(ProductoPromocion));

            }
            catch
            {
                return RedirectToAction(nameof(ProductoPromocion));
            }
        }

        //Agregar Roles

        public async Task<IActionResult> AgregarRol()
        {                  
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AgregarRol(Rol rol)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var json = JsonConvert.SerializeObject(rol);
                    var data = new StringContent(json, Encoding.UTF8, "application/json");
                    using (var httpClient = new HttpClient())
                    {
                        using (var response = await httpClient.PostAsync("https://localhost:7024/api/Rol", data))
                        {
                            if (response.IsSuccessStatusCode)
                            {
                                return RedirectToAction(nameof(Rol));
                            }
                        }
                    }
                }
                return RedirectToAction(nameof(AgregarRol));

            }
            catch
            {
                return RedirectToAction(nameof(AgregarRol));
            }
        }

        //Editar Roles

        public async Task<IActionResult> EditarRol(int id)
        {
            Rol rol = new Rol();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://localhost:7024/api/Rol/" + id))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    rol = JsonConvert.DeserializeObject<Rol>(apiResponse); //REVISAR NULOS
                }
            }

            return View(rol);
        }

        [HttpPost]
        public async Task<IActionResult> EditarRol(Rol rol)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var json = JsonConvert.SerializeObject(rol);
                    var data = new StringContent(json, Encoding.UTF8, "application/json");
                    using (var httpClient = new HttpClient())
                    {
                        using (var response = await httpClient.PutAsync("https://localhost:7024/api/Rol", data))
                        {
                            if (response.IsSuccessStatusCode)
                            {
                                return RedirectToAction(nameof(Rol));
                            }
                        }
                    }
                }
                return RedirectToAction(nameof(EditarRol));

            }
            catch
            {
                return RedirectToAction(nameof(EditarRol));
            }
        }

        //Agregar TipoProducto

        public async Task<IActionResult> AgregarTipoProducto()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AgregarTipoProducto(TipoProducto tipoProducto)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var json = JsonConvert.SerializeObject(tipoProducto);
                    var data = new StringContent(json, Encoding.UTF8, "application/json");
                    using (var httpClient = new HttpClient())
                    {
                        using (var response = await httpClient.PostAsync("https://localhost:7024/api/TipoProducto", data))
                        {
                            if (response.IsSuccessStatusCode)
                            {
                                return RedirectToAction(nameof(TipoProducto));
                            }
                        }
                    }
                }
                return RedirectToAction(nameof(AgregarTipoProducto));

            }
            catch
            {
                return RedirectToAction(nameof(AgregarTipoProducto));
            }
        }

        //Editar TipoProducto

        public async Task<IActionResult> EditarTipoProducto(int id)
        {
            TipoProducto tipoProducto = new TipoProducto();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://localhost:7024/api/TipoProducto/" + id))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    tipoProducto = JsonConvert.DeserializeObject<TipoProducto>(apiResponse); //REVISAR NULOS
                }
            }

            return View(tipoProducto);
        }

        [HttpPost]
        public async Task<IActionResult> EditarTipoProducto(TipoProducto tipoProducto)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var json = JsonConvert.SerializeObject(tipoProducto);
                    var data = new StringContent(json, Encoding.UTF8, "application/json");
                    using (var httpClient = new HttpClient())
                    {
                        using (var response = await httpClient.PutAsync("https://localhost:7024/api/TipoProducto", data))
                        {
                            if (response.IsSuccessStatusCode)
                            {
                                return RedirectToAction(nameof(TipoProducto));
                            }
                        }
                    }
                }
                return RedirectToAction(nameof(EditarTipoProducto));

            }
            catch
            {
                return RedirectToAction(nameof(EditarTipoProducto));
            }
        }

        //Agregar Usuario

        public async Task<IActionResult> AgregarUsuario()
        {
            List<Rol> roles = new List<Rol>();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://localhost:7024/api/Rol"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    roles = JsonConvert.DeserializeObject<List<Rol>>(apiResponse);
                }
            }

            List<SelectListItem> rolesOps = new List<SelectListItem>();
            foreach (Rol rol in roles)
            {
                rolesOps.Add(new SelectListItem
                {
                    Text = rol.Descripcion,
                    Value = rol.Id.ToString()
                });
            };

            ViewData["Roles"] = rolesOps;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AgregarUsuario(Usuario usuario)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var json = JsonConvert.SerializeObject(usuario);
                    var data = new StringContent(json, Encoding.UTF8, "application/json");
                    using (var httpClient = new HttpClient())
                    {
                        using (var response = await httpClient.PostAsync("https://localhost:7024/api/Usuario", data))
                        {
                            if (response.IsSuccessStatusCode)
                            {
                                return RedirectToAction(nameof(Usuario));
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

        //Editar Usuario

        public async Task<IActionResult> EditarUsuario(int id)
        {
            Usuario usuario = new Usuario();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://localhost:7024/api/Usuario/" + id))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    usuario = JsonConvert.DeserializeObject<Usuario>(apiResponse);
                }
            }

            List<Rol> roles = new List<Rol>();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://localhost:7024/api/Rol"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    roles = JsonConvert.DeserializeObject<List<Rol>>(apiResponse);
                }
            }

            List<SelectListItem> rolesOps = new List<SelectListItem>();
            foreach (Rol rol in roles)
            {
                if (rol.Id == usuario.Id) //REVISAR
                {
                    rolesOps.Add(new SelectListItem
                    {
                        Selected = true,
                        Text = rol.Descripcion,
                        Value = rol.Id.ToString()
                    });
                }
                else
                {
                    rolesOps.Add(new SelectListItem
                    {
                        Text = rol.Descripcion,
                        Value = rol.Id.ToString()
                    });
                }
            };

            ViewData["Roles"] = rolesOps;
            return View(usuario);
        }

        [HttpPost]
        public async Task<IActionResult> EditarUsuario(Usuario usuario)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var json = JsonConvert.SerializeObject(usuario);
                    var data = new StringContent(json, Encoding.UTF8, "application/json");
                    using (var httpClient = new HttpClient())
                    {
                        using (var response = await httpClient.PutAsync("https://localhost:7024/api/Usuario", data))
                        {
                            if (response.IsSuccessStatusCode)
                            {
                                return RedirectToAction(nameof(Usuario));
                            }
                        }
                    }
                }
                return RedirectToAction(nameof(EditarUsuario));

            }
            catch
            {
                return RedirectToAction(nameof(EditarUsuario));
            }
        }

    }
}