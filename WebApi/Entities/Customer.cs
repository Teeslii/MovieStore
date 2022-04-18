using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Entities;

public class Customer
{
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; } 
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public string NameSurname { get=> $"{Name} {Surname}"; }
        public string? Email { get; set; }
        public string Password { get; set; }
        public ICollection<Order> OrderMovies { get; set; }
         public ICollection<Genre> FavoriteGenres { get; set; }
        public bool IsActive { get; set; } = true;

}
