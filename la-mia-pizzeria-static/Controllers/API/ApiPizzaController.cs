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
        public IActionResult Create([FromBody] PizzaFormModel data)
        {
            using (PizzaContext context = new PizzaContext())
            {
                Pizza pizzaCreate = new Pizza();
                pizzaCreate.Name = data.Pizza.Name;
                pizzaCreate.Description = data.Pizza.Description;
                pizzaCreate.Image = data.Pizza.Image;
                pizzaCreate.Price = data.Pizza.Price;
                pizzaCreate.CategoryId = data.Pizza.CategoryId; //one to many
                pizzaCreate.Ingredients = new List<Ingredient>(); //many to many
                if (data.SelectIngredients != null)
                {
                    foreach (string selectedIngredientId in data.SelectIngredients) //per ogni Id di ingredienti selezionati
                    {
                        int selectedIntIngredientId = int.Parse(selectedIngredientId); //Parse() converte da un tipo all'altro (in questo caso l'Id string (messo da View) a Id int
                        Ingredient ingredient = context.Ingredients.Where(m => m.Id == selectedIntIngredientId).FirstOrDefault(); //ricerca e selezione dell'Id model corrispondente all'Id view 

                        pizzaCreate.Ingredients.Add(ingredient); //aggiunta dell'Id selezionato alla lista Ingredients
                    }
                }

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


        //DELETE
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            using (PizzaContext context = new PizzaContext())
            {
                Pizza pizzaDelete = context.Pizzas
                .Where(p => p.Id == id).FirstOrDefault();
                if (pizzaDelete != null)
                {
                    context.Pizzas.Remove(pizzaDelete);

                    context.SaveChanges();

                    return Ok();
                }
                else
                {
                    return NotFound();
                }
            }
        }
    }
}
