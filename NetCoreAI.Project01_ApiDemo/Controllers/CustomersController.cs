using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NetCoreAI.Project01_ApiDemo.Context;
using NetCoreAI.Project01_ApiDemo.Entities;

namespace NetCoreAI.Project01_ApiDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly ApiContext _context;

        public CustomersController(ApiContext context)
        {
            _context = context;
        }

        [HttpGet] //veri listelemede kullanılan istek türü
        public IActionResult CustomerList()
        {
            var value = _context.Customers.ToList();
            return Ok(value);
        }

        [HttpPost] //veri eklemede kullanılan istek türü
        public IActionResult AddCustomer(Customer customer)
        {
            _context.Customers.Add(customer);
            _context.SaveChanges();
            return Ok("Müşteri Ekleme İşlemi Başarılı");
        }

        [HttpDelete] //veri silmede kullanılan istek türü
        public IActionResult DeleteCustomer(int id)
        {
            var values = _context.Customers.Find(id);
            _context.Customers.Remove(values);
            _context.SaveChanges();
            return Ok("Müşteri Başarıyla Silindi.");
        }

        [HttpGet("GetCustomer")] //id'ye göre veri getirmede kullanılan istek türü
        public IActionResult GetCustomer(int id)
        {
            var values = _context.Customers.Find(id);
            return Ok(values);
        }

        [HttpPut] //veri güncellemede kullanılan istek türü
        public IActionResult UpdateCustomer(Customer customer)
        {
             _context.Customers.Update(customer);      
            _context.SaveChanges();
            return Ok("Müşteri Başarıyla Güncellendi");
        }


    }
}
