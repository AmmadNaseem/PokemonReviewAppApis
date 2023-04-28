using PokemonReviewApp.Data;
using PokemonReviewApp.Interfaces;
using PokemonReviewApp.Models;
using System.Xml.Linq;

namespace PokemonReviewApp.Repository
{
    public class PokemonRepository:IPokemonRepository
    {
        private readonly DataContext _context; //through datacontext i can bring any table of database.

        public PokemonRepository(DataContext context)
        {
            _context = context;
        }

        public bool CreatePokemon(int ownerId, int categoryId, Pokemon pokemon)
        {
           var pokemonOwnerEntity=_context.Owners.Where(a => a.Id == ownerId).FirstOrDefault();
           var category=_context.Categories.Where(a => a.Id ==categoryId).FirstOrDefault();

            var pokemonOwner = new PokemanOwner()
            {
                Owner = pokemonOwnerEntity,
                Pokemon = pokemon,
            };

            _context.Add(pokemonOwner);

            var pokemonCategory = new PokemanCategory()
            {
                Category=category,
                Pokeman=pokemon,
            };

            _context.Add(pokemonCategory);

            _context.Add(pokemon);

            return Save();

        }

        public bool DeletePokemon(Pokemon pokemon)
        {
            _context.Remove(pokemon);
            return Save();
        }

        public Pokemon GetPokemon(int id)
        {
            //GetPokemon is our details page.This will return one record.
            return _context.Pokemons.Where(p => p.Id == id).FirstOrDefault();
        }

        public Pokemon GetPokemon(string name)
        {
            return _context.Pokemons.Where(p => p.Name == name).FirstOrDefault();
        }

        public decimal GetPokemonRating(int pokeId)
        {
            var reviews= _context.Reviews.Where(p => p.Pokemon.Id==pokeId);
            if (reviews.Count()<=0)
            {
                return 0;
            }
            //first we sum the rating next we average of the rating.
            return ((decimal)reviews.Sum(r => r.Rating)/reviews.Count());
        }


        public ICollection<Pokemon> GetPokemons()
        {
            //Icollection can't be edited it can only shown. this will return list
            return _context.Pokemons.OrderBy(p => p.Id).ToList();
        }

        public bool PokemonExists(int pokeId)
        {
            //if we checking the something exist then we use any.
            return _context.Pokemons.Any(p => p.Id == pokeId);
        }    

        public bool UpdatePokemon(int ownerId, int categoryId, Pokemon pokemon)
        {
            _context.Update(pokemon);
            return Save();
        }
        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }
    }
}
