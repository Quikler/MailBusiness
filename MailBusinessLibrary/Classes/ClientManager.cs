using MailBusinessLibrary.Classes.Models;
using MailBusinessLibrary.Interfaces;

namespace MailBusinessLibrary.Classes
{
    public class ClientManager : IClientManager
    {
        private readonly IApplicationDbContext<Client> db;

        public Task AddClientAsync(Client client) => db.AddAsync(client);

        public Task DeleteClientAsync(Client client) => db.DeleteAsync(client);
        
        public Task UpdateClientAsync(Client client, Client newClient) => db.UpdateAsync(client, newClient);
        public Task UpdateClientAsync<TField>(Client client, string fieldName, TField newValue)
            => db.UpdateAsync(client, fieldName, newValue);

        public async Task<IEnumerable<Client>> GetClientsStatisticAsync(Predicate<Client> predicate)
            => await db.GetAllAsync(predicate);
        public async Task<IEnumerable<Client>> GetClientsStatisticAsync()
            => await db.GetAllAsync();

        public ClientManager(IApplicationDbContext<Client> dbContext) => db = dbContext;
    }
}
