namespace PokemonReviewApp.Models
{
    public class PokemanOwner
    {
        public int PokemanId { get; set; }
        public int OwnerId { get; set; }
        public Pokemon Pokemon { get; set; }
        public Owner Owner { get; set; }
    }
}
