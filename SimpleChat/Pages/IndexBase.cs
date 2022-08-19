using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;

namespace SimpleChat.Pages
{
    public class IndexBase : ComponentBase
    {
        [Inject]
        public NavigationManager? NavigationManager { get; set; }

        private HubConnection? hubConnection;
        //private List<string> messages = new List<string>();
        public List<Message> messages = new List<Message>();
        public List<Client> users = new List<Client>();
        public List<Client> groups = new List<Client>();
        public string? userInput;
        public string? userId;
        public string? messageInput;
        public string? toUserInput;
        public string buttonName = "Send";
        public int messageNumber = 0;
        public string bVisible = "visible";
        public bool isUserLogins = false;

        public class Message
        {
            public int id;
            public string? message;
        }

        public class Client
        {
            public int id;
            public string name;
        }

        protected override async Task OnInitializedAsync()
        {
            hubConnection = new HubConnectionBuilder()
                .WithUrl(NavigationManager.ToAbsoluteUri("/SimpleChatHub"))
                .Build();

            hubConnection.On<string, string>("ReceiveMessage", (user, message) =>
            {
                var encodedMsg = $"{user}: {message}";
                messages.Insert(0, new Message() { id = messageNumber++, message = encodedMsg });
                //messageNumber++;
                //userInput = string.Empty;
                messageInput = string.Empty;
                toUserInput = string.Empty;
                userId = hubConnection.ConnectionId;
                buttonName = buttonName.Equals("Sended") ? "Send" : "Sended";
                InvokeAsync(StateHasChanged);
            });

            await hubConnection.StartAsync();
            userId = hubConnection.ConnectionId;
        }

        public async Task Send()
        {
            if (hubConnection is not null)
            {
                await hubConnection.SendAsync("SendMessage", toUserInput, userInput, messageInput);
                //await hubConnection.SendAsync("SendPrivateMessage", toUserInput, userInput, "private: " + messageInput);
                //await hubConnection.SendAsync("SendMessageToCaller", toUserInput, userInput + "%", "%" + messageInput);
            }
        }

        public void UserLogin()
        {
            if (!string.IsNullOrEmpty(userInput))
            {
                isUserLogins = true;
                for (int i = 0; i < 10; i++)
                {
                    messages.Add(new Message() { id = messageNumber++, message = $"message #{i + 1}" });
                }
                for (int i = 0; i < 10; i++)
                {
                    users.Add(new Client() { id = i, name = $"User {i + 1}" });
                    groups.Add(new Client() { id = i, name = $"Group {i + 1}" });
                }
            }
        }

        public async Task Edit(int messageNumber)
        {
            if (hubConnection is not null)
            {
                await hubConnection.SendAsync("SendMessage", string.Empty, userInput, messages[messageNumber].message);
            }
        }

        public async Task ButtonVisible()
        {
            bVisible = "visible";
        }

        public async Task ButtonHide()
        {
            bVisible = "invisible";
        }

        public bool IsConnected =>
            hubConnection?.State == HubConnectionState.Connected;

        public async ValueTask DisposeAsync()
        {
            if (hubConnection is not null)
            {
                await hubConnection.DisposeAsync();
            }
        }
    }
}
