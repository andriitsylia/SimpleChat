using Microsoft.AspNetCore.SignalR;

namespace SimpleChat.Hubs
{
    public class SimpleChatHub : Hub
    {
        public async Task SendMessage(string sender, string member, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", member, message);
            if (string.IsNullOrWhiteSpace(sender))
            {
                await Clients.Client(sender).SendAsync("ReceiveMessage", member, $"private: {message}");
            }
        }
    }
}
