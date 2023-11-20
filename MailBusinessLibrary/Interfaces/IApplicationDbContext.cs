using MailBusinessLibrary.Classes;

namespace MailBusinessLibrary.Interfaces
{
    public interface IApplicationDbContext<T>
    {
        Task AddAsync(T data);
        Task AddRangeAsync(IEnumerable<T> data);
        Task UpdateAsync(T data, T newData);
        Task UpdateAsync<TField>(T data, string fieldName, TField newValue);

        Task<List<T>> GetAllAsync();
        Task<List<T>> GetAllAsync(Predicate<T> predicate);

        Task DeleteAsync(T data);
        Task DeleteRangeAsync(IEnumerable<T> data);
    }
}
