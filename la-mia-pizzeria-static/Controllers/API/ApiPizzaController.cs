using la_mia_pizzeria_static.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace la_mia_pizzeria_static.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApiPizzaController : ControllerBase //ControllerBase offre metodi e proprietà utili per la gestione delle richieste e risposte HTTP 
    { 
        //GET
        [HttpGet]
        public IActionResult Index()
        {
            using (PizzaContext context = new PizzaContext())
            {
                IQueryable<Pizza> pizzas = context.Pizzas;

                return Ok(pizzas.ToList());
            }
        }
    }
}
