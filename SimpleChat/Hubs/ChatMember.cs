namespace SimpleChat.Hubs
{
    public class ChatMember
    {
        private readonly List<ChatConnection> _connections = new();
        private readonly List<string> _talks = new();
        public string Name { get; }

        public IEnumerable<ChatConnection> Connections
        {
            get
            {
                return _connections;
            }
        }

        public IEnumerable<string> ConnectionIds
        {
            get
            {
                return Connections.Select(c => c.ConnectionId);
            }
        }

        public IEnumerable<string> Talks
        {
            get
            {
                return _talks;
            }
        }

        public DateTime? ConnectedAt
        {
            get
            {
                if (Connections.Any())
                {
                    return Connections.OrderByDescending(cc => cc.ConnectedAt).Select(c => c.ConnectedAt).First();
                }
                return null;
            }
        }

        public ChatMember(string name)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
        }

        public void AppendConnection(string connectionId)
        {
            if (!string.IsNullOrWhiteSpace(connectionId))
            {
                var connection = new ChatConnection()
                {
                    ConnectedAt = DateTime.Now,
                    ConnectionId = connectionId
                };
                _connections.Add(connection);
            }
        }

        public void RemoveConnection(string connectionId)
        {
            if (!string.IsNullOrWhiteSpace(connectionId))
            {
                var connection = _connections.SingleOrDefault(c => c.ConnectionId.Equals(connectionId));
                if (connection != null)
                {
                    _connections.Remove(connection);
                }
            }
        }

        public void AppendTalks(IEnumerable<string> talks)
        {
            if (talks != null)
            {
                IEnumerable<string> u = Talks.Union<string>(talks).ToList();
                _talks.Clear();
                _talks.AddRange(u);
            }
        }
    }
}
