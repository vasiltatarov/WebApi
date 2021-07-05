using System.ComponentModel.DataAnnotations;

namespace WebApi.Data.Models
{
    public class Book
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Author { get; set; }

        public int Year { get; set; }

        public double IMDB { get; set; }
    }
}
