

namespace WebApi.Entities;

public class Customer : User
{
        public string? Email { get; set; }
        public string Password { get; set; }
        public string? RefreshToken { get; set; }
        public DateTime? RefresTokenExpireDate { get; set; }
        public ICollection<Order> Orders { get; set; }
        public ICollection<CustomerFavoritGenre> CustomerFavoritGenres { get; set; }

      
}
