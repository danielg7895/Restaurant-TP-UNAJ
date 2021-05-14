using Domain.Commands;
using System.Collections.Generic;

namespace AccessData.Commands
{
    public class GenericRepository : IGenericRepository
    {

        private readonly RestaurantContext _context;

        public GenericRepository(RestaurantContext context)
        {
            _context = context;
        }

        public void Add<T>(T entity) where T : class
        {
            _context.Add(entity);
            _context.SaveChanges();
        }

        public void AddList<T>(List<T> entityList) where T : class {
            entityList.ForEach(entity =>
            {
                Add(entity);
            });
        }

        public void Remove<T>(T entity) where T : class
        {
            _context.Remove(entity);
            _context.SaveChanges();
        }

        public void Update<T>(T entity) where T : class
        {
            _context.Update(entity);
            _context.SaveChanges();
        }

    }
}
