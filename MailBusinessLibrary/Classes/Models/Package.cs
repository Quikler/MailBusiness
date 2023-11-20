using MongoDB.Bson.Serialization.Attributes;

namespace MailBusinessLibrary.Classes.Models
{
    public record Package
    {
        private string _content;
        public string Content
        {
            get => _content;
            set
            {
                if(string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Content cannot be empty", nameof(value));

                _content = value;
            }
        }

        public Direction Direction { get; set; }

        public DateTime DispatchTime { get; set; }
        public double Weight { get; set; }

        public Package(string content, DateTime dispatchTime, double weight, Direction direction)
        {
            Content = content;
            DispatchTime = dispatchTime;
            Weight = weight;
            Direction = direction;
        }
    }
}