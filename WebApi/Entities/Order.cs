using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Entities;

public class Order
{
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; } 
        public int MovieId { get; set; }
        public Movie Movie { get; set; }
        
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
        public decimal Price { get; set; }
        public DateTime? PurchasedDate { get; set; }
         
}
