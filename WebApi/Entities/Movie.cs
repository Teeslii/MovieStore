using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Entities;

public class Movie
{
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Title { get; set; } 
        public int GenreId { get; set; }
        public Genre Genre { get; set; }
        public int DirectorId { get; set; }
        public Director Director { get; set; }
        public ICollection<MovieOfActors> MovieOfActors { get; set; }   
        
        public decimal Price { get; set; }
        public int ReleaseYear { get; set; }
        public bool IsActive { get; set; } = true;
}
