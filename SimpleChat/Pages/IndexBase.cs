using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;
using DTO.Member;
using SimpleChat.Interfaces;
using DTO.Talk;

namespace SimpleChat.Pages
{
    public class IndexBase : ComponentBase
    {
        [Inject] private NavigationManager? NavigationManager { get; set; }
        [Inject] private IMemberController? MemberController { get; set; }
        [Inject] private ITalkController? TalkController { get; set; }
        [Inject] private ITalkMemberController? TalkMemberController { get; set; }

        private HubConnection? hubConnection;
        public string? connectionId;
        public string memberLogin = string.Empty;
        public bool isMemberLogins;
        public List<MemberModel> members = new();
        public List<TalkModel> talks = new();
        public List<MemberModel> talkMembers = new();
        public string newMember = string.Empty;
        public string newTalk = string.Empty;
        public MemberModel memberSender = new();
        public MemberModel memberReceiver = new();
        public TalkModel talkReceiver = new();
        public int memberReceiverId;

        public List<Message> messages = new();
        
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
                isMemberLogins = !isMemberLogins;
                
                members = MemberController.GetAll().ToList();
                memberSender = members.Find(m => m.NickName.ToUpper().Equals(memberLogin.ToUpper()));
                if (memberSender != null)
                {
                    memberLogin = memberSender.NickName;
                    members.Remove(memberSender);
                }
                else
                {
                    memberSender = new MemberModel() { NickName = memberLogin };
                    MemberController.Create(memberSender);
                }

                talks = TalkController.GetNonPrivate().ToList();
            }
        }

        public void SelectMemberReceiver(ChangeEventArgs e)
        {
            if (MemberController != null && e.Value != null)
            {
                memberReceiver = MemberController.GetById(Convert.ToInt32(e.Value));
            }
        }

        public void AddNewMember()
        {
            if (!string.IsNullOrWhiteSpace(newMember))
            {
                if (members.Find(m => m.NickName.ToUpper().Equals(newMember.ToUpper())) == null)
                {
                    MemberController.Create(new MemberModel() { NickName = newMember });
                    newMember = string.Empty;
                    members = MemberController.GetAll().ToList();
                    MemberModel member = members.Find(m => m.NickName.ToUpper().Equals(memberLogin.ToUpper()));
                    if (member != null)
                    {
                        members.Remove(member);
                    }
                }
            }
        }

        public void SelectTalkReceiver(ChangeEventArgs e)
        {
            if (TalkController != null && e.Value != null)
            {
                talkReceiver = TalkController.GetById(Convert.ToInt32(e.Value));
            }

            talkMembers  = TalkMemberController.GetByIds(talkReceiver.Id, )
        }

        public async Task Edit(int messageNumber)
        {
            if (hubConnection is not null)
            {
                await hubConnection.SendAsync("SendMessage", string.Empty, memberLogin, messages[messageNumber].message);
            }
        }

        public void ButtonVisible()
        {
            bVisible = "visible";
        }

        public void ButtonHide()
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
