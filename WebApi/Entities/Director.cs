using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Entities;

public class Director : User
{
     
        public List<Movie> Movies { get; set; }
}
