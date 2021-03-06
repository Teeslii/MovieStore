using Microsoft.EntityFrameworkCore;

namespace WebApi.Entities
{
    [Keyless]
    public class CustomerFavoriteGenre
    {
        public int GenreId { get; set; }
        public Genre Genre { get; set; }
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
    }
}