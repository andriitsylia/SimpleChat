using Microsoft.AspNetCore.SignalR;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace SimpleChat.Hubs
{
    public class SimpleChatHub : Hub<ISimpleChatHub>
    {
        private readonly ChatManager _chatManager;
        //private const string _defaultGroupName = "General";

        public SimpleChatHub(ChatManager chatManager)
        {
            _chatManager = chatManager;
        }

        //public async Task ConnectMember(string name, IEnumerable<string> talks)
        //{
        //    var connectionId = Context.ConnectionId;
        //    _chatManager.ConnectMember(name, connectionId);
        //    //await Groups.AddToGroupAsync(connectionId, _defaultGroupName);
        //}

        public override async Task OnConnectedAsync()
        {
            //var connectionId = Context.ConnectionId;
            //var chatMember = _chatManager.ChatMembers.FirstOrDefault(cm => cm.Connections.Select(c => c.ConnectionId).Contains(connectionId));
            //foreach (var talk in chatMember.Talks)
            //{
            //    await Groups.AddToGroupAsync(connectionId, talk);
            //}

            await base.OnConnectedAsync();
        }

        public async Task ConnectMember(string name, string connectionId, IEnumerable<string> talks)
        {
            //var connectionId = Context.ConnectionId;
            var chatMember = _chatManager.ChatMembers.FirstOrDefault(cm => cm.Connections.Select(c => c.ConnectionId).Contains(connectionId));
            foreach (var talk in chatMember.Talks)
            {
                await Groups.AddToGroupAsync(connectionId, talk);
            }

        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            var isMemberRemoved = _chatManager.DisconnectMember(Context.ConnectionId);
            if (!isMemberRemoved)
            {
                await base.OnDisconnectedAsync(exception);
            }

            //await Groups.RemoveFromGroupAsync(Context.ConnectionId, _defaultGroupName);
            await base.OnDisconnectedAsync(exception);
        }

        public async Task SendMessageAsync(string sender, string message)
        {
            await Clients.All.SendMessageAsync(sender, message);
            //await Clients.All.SendMessageAsync("ReceiveMessage", member, message);
            //if (string.IsNullOrWhiteSpace(sender))
            //{
            //    await Clients.Client(sender).SendAsync("ReceiveMessage", member, $"private: {message}");
            //}
        } 

        public async Task SendMessageToMemberAsync(string member, string sender, string message)
        {
            var chatMember = _chatManager.ChatMembers.FirstOrDefault(m => string.Equals(m.Name, member, StringComparison.CurrentCultureIgnoreCase));
            var chatSender = _chatManager.ChatMembers.FirstOrDefault(m => string.Equals(m.Name, sender, StringComparison.CurrentCultureIgnoreCase));
            List<string> connections = new List<string>();
            connections.AddRange(chatMember.Connections.Select(c => c.ConnectionId).ToList());
            connections.AddRange(chatSender.Connections.Select(c => c.ConnectionId).ToList());
            IReadOnlyList<string> connectionList = connections;
            await Clients.Clients(connectionList).SendMessageAsync(sender, message);
        }
        public async Task SendMessageToTalkAsync(string talk, string sender, string message)
        {
            await Clients.Group(talk).SendMessageAsync(sender, message);
        }


    }
}
