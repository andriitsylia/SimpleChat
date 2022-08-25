using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.SignalR.Client;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace SimpleChat.Hubs
{
    public class SimpleChatHub : Hub<ISimpleChatHub>
    {
        private readonly ChatManager _chatManager;

        public SimpleChatHub(ChatManager chatManager)
        {
            _chatManager = chatManager;
        }

        public override async Task OnConnectedAsync()
        {
            await base.OnConnectedAsync();
        }

        public async Task ConnectMember(string name, string connectionId, IEnumerable<string> talks)
        {
            _chatManager.ConnectMember(name, connectionId, talks);
            foreach (var talk in talks)
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
            await base.OnDisconnectedAsync(exception);
        }

        public async Task SendMessageAsync(string sender, string message)
        {
            await Clients.All.SendMessageAsync(sender, message);
        } 

        public async Task SendMessageToMemberAsync(string participant, string sender, string message)
        {
            var chatParticipant = _chatManager.GetConnectedMemberByName(participant);
            var chatSender = _chatManager.GetConnectedMemberByName(sender);
            IReadOnlyList<string> connectionList = chatParticipant.ConnectionIds.Concat(chatSender.ConnectionIds).ToList();
            await Clients.Clients(connectionList).SendMessageAsync(sender, message);
        }

        public async Task SendMessageToTalkAsync(string talk, string sender, string message)
        {
            await Clients.Group(talk).SendMessageAsync(sender, message);
        }

    }
}
