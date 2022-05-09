using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Entities;

public class Actor : User
{
    public ICollection<MovieOfActors> MovieOfActors { get; set; }
   
    
}    