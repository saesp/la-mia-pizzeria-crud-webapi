using la_mia_pizzeria_static.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Linq;
using System.Runtime.ConstrainedExecution;

namespace la_mia_pizzeria_static.Controllers
{
    public class PizzaController : Controller
    {
        private IEnumerable<Ingredient> ingredients;

        //se non inserisco [Http...] ci sarà di deafault [HttpGet]
        public IActionResult Index()
        {
            using (PizzeriaContext context = new PizzeriaContext())
            {
                var pizzas = context.Pizzas.ToList();

                return View(pizzas);
            }
        }


        // CRUD

        //Create
        [Authorize(Roles = "ADMIN")] //[Autorize] protegge l'action dai non autorizzati, Roles specifica quali ruoli hanno gli utenti autenticati per accedere alle varie actions
        [HttpGet]
        public IActionResult Create()
        {
            using (PizzeriaContext context = new PizzeriaContext())
            {
                List<Category> categories = context.Categories.ToList();
                List<Ingredient> ingredients = context.Ingredients.ToList();

                //creazione model da passare alla pagina get
                PizzaFormModel model = new PizzaFormModel();
                model.Pizza = new Pizza();
                model.Categories = categories;
                // many to many
                List<SelectListItem> listIngredients = new();
                foreach(Ingredient ingredient in ingredients)
                {
                    listIngredients.Add(
                        new SelectListItem()
                        {
                            Text = ingredient.Name,
                            Value = ingredient.Id.ToString()
                        }
                    ); 
                }
                model.Ingredients = listIngredients; 

                return View("Create", model);
            }
        }

        [Authorize(Roles = "ADMIN")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(PizzaFormModel data)
        {
            if (!ModelState.IsValid)
            {
                return View("Create", data);
            }
            using (PizzeriaContext context = new PizzeriaContext())
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
                    foreach (string selectedIngredientId in data.SelectIngredients) 
                    {
                        int selectedIntIngredientId = int.Parse(selectedIngredientId); //Parse() converte da un tipo all'altro
                        Ingredient ingredient = context.Ingredients.Where(m => m.Id == selectedIntIngredientId).FirstOrDefault(); 

                        pizzaCreate.Ingredients.Add(ingredient);
                    }
                }
                context.Pizzas.Add(pizzaCreate);
                context.SaveChanges();

                return RedirectToAction("Index");
            }
        }


        //Read
        [Authorize(Roles = "ADMIN,USER")]
        [HttpGet]
        public IActionResult View(int id)
        {
            using (PizzeriaContext context = new PizzeriaContext())
            {
                Pizza pizza = context.Pizzas.Where(p => p.Id == id).Include(p => p.Category).Include(m => m.Ingredients).FirstOrDefault(); //metodo Include() per recuperare Category e Ingredients assieme a Pizza

                return View(pizza);
            }
        }


        //Update
        [Authorize(Roles = "ADMIN")]
        [HttpGet]
        public IActionResult Edit(int id)
        {
            using (PizzeriaContext context = new PizzeriaContext())
            {
                //passo i dati per avere i valori degli attributi nella pagina Edit
                var pizzaEdit = context.Pizzas.Include(m => m.Ingredients).FirstOrDefault(p => p.Id == id);

                List<Category> categories = context.Categories.ToList();
                List<Ingredient> ingredients = context.Ingredients.ToList();

                //creazione model da passare alla pagina get
                PizzaFormModel model = new PizzaFormModel();

                model.Pizza = pizzaEdit;
                model.Categories = categories;
                List<SelectListItem> listIngredients = new(); // many to many
                foreach (Ingredient ingredient in ingredients)
                {
                    listIngredients.Add(
                        new SelectListItem()
                        {
                            Text = ingredient.Name,
                            Value = ingredient.Id.ToString(),
                            Selected = pizzaEdit.Ingredients.Any(m=>m.Id == ingredient.Id) //Any() restituisce un valore booleano che indica se l'enumerazione soddisfa una determinata condizione
                        }
                    );
                }
                model.Ingredients = listIngredients;


                return View("Edit", model);
            }
        }

        [Authorize(Roles = "ADMIN")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, PizzaFormModel data)
        {
            if (!ModelState.IsValid)
            {
                return View("Edit", data);
            }
            using (PizzeriaContext context = new PizzeriaContext())
            {
                var pizzaEdit = context.Pizzas.Where(p => p.Id == id).Include(m=>m.Ingredients).FirstOrDefault(); //FirstOrDefaut prende l'id corrispondente e se non lo trova darà null

                if (pizzaEdit != null)
                {
                    pizzaEdit.Name = data.Pizza.Name;
                    pizzaEdit.Description = data.Pizza.Description;
                    pizzaEdit.Image = data.Pizza.Image;
                    pizzaEdit.Price = data.Pizza.Price;
                    pizzaEdit.CategoryId = data.Pizza.CategoryId;
                    if (data.SelectIngredients != null) //many to many
                    {
                        foreach (string selectedIngredientId in data.SelectIngredients)
                        {
                            int selectedIntIngredientId = int.Parse(selectedIngredientId);
                            Ingredient ingredient = context.Ingredients.Where(m => m.Id == selectedIntIngredientId).FirstOrDefault();

                            pizzaEdit.Ingredients.Add(ingredient);
                        }
                    }

                    context.SaveChanges();

                    return RedirectToAction("Index");
                }
                else
                {
                    return NotFound(); //restituisce errore 404
                }
            }
        }


        //Delete
        [Authorize(Roles = "ADMIN")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            using (PizzeriaContext context = new PizzeriaContext())
            {
                var pizzaDelete = context.Pizzas.Where(p => p.Id == id).FirstOrDefault();

                if (pizzaDelete != null)
                {
                    context.Pizzas.Remove(pizzaDelete);
                    context.SaveChanges();

                    return RedirectToAction("Index");
                }
                else
                {
                    return NotFound();
                }
            }
        }








        //public class PizzaController : Controller
        //{
        //    public IActionResult Index()
        //    {
        //        return View(Pizza.pizzas);
        //    }

        //    // CRUD

        //    //Create
        //    [HttpGet]
        //    public IActionResult Create()
        //    {
        //        return View();
        //    }

        //    [HttpPost]
        //    [ValidateAntiForgeryToken]
        //    public IActionResult Create(Pizza data)
        //    {
        //        if (!ModelState.IsValid)
        //        {
        //            return View("Create", data);
        //        }

        //        Pizza pizzaCreate = new Pizza();
        //        pizzaCreate.Name = data.Name;
        //        pizzaCreate.Description = data.Description;
        //        pizzaCreate.Image = data.Image;
        //        pizzaCreate.Price = data.Price;
        //        pizzaCreate.Category = data.Category;

        //        Pizza.pizzas.Add(data);

        //        return RedirectToAction("Index");
        //    }


        //    //Read
        //    [HttpGet] //se non inserisco [Http...] ci sarà di deafault [HttpGet]
        //    public IActionResult View(int id)
        //    {
        //        var pizza = Pizza.pizzas.FirstOrDefault(p => p.Id == id);

        //        return View(pizza);
        //    }


        //    //Update
        //    [HttpGet]
        //    public IActionResult Edit(int id)
        //    {
        //        //passo i dati per avere i valori degli attributi nella pagina Edit
        //        var pizza = Pizza.pizzas.FirstOrDefault(p => p.Id == id);

        //        return View(pizza);
        //    }

        //    [HttpPost]
        //    [ValidateAntiForgeryToken]
        //    public IActionResult Edit(int id, Pizza data)
        //    {
        //        var pizzaEdit = Pizza.pizzas.Where(p => p.Id == id).FirstOrDefault(); //FirstOrDefaut prendel'id corrispondente e se non lo trova darà null

        //        if (pizzaEdit != null)
        //        {
        //            pizzaEdit.Name = data.Name;
        //            pizzaEdit.Description = data.Description;
        //            pizzaEdit.Image = data.Image;
        //            pizzaEdit.Price = data.Price;
        //            pizzaEdit.Category = data.Category;

        //            return RedirectToAction("Index");
        //        }
        //        else
        //        {
        //            return NotFound(); //restituisce errore 404
        //        }
        //    }


        //    //Delete
        //    [HttpPost]
        //    [ValidateAntiForgeryToken]
        //    public IActionResult Delete(int id)
        //    {
        //        var pizzaDelete = Pizza.pizzas.Where(p => p.Id == id).FirstOrDefault();

        //        if (pizzaDelete != null)
        //        {
        //            Pizza.pizzas.Remove(pizzaDelete);

        //            return RedirectToAction("Index");
        //        }
        //        else
        //        {
        //            return NotFound();
        //        }
        //    }
    }
}
