using la_mia_pizzeria_static.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace la_mia_pizzeria_static.Controllers.API
{ 
    [Route("api/[controller]/[action]")]
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


        //POST
        [HttpPost]
        public IActionResult Create([FromBody] Pizza data)
        {
            using (PizzaContext context = new PizzaContext())
            {
                Pizza pizzaCreate = new Pizza();
                pizzaCreate.Name = data.Name;
                pizzaCreate.Description = data.Description;
                pizzaCreate.Image = data.Image;
                pizzaCreate.Price = data.Price;
                pizzaCreate.CategoryId = data.CategoryId; //one to many
                
                context.Pizzas.Add(pizzaCreate);
                context.SaveChanges();


                return Ok();
            }
        }


        //PUT
        [HttpPut("{id}")]
        public IActionResult Edit(int id, [FromBody] Pizza data)
        {
            using (PizzaContext context = new PizzaContext())
            {
                var pizzaEdit = context.Pizzas.Where(p => p.Id == id).Include(m => m.Ingredients).FirstOrDefault();

                if (pizzaEdit != null)
                {
                    pizzaEdit.Name = data.Name;
                    pizzaEdit.Description = data.Description;
                    pizzaEdit.Image = data.Image;
                    pizzaEdit.Price = data.Price;
                    pizzaEdit.CategoryId = data.CategoryId;

                    context.SaveChanges();

                    return Ok();
                }
                else
                {
                    // se non è stato trovato resituiamo che non esiste
                    return NotFound();
                }
            }
        }
    }
}
