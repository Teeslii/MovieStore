using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Entities
{
    public class User
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; } 
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public string NameSurname { get=> $"{Name} {Surname}"; }
         public bool IsActive { get; set; } = true;
    }
}