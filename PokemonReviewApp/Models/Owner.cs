namespace PokemonReviewApp.Models
{
    public class Owner
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gym { get; set; }
        public Country Country { get; set; } //this is one relation it always going to take one country.
        public ICollection<PokemanOwner> PokemanOwners { get; set; }
    }
}
