using la_mia_pizzeria_static.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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


        ////PUT
        //[HttpPut("{id}")]
        //public IActionResult Update(int id, [FromBody] Pizza data)
        //{
        //    using (PizzaContext context = new PizzaContext())
        //    {
        //        // cerchiamo il dato
        //        PizzaContext pizzaEdit = context.PizzaContexts
        //        .Where(pizza => pizza.Id == id).FirstOrDefault();
        //        if (pizzaEdit != null)
        //        {
        //            //---


        //            return Ok();
        //        }
        //        else
        //        {
        //            // se non è stato trovato resituiamo che non esiste
        //            return NotFound();
        //        }
        //    }
        //}
    }
}
