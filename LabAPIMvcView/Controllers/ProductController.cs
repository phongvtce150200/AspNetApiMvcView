using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;
using LabAPIMvcView.Models;
using Microsoft.AspNetCore.Mvc;
//using LabAPIMvcView.Models;

namespace LabAPIMvcView.Controllers
{
    public class ProductController : Controller
    {
        private readonly HttpClient client = null;
        private string api;
        public ProductController()
        {
            client = new HttpClient();
            var contenType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contenType);
            this.api = "https://localhost:5001/api/Product";
        }

        public async Task<IActionResult> Index()
        {
            HttpResponseMessage respone = await client.GetAsync(api);
            string data = await respone.Content.ReadAsStringAsync();

            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

            List<Product> list = JsonSerializer.Deserialize<List<Product>>(data, options);

            return View(list);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Product obj)
        {
            if (ModelState.IsValid)
            {
                string data = JsonSerializer.Serialize(obj);
                var content = new StringContent(data, System.Text.Encoding.UTF8, "application/json");
                HttpResponseMessage respone = await client.PostAsync(api, content);
                if (respone.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    return RedirectToAction("Index");
                }
                return RedirectToAction("Index");
            }
            return View(obj);
        }
        public async Task<IActionResult> Edit(int id)
        {
            HttpResponseMessage respone = await client.GetAsync(api + "/" + id);
            if (respone.IsSuccessStatusCode)
            {
                var data = respone.Content.ReadAsStringAsync().Result;
                var product = JsonSerializer.Deserialize<Product>(data);
                return View(product);
            }
            return NotFound();
        }
        [HttpPost]
        public async Task<IActionResult> Edit(int id, Product obj)
        {
            obj.productId = id;
            string data = JsonSerializer.Serialize(obj);
            var content = new StringContent(data, System.Text.Encoding.UTF8, "application/json");
            HttpResponseMessage respone = await client.PutAsync(api+"/"+id, content);
            if(respone.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return Redirect("Home/Error");
        }

        public async Task<IActionResult> Delete(int id)
        {
            HttpResponseMessage respone = await client.GetAsync(api + "/" + id);
            if (respone.IsSuccessStatusCode)
            {
                var data = respone.Content.ReadAsStringAsync().Result;
                Product product = JsonSerializer.Deserialize<Product>(data);
                return View(product);
            }
            return NotFound();
        }
        [HttpPost]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            HttpResponseMessage respone = await client.DeleteAsync(api + "/" + id);
            if (respone.IsSuccessStatusCode)
            {          
                return RedirectToAction("Index");
            }
            return NotFound();
        }
    }
}