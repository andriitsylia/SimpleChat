namespace SimpleChat.Hubs
{
    public interface ISimpleChatHub
    {
        Task SendMessageAsync(string sender, string message);

    }
}
