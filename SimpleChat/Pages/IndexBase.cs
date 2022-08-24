using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;
using DTO.Member;
using SimpleChat.Interfaces;
using DTO.Talk;
using DTO.Message;
using DAL.Entities;
using static System.Net.Mime.MediaTypeNames;
using SimpleChat.Hubs;

namespace SimpleChat.Pages
{
    public enum ReceiverType
    {
        Member,
        Talk
    }

    public class Receiver
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ReceiverType Type { get; set; }
        public int TalkId { get; set; }
    }

    public class IndexBase : ComponentBase
    {
        public const int MESSAGES_ON_PAGE = 5;
        public const int PREVIOUS_PAGE = 1;
        public const int NEXT_PAGE = -1;

        [Inject] private NavigationManager? NavigationManager { get; set; }
        [Inject] private IMemberController? MemberController { get; set; }
        [Inject] private ITalkController? TalkController { get; set; }
        [Inject] private IMessageController? MessageController { get; set; }
        [Inject] private ChatManager ChatManager { get; set; }

        private HubConnection? hubConnection;
        public string? connectionId;
        public string memberLogin = string.Empty;
        public bool isMemberLogins;
        public List<MemberModel> members = new();
        public List<TalkModel> talks = new();
        public List<MemberModel> talkMembers = new();
        public List<MessageModel> messages = new List<MessageModel>();
        public List<MessageModel> allMessages = new List<MessageModel>();
        public string newMember = string.Empty;
        public string newTalk = string.Empty;
        public MemberModel memberSender = new();
        public MemberModel memberReceiver = new();
        public TalkModel talkReceiver = new();
        //public int memberReceiverId;
        //public ReceiverType receiverType;
        //public string receiverName = string.Empty;
        public Receiver receiver = new();
        public string outputMessage = string.Empty;

        public string buttonName = "Send";
        public int messageNumber = 0;

        public class Message
        {
            public int id;
            public string sender;
            public string? message;
        }
        
        public Message mmm;
        private List<TalkModel> memberTalks = new();
        public bool buttonPreviousDisabled;
        public bool buttonNextDisabled = true;
        public int pageCounter;
        public string editedMessage;
        public string answerMessage;
        public MessageModel mes = new();
        public bool isReceiverTalk;

        protected override async Task OnInitializedAsync()
        {
            //members = MemberController.GetAll().ToList();
            //talks = TalkController.GetAll().ToList();
        }
        
        public async Task HubConnect(string member, IEnumerable<string> talks)
        {
            hubConnection = new HubConnectionBuilder()
                .WithUrl(NavigationManager.ToAbsoluteUri("/SimpleChatHub"))
                .Build();

            //hubConnection.On<string, string>("ReceiveMessage", (user, message) =>
            //{
            //    var encodedMsg = $"{user}: {message}";
            //    //messages.Insert(0, new Message() { id = messageNumber++, message = encodedMsg });
            //    //this.message = string.Empty;
            //    //connectionId = hubConnection.ConnectionId;
            //    //buttonName = buttonName.Equals("Sended") ? "Send" : "Sended";
            //    InvokeAsync(StateHasChanged);
            //});
            
            hubConnection.On<string, string>("SendMessageAsync", (sender, message) =>
            {
                allMessages = MessageController.GetAll().Where(m => m.TalkId == receiver.TalkId).Select(m => m).Reverse().ToList();
                messages = (List<MessageModel>)GetTwentyMessages(allMessages);
                //var encodedMsg = $"{sender,15}: {message}";
                //messages.Insert(0, new Message() { id = ++messageNumber, sender = sender, message = message });
                //this.message = string.Empty;
                //connectionId = hubConnection.ConnectionId;
                //buttonName = buttonName.Equals("Sended") ? "Send" : "Sended";
                InvokeAsync(StateHasChanged);
            });

            //hubConnection.On<string, string>("SendMessageToMemberAsync", (sender, message) =>
            //{
            //    var encodedMsg = $"{sender} says {message}";
            //    messages.Insert(0, new Message() { id = ++messageNumber, message = encodedMsg });
            //    //this.message = string.Empty;
            //    //connectionId = hubConnection.ConnectionId;
            //    //buttonName = buttonName.Equals("Sended") ? "Send" : "Sended";
                //InvokeAsync(StateHasChanged);
            //});

            //hubConnection.On<string, string>("SendMessageToTalkAsync", (sender, message) =>
            //{
            //    var encodedMsg = $"{sender} says {message}";
            //    messages.Insert(0, new Message() { id = ++messageNumber, message = encodedMsg });
            //    //this.message = string.Empty;
            //    //connectionId = hubConnection.ConnectionId;
            //    //buttonName = buttonName.Equals("Sended") ? "Send" : "Sended";
            //    InvokeAsync(StateHasChanged);
            //});

            await hubConnection.StartAsync();

            ChatManager.ConnectMember(memberSender.NickName, hubConnection.ConnectionId, talks);
            await hubConnection.SendAsync("ConnectMember", memberSender.NickName, hubConnection.ConnectionId, talks);
            connectionId = hubConnection.ConnectionId;
        }

        public async Task Send()
        {
            if (hubConnection is not null)
            {
                //List<TalkModel> talks = TalkController.GetPrivate().ToList();

                MessageModel messageModel = new()
                {
                    Text = outputMessage,
                    Sender = memberSender.NickName,
                    Created = DateTime.Now,
                    MemberId = memberSender.Id,
                    TalkId = receiver.TalkId
                };

                MessageController.Create(messageModel);
                allMessages = MessageController.GetAll().Where(m => m.TalkId == receiver.TalkId).Select(m => m).Reverse().ToList();
                messages = (List<MessageModel>)GetTwentyMessages(allMessages);

                switch (receiver.Type)
                {
                    case ReceiverType.Member:
                        await hubConnection.SendAsync("SendMessageToMemberAsync",
                                                      receiver.Name,
                                                      memberSender.NickName,
                                                      outputMessage);
                        break;
                    case ReceiverType.Talk:
                        await hubConnection.SendAsync("SendMessageToTalkAsync",
                                                      receiver.Name,
                                                      memberSender.NickName,
                                                      outputMessage);
                        break;
                    default:
                        await hubConnection.SendAsync("SendMessageAsync", memberSender.NickName, outputMessage);
                        break;
                }
                outputMessage = string.Empty;
            }
        }

        public async Task MemberLogins()
        {
            if (!string.IsNullOrEmpty(memberLogin))
            {
                //isMemberLogins = !isMemberLogins;
                members = MemberController.GetAll().ToList();
                

                foreach (var m in members)
                {
                    
                    ChatManager.ConnectMember(m.NickName, string.Empty, m.Talks.Select(t => t.Name).ToList());
                }

                var tal = TalkController.GetAll();
                foreach (var t in tal)
                {

                    ChatManager.ChatTalks.TryAdd(t.Name, t.Members.Select(m => m.NickName).ToList());
                }

                memberSender = members.Find(m => string.Equals(m.NickName,
                                                               memberLogin,
                                                               StringComparison.CurrentCultureIgnoreCase));
                if (memberSender != null)
                {
                    isMemberLogins = !isMemberLogins;

                    memberLogin = memberSender.NickName;
                    members.Remove(memberSender);
                    //memberTalks = talks.Where(t => !t.IsPrivate).Select(t => t).ToList();
                    talks = TalkController.GetNonPrivate().ToList();
                    

                    await HubConnect(memberSender.NickName, memberSender.Talks.Select(t => t.Name).ToList());
                }
                //else
                //{
                //    memberSender = new MemberModel() { NickName = memberLogin };
                //    MemberController.Create(memberSender);
                //}

            }
        }

        public async Task SelectMemberReceiver(ChangeEventArgs e)
        {
            if (MemberController != null && e.Value != null)
            {
                memberReceiver = MemberController.GetById(Convert.ToInt32(e.Value));
               
                TalkModel talk = memberReceiver.Talks.FirstOrDefault(
                    t => string.Equals(t.Name,
                                       $"{memberSender.NickName}_{memberReceiver.NickName}",
                                       StringComparison.CurrentCultureIgnoreCase)
                      || string.Equals(t.Name,
                                       $"{memberReceiver.NickName}_{memberSender.NickName}",
                                       StringComparison.CurrentCultureIgnoreCase));

                //if (talk == null)
                //{
                //    talk = new TalkModel()
                //    {
                //        Name = $"{memberSender.NickName.ToUpper()}_{memberReceiver.NickName.ToUpper()}",
                //        IsPrivate = true,
                //        Members = { memberSender, memberReceiver }
                //    };

                //    TalkController.Create(talk);
                //    talk = TalkController.GetByName(talk.Name);
                //}
                if (talk != null)
                {
                    receiver = new Receiver()
                    {
                        Id = memberReceiver.Id,
                        Name = memberReceiver.NickName,
                        Type = ReceiverType.Member,
                        TalkId = talk.Id
                    };
                    isReceiverTalk = false;
                    pageCounter = 0;
                    allMessages = MessageController.GetAll().Where(m => m.TalkId == receiver.TalkId).Select(m => m).Reverse().ToList();
                    messages = GetTwentyMessages(allMessages);
                    int i = 0;
                }
                else
                {
                    allMessages.Clear();
                    messages.Clear();
                }
                //await hubConnection.SendAsync("SendMessageAsync", string.Empty, String.Empty);
            }
        }

        public void AddNewMember()
        {
            //if (!string.IsNullOrWhiteSpace(newMember))
            //{
            //    if (members.Find(m => m.NickName.ToUpper().Equals(newMember.ToUpper())) == null)
            //    {
            //        MemberController.Create(new MemberModel() { NickName = newMember });
            //        newMember = string.Empty;
            //        members = MemberController.GetAll().ToList();
            //        MemberModel member = members.Find(m => m.NickName.ToUpper().Equals(memberLogin.ToUpper()));
            //        if (member != null)
            //        {
            //            members.Remove(member);
            //        }
            //    }
            //}
        }

        public async Task SelectTalkReceiver(ChangeEventArgs e)
        {
            if (TalkController != null && e.Value != null)
            {
                talkReceiver = TalkController.GetById(Convert.ToInt32(e.Value));
                
                receiver = new Receiver()
                {
                    Id = talkReceiver.Id,
                    Name = talkReceiver.Name,
                    Type = ReceiverType.Talk,
                    TalkId = talkReceiver.Id
                };
                isReceiverTalk = true;
                talkMembers = talkReceiver.Members.ToList();
                allMessages = MessageController.GetAll().Where(m => m.TalkId == receiver.TalkId).Select(m => m).Reverse().ToList();
                messages = (List<MessageModel>)GetTwentyMessages(allMessages);
            }
            //await hubConnection.SendAsync("SendMessageAsync", string.Empty, String.Empty);
        }

        public void AddNewTalk()
        {
            //if (!string.IsNullOrWhiteSpace(newTalk))
            //{
            //    TalkController.Create(new TalkModel() { Name = newTalk });
            //    newTalk = string.Empty;
            //    talks = TalkController.GetNonPrivate().ToList();
            //}
        }

        public void PageButtonClick(int direction)
        {
            float pageCoefficient = (float)allMessages.Count() / (float)MESSAGES_ON_PAGE;

            pageCounter += direction;
            
            if (pageCounter <= 0)
            {
                buttonNextDisabled = true;
            }
            else
            {
                buttonNextDisabled = false;
            }
            if (pageCounter > pageCoefficient)
            {
                buttonPreviousDisabled = true;
            }
            else
            {
                buttonPreviousDisabled = false;
            }
            messages = (List<MessageModel>)GetTwentyMessages(allMessages);
        }

        public List<MessageModel> GetTwentyMessages(List<MessageModel> message)
        {
            var result = message.Skip(MESSAGES_ON_PAGE * pageCounter).Take(MESSAGES_ON_PAGE).ToList();
            return result;
        }

        public bool inputEditedMessage;
        public bool inputAnswerMessage;
        public void Edit(int messageId)
        {
            mes = messages.SingleOrDefault(m => m.Id == messageId);
            if (receiver.Type == ReceiverType.Member)
            {
                if (mes.MemberId == memberSender.Id)
                {
                    editedMessage = mes.Text;
                }
            }
            if (receiver.Type == ReceiverType.Talk)
            {
                editedMessage = mes.Text;

                if (memberSender.Id != mes.MemberId)
                {
                    inputAnswerMessage = false;
                    inputEditedMessage = true;
                }
                else
                {
                    inputAnswerMessage = true;
                    inputEditedMessage = false;
                    
                }
                
                
            }
        }

        public async Task SendDirectClick()
        {
            string outText = $"{answerMessage}\n(Reply in {receiver.Name} on {mes.Created.ToString()})";
            
            TalkModel talk = memberSender.Talks.FirstOrDefault(
                    t => string.Equals(t.Name,
                                       $"{memberSender.NickName}_{mes.Sender}",
                                       StringComparison.CurrentCultureIgnoreCase)
                      || string.Equals(t.Name,
                                       $"{mes.Sender}_{memberSender.NickName}",
                                       StringComparison.CurrentCultureIgnoreCase));
            if (talk != null)
            {
                MessageModel messageModel = new()
                {
                    Text = outText,
                    Sender = memberSender.NickName,
                    Created = DateTime.Now,
                    MemberId = memberSender.Id,
                    TalkId = talk.Id
                };

                MessageController.Create(messageModel);
                answerMessage = string.Empty;
                inputEditedMessage = false;


                await hubConnection.SendAsync("SendMessageToMemberAsync", mes.Sender, memberSender.NickName, outText);

            }
        }

        public async Task SendToAllClick()
        {
            string outText = $"{answerMessage}\n(Reply on {mes.Sender}, {mes.Created.ToString()})";
            MessageModel messageModel = new()
            {
                Text = outText,
                Sender = memberSender.NickName,
                Created = DateTime.Now,
                MemberId = memberSender.Id,
                TalkId = receiver.TalkId
            };

            MessageController.Create(messageModel);
            answerMessage = string.Empty;
            inputEditedMessage = false;


            await hubConnection.SendAsync("SendMessageToTalkAsync", receiver.Name, memberSender.NickName, outText);

        }

        public async Task UpdateMessageClick()
        {
            mes.Text = editedMessage;
            MessageController.Update(mes);
            //allMessages = MessageController.GetAll().Where(m => m.TalkId == receiver.TalkId).Select(m => m).Reverse().ToList();
            //messages = (List<MessageModel>)GetTwentyMessages(allMessages);
            editedMessage = string.Empty;
            //await hubConnection.SendAsync("SendMessageAsync", memberSender.NickName, "UpdateMessageClick");
            switch (receiver.Type)
            {
                case ReceiverType.Member:
                    await hubConnection.SendAsync("SendMessageToMemberAsync",
                                                  receiver.Name,
                                                  memberSender.NickName,
                                                  "");
                    break;
                case ReceiverType.Talk:
                    await hubConnection.SendAsync("SendMessageToTalkAsync",
                                                  receiver.Name,
                                                  memberSender.NickName,
                                                  "");
                    break;
                default:
                    await hubConnection.SendAsync("SendMessageAsync", memberSender.NickName, "");
                    break;
            }
            outputMessage = string.Empty;
        }

        public async Task DeleteMessageClick()
        {
            if (mes.MemberId == memberSender.Id)
            {
                MessageController.Delete(mes.Id);
                //allMessages = MessageController.GetAll().Where(m => m.TalkId == receiver.TalkId).Select(m => m).Reverse().ToList();
                //messages = (List<MessageModel>)GetTwentyMessages(allMessages);
                editedMessage = string.Empty;
                await hubConnection.SendAsync("SendMessageAsync", memberSender.NickName, "DeleteMessageClick");
            }
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
