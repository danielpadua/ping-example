using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace TestApi.Controllers
{
    [ApiController]
    [Route("customers")]
    public class CustomerController : ControllerBase
    {

        private static readonly dynamic[] Customers = new dynamic[]
        {
            new { Id = 1, Name = "Daniel", IsActive = true, IsAdmin = false },
            new { Id = 2, Name = "Humberto", IsActive = false, IsAdmin = false },
            new { Id = 3, Name = "Fabricio", IsActive = true, IsAdmin = false },
            new { Id = 4, Name = "Matheus", IsActive = false, IsAdmin = false },
            new { Id = 5, Name = "Admin", IsActive = true, IsAdmin = true },
        };

        private readonly ILogger<CustomerController> _logger;

        public CustomerController(ILogger<CustomerController> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Can only be called by an admin
        /// </summary>
        /// <returns>list of customers</returns>
        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(Customers);
        }

        /// <summary>
        /// Can only check information of your own profile 
        /// and if the user status is active
        /// </summary>
        /// <param name="id">id of the customer</param>
        /// <returns>customer info</returns>
        [HttpGet("{id}")]
        public IActionResult GetSingle(int id)
        {
            var customer = Customers.FirstOrDefault(c => c.Id == id);
            return Ok(customer);
        }

        /// <summary>
        /// Gets customer status, will only be called by ping datagov
        /// </summary>
        /// <param name="id">id of the customer</param>
        /// <returns>customer status</returns>
        [HttpGet("status/{id}")]
        public IActionResult GetStatus(int id)
        {
            var customer = Customers.FirstOrDefault(c => c.Id == id);
            var status = customer?.IsActive ?? false;
            return Ok(new { status = status });
        }


        /// <summary>
        /// Check whether the customer is an admin
        /// </summary>
        /// <param name="id">id of the customer</param>
        /// <returns>isAdmin if customer is admin</returns>
        [HttpGet("status/{id}")]
        public IActionResult GetIsAdmin(int id)
        {
            var customer = Customers.FirstOrDefault(c => c.Id == id);
            var isAdmin = customer?.IsAdmin ?? false;
            return Ok(new { isAdmin = isAdmin });
        }

    }
}
