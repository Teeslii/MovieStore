using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Entities;

public class Director
{
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; } 
        public string? Name { get; set; }
        public string? Surname { get; set; }

        public string NameSurname { get=> $"{Name} {Surname}"; }
        public List<Movie> Movies { get; set; }
}
