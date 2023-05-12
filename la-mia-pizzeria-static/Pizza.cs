using System.ComponentModel.DataAnnotations;

namespace la_mia_pizzeria_static
{
    //VERSIONE CON DB
    public class Pizza
    {   //attributi = colonne

        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Il campo è obbligatorio.")]
        public string Name { get; set; }

        [Range(5, 1000, ErrorMessage = "Il campo deve rientare nel range di 5-1000 caratteri.")]
        public string? Description { get; set; }

        [StringLength(1000, ErrorMessage = "Il campo deve avere max 1000 caratteri.")]
        public string? Image { get; set; }

        [Required(ErrorMessage = "Il campo è obbligatorio.")]
        [Range(0, int.MaxValue, ErrorMessage = "Il campo non può avere numeri negativi.")]
        public int Price { get; set; }

        //relazione uno a molti
        public int? CategoryId { get; set; }
        public Category? Category { get; set; }

        //relazione molti a molti
        public List<Ingredient>? Ingredients { get; set; }


        //costruttore vuoto per permettere la creazione tramite form di nuovi oggetti Pizza
        public Pizza() { }
    }
}

















    //VERSIONE SENZA DB

    //public class Pizza
    //{
    //    public int Id { get; set; }

    //    private static int increment = 7; //si parte da 7 perché ho già inserito manualmente 6 istanze (e quindi 6 id)

    //    [Required(ErrorMessage = "Il campo è obbligatorio.")]
    //    [Range(2, 100, ErrorMessage = "Il campo deve rientare nel range di 2-100 caratteri.")]
    //    public string Name { get; set; }

    //    //[MoreFiveWords]
    //    [Range(5, 1000, ErrorMessage = "Il campo deve rientare nel range di 5-1000 caratteri.")]
    //    public string? Description { get; set; }

    //    [StringLength(200, ErrorMessage = "Il campo deve rientare nel range di 5-500 caratteri.")]
    //    public string? Image { get; set; }

    //    [Required(ErrorMessage = "Il campo è obbligatorio.")]
    //    [Range(0, int.MaxValue, ErrorMessage = "Il campo non può avere numeri negativi.")]
    //    public int Price { get; set; }

    //    public string Category { get; set; }


    //    //costruttore vuoto per permettere la creazione tramite form di nuovi oggetti Pizza
    //    public Pizza()
    //    {
    //        Id = increment;
    //        increment++; //questo serve ad incrementare di 1 l'Id ad ogni creazione di un nuovo oggetto

    //    } 

    //    public Pizza(int id, string name, string? description, string? image, int price, string category)
    //    {
    //        Id = id;
    //        Name = name;
    //        Description = description;
    //        Image = image;
    //        Price = price;
    //        Category = category;
    //    }


    //    public static List<Pizza> pizzas = new()
    //    {
    //        new (1, "Marinara", "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nullam mattis, nunc ut varius pharetra, enim ipsum euismod ante, a sodales mauris dui nec lectus. Pellentesque ut justo vel nulla sagittis imperdiet non non risus. Nam et sollicitudin orci. Sed finibus eros ac bibendum mollis. ", "https://th.bing.com/th/id/R.2b81070d5bf07de3fc737e11828ec9fa?rik=HyW%2bu6OVuIEfWw&pid=ImgRaw&r=0", 4, "Rossa"),
    //        new (2, "Margherita", "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nullam mattis, nunc ut varius pharetra, enim ipsum euismod ante, a sodales mauris dui nec lectus. Pellentesque ut justo vel nulla sagittis imperdiet non non risus. Nam et sollicitudin orci. Sed finibus eros ac bibendum mollis. ", "https://cdn.vox-cdn.com/thumbor/-8vtkFItJQ85nEafqXxZ_oYz694=/0x0:4500x3000/1200x800/filters:focal(1890x1140:2610x1860)/cdn.vox-cdn.com/uploads/chorus_image/image/60380693/20180424_GH_UNAPIZZA_1012.0.jpg", 5, "Rossa"),
    //        new (3, "Filetto", "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nullam mattis, nunc ut varius pharetra, enim ipsum euismod ante, a sodales mauris dui nec lectus. Pellentesque ut justo vel nulla sagittis imperdiet non non risus. Nam et sollicitudin orci. Sed finibus eros ac bibendum mollis. ", "https://th.bing.com/th/id/OIP.b8GPIG9ud5VziECz8Q9L7AHaE8?pid=ImgDet&rs=1", 7, "Bianca"),
    //        new (4, "Capricciosa", "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nullam mattis, nunc ut varius pharetra, enim ipsum euismod ante, a sodales mauris dui nec lectus. Pellentesque ut justo vel nulla sagittis imperdiet non non risus. Nam et sollicitudin orci. Sed finibus eros ac bibendum mollis. ", "https://th.bing.com/th/id/R.983f28b64fa9d565d7fd63bf557b6b23?rik=xYG9aRt7az1v2g&pid=ImgRaw&r=0", 7, "Rossa"),
    //        new (5, "Bufala", "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nullam mattis, nunc ut varius pharetra, enim ipsum euismod ante, a sodales mauris dui nec lectus. Pellentesque ut justo vel nulla sagittis imperdiet non non risus. Nam et sollicitudin orci. Sed finibus eros ac bibendum mollis. ", "https://th.bing.com/th/id/R.d026806ae7b9b122e68f1b28b22dba7b?rik=Rm%2bw5ToaHvSvKw&riu=http%3a%2f%2fwww.italianconceptrestaurant.com%2fwp-content%2fuploads%2f2020%2f06%2fbufala.jpg&ehk=11iv1pKIcWHQiV8m4lr2OHb1eunZyiKob%2fvJrcfdst8%3d&risl=&pid=ImgRaw&r=0", 8, "Rossa"),
    //        new (6, "Ortolana", "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nullam mattis, nunc ut varius pharetra, enim ipsum euismod ante, a sodales mauris dui nec lectus. Pellentesque ut justo vel nulla sagittis imperdiet non non risus. Nam et sollicitudin orci. Sed finibus eros ac bibendum mollis. ", "https://th.bing.com/th/id/R.711776b78a0f958f0dcb7d202ace90c9?rik=p2m7q8Z3oyU0lw&pid=ImgRaw&r=0", 8, "Bianca"),
    //    };

    //}


    ////classe che estende ValidationAttribute per creare una validazione personalizzata
    //public class MoreFiveWordsAttribute : ValidationAttribute
    //{
    //    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    //    {
    //        string fieldValue = (string) value;

    //        if (fieldValue == null || fieldValue.Trim().Split(' ').Length < 5); 
    //        {
    //            return new ValidationResult("Il campo deve contenere almeno 5 parole.");
    //        }

    //        return ValidationResult.Success;
    //    }
    //}
//}
