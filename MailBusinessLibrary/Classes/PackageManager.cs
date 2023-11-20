using MailBusinessLibrary.Classes.Models;
using MailBusinessLibrary.Interfaces;

namespace MailBusinessLibrary.Classes
{
    public class PackageManager : IPackageManager
    {
        private readonly IApplicationDbContext<Client> db;

        private async Task<List<Package>> GetAllPackagesAsync()
            => (await db.GetAllAsync()).SelectMany(c => c.Packages ?? Enumerable.Empty<Package>()).ToList();

        private async Task<List<Package>> GetAllPackagesAsync(Predicate<Package> predicate)
            => (await GetAllPackagesAsync()).FindAll(predicate);

        public async Task<IEnumerable<Package>> GetPackagesStatisticAsync(Predicate<Package> predicate)
            => await GetAllPackagesAsync(predicate);

        public async Task<IEnumerable<Package>> SortPackagesAsync<TField>(Func<Package, TField> selector)
            => (await GetAllPackagesAsync()).OrderBy(selector);

        public Task RegisterPackagesOnClientAsync(Client client, IEnumerable<Package> packages)
        {
            if (packages is null) throw new ArgumentNullException(nameof(packages),
                "Method RegisterPackagesOnClientAsync must add some range of packages to the specified client");

            client.Packages ??= new List<Package>();
            client.Packages.AddRange(packages);

            return db.UpdateAsync(client, "Packages", client.Packages);
        }

        public async Task UpdatePackageInfoAsync(Client client, Package package, Package newPackage)
        {
            int i = client.Packages?.IndexOf(package) ?? 
                throw new ArgumentException("Nothing to update with specified client", nameof(client));

            client.Packages?.Remove(package);
            client.Packages?.Insert(i, newPackage);

            await db.UpdateAsync(client, client);
        }

        public PackageManager(IApplicationDbContext<Client> dbContext) => db = dbContext;
    }
}
