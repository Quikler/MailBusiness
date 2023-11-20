using MailBusinessLibrary.Classes.Models;

namespace MailBusinessLibrary.Interfaces
{
    public interface IPackageManager
    {
        Task<IEnumerable<Package>> GetPackagesStatisticAsync(Predicate<Package> predicate);

        Task<IEnumerable<Package>> SortPackagesAsync<TField>(Func<Package, TField> selector);

        Task RegisterPackagesOnClientAsync(Client client, IEnumerable<Package> packages);
        Task UpdatePackageInfoAsync(Client client, Package package, Package newPackage);
    }
}
