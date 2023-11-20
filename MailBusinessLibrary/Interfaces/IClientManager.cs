using MailBusinessLibrary.Classes.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailBusinessLibrary.Interfaces
{
    public interface IClientManager
    {
        Task AddClientAsync(Client client);
        Task UpdateClientAsync(Client client, Client newClient);
        Task UpdateClientAsync<TField>(Client client, string fieldName, TField newValue);
        Task DeleteClientAsync(Client client);

        Task<IEnumerable<Client>> GetClientsStatisticAsync();
        Task<IEnumerable<Client>> GetClientsStatisticAsync(Predicate<Client> predicate);
    }
}
