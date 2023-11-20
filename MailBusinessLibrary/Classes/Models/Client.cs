using MongoDB.Bson.Serialization.Attributes;

namespace MailBusinessLibrary.Classes.Models
{
    [BsonIgnoreExtraElements]
    public record Client
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string Id { get; set; }

        private string _name;
        public string Name
        {
            get => _name;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Name cannot be empty", nameof(value));

                _name = value;
            }
        }

        private string _city;
        public string City
        {
            get => _city;
            set
            {
                if (value.Length < 2 || value.Length > 168)
                    throw new ArgumentException("City name must be greater than 1 and less than 169 symbols", nameof(value));

                _city = value;
            }
        }

        public List<Package>? Packages { get; set; }

        public Client(string name, string city, IEnumerable<Package>? packages)
        {
            Name = name; City = city;
            Packages = packages?.ToList();
        }
    }
}
