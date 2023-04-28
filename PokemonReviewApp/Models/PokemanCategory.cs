namespace PokemonReviewApp.Models
{
    public class PokemanCategory
    {
        public int PokemanId { get; set; }
        public int CategoryId { get; set; }
        public Pokemon Pokeman { get; set; }
        public Category Category { get; set; }
    }
}
