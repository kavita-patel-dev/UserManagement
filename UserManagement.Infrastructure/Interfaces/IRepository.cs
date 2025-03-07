namespace UserManagement.Infrastructure.Interfaces
{
    public interface IRepository<T> where T : class
    {
        Task<(IEnumerable<T> Data, int TotalCount)> GetAllAsync(int skip = 0, int take = 50);
    }
}
