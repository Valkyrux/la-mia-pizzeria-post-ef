using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace la_mia_pizzeria_ef.Models
{
    [Table("pizze")]
    public class Pizza
    {
        [Key]
        [Required(ErrorMessage = "Id richiesto")]
        public int Id { get; set; }
        [Required(ErrorMessage = "Nome richiesto")]
        public string Name { get; set; }
        public string? Description { get; set; }

        public string? ImgPath { get; set; }
        [Required(ErrorMessage = "Prezzo richiesto")]
        public double Price { get; set; }

        [NotMapped()]
        public IFormFile? formFile { get; set; }
        
        public Pizza(string nome, string? description, string? imgPath, double prezzo)
        {

            Name = nome;
            Description = description;
            ImgPath = imgPath;
            Price = prezzo;
        }

        public Pizza() { }

        public string getPriceToString()
        {
            double price = (double)Price;
            return price.ToString("0.00").Replace('.', ',');

        }
    }
}
