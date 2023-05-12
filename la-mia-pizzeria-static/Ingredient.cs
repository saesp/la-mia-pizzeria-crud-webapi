using System.ComponentModel.DataAnnotations;

namespace la_mia_pizzeria_static
{
    public class Ingredient
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string? Description { get; set; }

        public List<Pizza>? Pizzas { get; set; }


        public Ingredient() { }
    }
}
