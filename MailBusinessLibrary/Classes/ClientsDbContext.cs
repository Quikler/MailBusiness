using MailBusinessLibrary.Classes.Models;
using MailBusinessLibrary.Interfaces;
using MongoDB.Driver;

namespace MailBusinessLibrary.Classes
{
    public class ClientsDbContext : IApplicationDbContext<Client>
    {
        private readonly string ConnectionString, DatabaseName, CollectionName;

        private readonly IMongoClient _mongoClient;
        private readonly IMongoDatabase _mongoDatabase;

        private IMongoCollection<Client> GetDbCollection() => _mongoDatabase.GetCollection<Client>(CollectionName);

        public async Task<List<Client>> GetAllAsync()
        {
            IMongoCollection<Client> collection = GetDbCollection();
            var results = await collection.FindAsync(c => true);
            return results.ToList();
        }

        public async Task<List<Client>> GetAllAsync(Predicate<Client> predicate)
        {
            return (await GetAllAsync()).FindAll(predicate);
        }

        public Task AddAsync(Client client)
        {
            IMongoCollection<Client> collection = GetDbCollection();
            return collection.InsertOneAsync(client);
        }

        public Task AddRangeAsync(IEnumerable<Client> clients)
        {
            IMongoCollection<Client> collection = GetDbCollection();
            return collection.InsertManyAsync(clients);
        }

        public Task UpdateAsync<TField>(Client client, string fieldName, TField newValue)
        {
            IMongoCollection<Client> collection = GetDbCollection();

            // Creating equality filter with specified client's id
            var filter = Builders<Client>.Filter.Eq(p => p.Id, client.Id);

            // Creating an update with specified field name and new value as replacement
            var update = Builders<Client>.Update.Set(fieldName, newValue);

            return collection.UpdateOneAsync(filter, update);
        }

        public Task UpdateAsync(Client client, Client newClient)
        {
            IMongoCollection<Client> collection = GetDbCollection();

            // Creating equality filter with specified client's id
            var filter = Builders<Client>.Filter.Eq(p => p.Id, client.Id);

            return collection.ReplaceOneAsync(filter, newClient);
        }

        public Task DeleteAsync(Client client)
        {
            IMongoCollection<Client> collection = GetDbCollection();

            // Creating equality filter with specified client's id
            FilterDefinition<Client> filter = Builders<Client>.Filter.Eq(c => c.Id, client.Id);

            return collection.DeleteOneAsync(filter);
        }

        public Task DeleteRangeAsync(IEnumerable<Client> clients)
        {
            IMongoCollection<Client> collection = GetDbCollection();

            var idsToDelete = clients.Select(c => c.Id);

            // Creating filter for choosing documents with specified identificators
            var filter = Builders<Client>.Filter.In(p => p.Id, idsToDelete);

            return collection.DeleteManyAsync(filter);
        }

        public ClientsDbContext(string connectionString, string databaseName, string collectionName)
        {
            ConnectionString = connectionString;
            DatabaseName = databaseName;
            CollectionName = collectionName;

            _mongoClient = new MongoClient(ConnectionString);
            _mongoDatabase = _mongoClient.GetDatabase(DatabaseName);
        }
    }
}
