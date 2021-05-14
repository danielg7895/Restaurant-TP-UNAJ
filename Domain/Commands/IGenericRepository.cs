namespace Domain.Commands
{
    public interface IGenericRepository
    {
        public void Add<T>(T entity) where T : class;
        public void Remove<T>(T entity) where T : class;
        public void Update<T>(T entity) where T : class;

    }
}
