using Microsoft.AspNetCore.Mvc;
using NetCoreAI.Project02_ApiConsumeUI.Dtos;
using Newtonsoft.Json;


namespace NetCoreAI.Project02_ApiConsumeUI.Controllers
{
    public class CustomerController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory; 

        public CustomerController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> Index()
        {
            var client = _httpClientFactory.CreateClient();//istemci oluştur
            var responseMessage = await client.GetAsync("https://localhost:7013/api/Customers");//API'ye GET isteği gönder
            if (responseMessage.IsSuccessStatusCode)//response başarılı ise
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();//response message içeriğini oku, jsonData içine ata.
                var values=JsonConvert.DeserializeObject<List<ResultCustomerDto>>(jsonData);//gelen json verisini normal stringe dönştür.ResultCustomerDto  ile eşleştir.
                return View(values);//verileri View'a gönder

            }
            return View();
        }
    }
}
