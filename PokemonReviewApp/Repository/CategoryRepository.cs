using AutoMapper.Configuration.Conventions;
using PokemonReviewApp.Data;
using PokemonReviewApp.Interfaces;
using PokemonReviewApp.Models;

namespace PokemonReviewApp.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly DataContext _context;

        public CategoryRepository(DataContext context)
        {
            _context = context;
        }
        public bool CategoryExist(int id)
        {
            //Any() is just going to return a bool value.
            return _context.Categories.Any(c => c.Id == id);
        }

        public bool CreateCategory(Category category)
        {
            //Change Tracker
            //In adding their can be following states:
            //add, updating, modifying
            //connected vs disconnected
            //EntityState.Added =>this is disconnected state
            _context.Add(category);
            return Save();
        }
        public bool UpdateCategory(Category category)
        {
            _context.Update(category);
            return Save();
        }

        public bool DeleteCategory(Category category)
        {
            _context.Remove(category);
            return Save();
        }

        public ICollection<Category> GetCategories()
        {
            return _context.Categories.ToList();
        }

        public Category GetCategory(int id)
        {
            return _context.Categories.Where(e=>e.Id==id).FirstOrDefault();
        }

        public ICollection<Pokemon> GetPokemonByCategory(int categoryId)
        {
            return _context.PokemanCategories
                .Where(e => e.CategoryId == categoryId)
                .Select(c => c.Pokeman).ToList();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges(); //savechanges generated the sql and send to the server.
            return saved > 0 ? true : false;
        }

        
    }
}
