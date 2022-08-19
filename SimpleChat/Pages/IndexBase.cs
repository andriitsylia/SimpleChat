using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;
using DTO.Member;
using SimpleChat.Interfaces;

namespace SimpleChat.Pages
{
    public class IndexBase : ComponentBase
    {
        [Inject]
        private NavigationManager? NavigationManager { get; set; }

        [Inject]
        private IMemberController _memberController { get; set; }

        private HubConnection? hubConnection;
        public string? connectionId;
        public string memberLogin = string.Empty;
        public bool isMemberLogins = false;
        public List<MemberModel> members = new();
        public string newMember = string.Empty; 


        //private List<string> messages = new List<string>();
        public List<Message> messages = new List<Message>();
        
        public List<Client> groups = new List<Client>();

        public string? messageInput;
        public string? toUserInput;
        public string buttonName = "Send";
        public int messageNumber = 0;
        public string bVisible = "visible";
        

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
                messageInput = string.Empty;
                toUserInput = string.Empty;
                connectionId = hubConnection.ConnectionId;
                buttonName = buttonName.Equals("Sended") ? "Send" : "Sended";
                InvokeAsync(StateHasChanged);
            });

            await hubConnection.StartAsync();

            connectionId = hubConnection.ConnectionId;
        }

        public async Task Send()
        {
            if (hubConnection is not null)
            {
                await hubConnection.SendAsync("SendMessage", toUserInput, memberLogin, messageInput);
                //await hubConnection.SendAsync("SendPrivateMessage", toUserInput, userInput, "private: " + messageInput);
                //await hubConnection.SendAsync("SendMessageToCaller", toUserInput, userInput + "%", "%" + messageInput);
            }
        }

        public void MemberLogins()
        {
            if (!string.IsNullOrEmpty(memberLogin))
            {
                isMemberLogins = true;
                members = _memberController.GetAll().ToList();
                MemberModel member = members.Find(m => m.NickName.ToUpper().Equals(memberLogin.ToUpper()));
                if (member != null)
                {
                    members.Remove(member);
                }
                else
                {
                    _memberController.Create(new MemberModel() { NickName = memberLogin });
                }
            }
        }

        public void AddNewMember()
        {
            if (!string.IsNullOrWhiteSpace(newMember))
            {
                if (members.Find(m => m.NickName.ToUpper().Equals(newMember.ToUpper())) == null)
                {
                    _memberController.Create(new MemberModel() { NickName = newMember });
                    newMember = string.Empty;
                    members = _memberController.GetAll().ToList();
                    MemberModel member = members.Find(m => m.NickName.ToUpper().Equals(memberLogin.ToUpper()));
                    if (member != null)
                    {
                        members.Remove(member);
                    }
                }
            }
        }

        public async Task Edit(int messageNumber)
        {
            if (hubConnection is not null)
            {
                await hubConnection.SendAsync("SendMessage", string.Empty, memberLogin, messages[messageNumber].message);
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
