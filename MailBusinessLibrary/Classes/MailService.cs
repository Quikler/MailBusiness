using MailBusinessLibrary.Classes.Models;
using MailBusinessLibrary.Interfaces;

namespace MailBusinessLibrary.Classes
{
    public class MailService
    {
        private readonly IApplicationDbContext<Client> db;

        public string Name { get; }
        public IClientManager ClientManager { get; set; }
        public IPackageManager PackageManager { get; set; }

        public MailService(string serviceName, string connectionDbString, string dbName, string collectionName)
        {
            Name = serviceName;
            db = new ClientsDbContext(connectionDbString, dbName, collectionName);
            ClientManager = new ClientManager(db);
            PackageManager = new PackageManager(db);
        }
    }
}