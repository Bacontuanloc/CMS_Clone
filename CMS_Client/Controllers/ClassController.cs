using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Text;
using CMS_Client.Entities;

namespace CMS_Client.Controllers
{
    public class ClassController : Controller
    {
        private readonly HttpClient client = null;
        private string ClassApiUrl = "";

        public ClassController()
        {
            client = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
            ClassApiUrl = "https://localhost:7158/api/Class";
        }

        public async Task<IActionResult> Index()
        {
            HttpResponseMessage response = await client.GetAsync(ClassApiUrl);
            string strData = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };
            List<Class> listClasses = JsonSerializer.Deserialize<List<Class>>(strData, options);
            return View(listClasses);
        }

        //public async Task<IActionResult> Details(int id)
        //{
        //    ClassApiUrl = $"https://localhost:7158/api/Class/{id}";
        //    HttpResponseMessage response = await client.GetAsync(ClassApiUrl);
        //    string strData = await response.Content.ReadAsStringAsync();

        //    var options = new JsonSerializerOptions
        //    {
        //        PropertyNameCaseInsensitive = true
        //    };
        //    Class c = JsonSerializer.Deserialize<Class>(strData, options);
        //    return View(c);
        //}

        //public async Task<IActionResult> Create()
        //{
        //    //CategoryApiUrl = $"http://localhost:5099/api/Category";
        //    //HttpResponseMessage response = await client.GetAsync(CategoryApiUrl);
        //    //string strData = await response.Content.ReadAsStringAsync();

        //    //var options = new JsonSerializerOptions
        //    //{
        //    //    PropertyNameCaseInsensitive = true
        //    //};
        //    //List<Category> category = JsonSerializer.Deserialize<List<Category>>(strData, options);
        //    return View();
        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create(Class cl)
        //{
        //    cl.ClassId = 0;
        //    var payload = JsonSerializer.Serialize(cl);

        //    HttpContent c = new StringContent(payload, Encoding.UTF8, "application/json");

        //    HttpResponseMessage response = await client.PostAsync(ClassApiUrl, c);
        //    string strData = await response.Content.ReadAsStringAsync();


        //    //CategoryApiUrl = $"http://localhost:5099/api/Category";
        //    //HttpResponseMessage responseCate = await client.GetAsync(CategoryApiUrl);
        //    //string strDataCate = await responseCate.Content.ReadAsStringAsync();

        //    //var options = new JsonSerializerOptions
        //    //{
        //    //    PropertyNameCaseInsensitive = true
        //    //};
        //    //List<Category> category = JsonSerializer.Deserialize<List<Category>>(strDataCate, options);

        //    //return View(category);
        //    return RedirectToAction("Index", "Class");
        //}

        //public async Task<ActionResult> EditAsync(int id)
        //{
        //    ClassApiUrl = $"http://localhost:5099/api/Product/{id}";
        //    HttpResponseMessage response = await client.GetAsync(ClassApiUrl);
        //    string strData = await response.Content.ReadAsStringAsync();

        //    var options = new JsonSerializerOptions
        //    {
        //        PropertyNameCaseInsensitive = true
        //    };
        //    Class c = JsonSerializer.Deserialize<Class>(strData, options);
        //    return View(c);
        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(IFormCollection collection)
        //{
        //    Class c = new Class();
        //    c.ClassId = Int32.Parse(collection["ClassId"]);
        //    c.ClassCode = collection["ClassCode"];
        //    c.Description = collection["Description"];
        //    p.UnitsInStock = Int32.Parse(collection["UnitsInStock"]);
        //    p.UnitPrice = Decimal.Parse(collection["UnitPrice"]);

        //    var payload = JsonSerializer.Serialize(p);

        //    HttpContent c = new StringContent(payload, Encoding.UTF8, "application/json");

        //    HttpResponseMessage response = await client.PutAsync(ClassApiUrl, c);
        //    string strData = await response.Content.ReadAsStringAsync();

        //    ClassApiUrl = $"http://localhost:5099/api/Product/{p.ProductId}";
        //    response = await client.GetAsync(ClassApiUrl);
        //    strData = await response.Content.ReadAsStringAsync();

        //    var options = new JsonSerializerOptions
        //    {
        //        PropertyNameCaseInsensitive = true
        //    };
        //    Product product = JsonSerializer.Deserialize<Product>(strData, options);
        //    return View(product);
        //}

        //[HttpGet]
        //public async Task<IActionResult> DeleteAsync(int id)
        //{
        //    ClassApiUrl = $"http://localhost:5099/api/Product/{id}";
        //    HttpResponseMessage response = await client.DeleteAsync(ClassApiUrl);
        //    string strData = await response.Content.ReadAsStringAsync();

        //    return RedirectToAction("Index", "Product");
        //}
    }
}
