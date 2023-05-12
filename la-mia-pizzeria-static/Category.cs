using System.ComponentModel.DataAnnotations;

namespace la_mia_pizzeria_static
{
    public class Category
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [Range(5, 100)]
        public string? Description { get; set; }

        public List<Category> Categories { get; set; }
        
    }
}
