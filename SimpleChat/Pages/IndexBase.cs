using DTO.Member;
using DTO.Message;
using DTO.Talk;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;
using SimpleChat.Interfaces;
using SimpleChat.Services;

namespace SimpleChat.Pages
{
    public class IndexBase : ComponentBase
    {
        public const int MESSAGES_ON_PAGE = 20;

        [Inject] private NavigationManager? NavigationManager { get; set; }
        [Inject] private IMemberController? MemberController { get; set; }
        [Inject] private ITalkController? TalkController { get; set; }
        [Inject] private IMessageController? MessageController { get; set; }

        public List<MemberModel> allMembers = new();
        public List<MessageModel> allMessages = new List<MessageModel>();
        public List<MessageModel> visibleMessages = new List<MessageModel>();
        public List<MemberModel> talkMembers = new();
        public MessageModel selectedMessage = new();
        public string memberLogin = string.Empty;
        public bool isMemberLogon;
        public MemberModel sender = new();
        public List<TalkModel> senderNonPrivateTalks = new();
        public PageHandler pageHandler = new PageHandler(MESSAGES_ON_PAGE);
        public HubConnection? hubConnection;
        public string? connectionId;
        public string outputMessage = string.Empty;
        public string editedMessage = string.Empty;
        public string answerMessage = string.Empty;
        public bool isSelectedOwnMessage;
        public bool isSelectedNotOwnMessage;
        public Participant participant = new();

        protected override async Task OnInitializedAsync()
        {
            allMembers = MemberController.GetAll().ToList();
            pageHandler = new PageHandler(MESSAGES_ON_PAGE);
        }

        public async Task MemberLogins()
        {
            if (!string.IsNullOrEmpty(memberLogin))
            {
                sender = allMembers.FirstOrDefault(m => string.Equals(m.NickName, memberLogin, StringComparison.CurrentCultureIgnoreCase));
                if (sender != null)
                {
                    isMemberLogon = !isMemberLogon;
                    memberLogin = sender.NickName;
                    allMembers.Remove(sender);
                    senderNonPrivateTalks = sender.Talks.Where(t => !t.IsPrivate).ToList();
                    await HubConnect(sender.NickName, sender.Talks.Select(t => t.Name).ToList());
                }
                else
                {
                    memberLogin = string.Empty;
                }
            }
        }

        public async Task HubConnect(string member, IEnumerable<string> talks)
        {
            hubConnection = new HubConnectionBuilder()
                .WithUrl(NavigationManager.ToAbsoluteUri("/SimpleChatHub"))
                .Build();

            hubConnection.On<string, string>("SendMessageAsync", (sender, message) =>
            {
                LoadAllMessages();
                InvokeAsync(StateHasChanged);
            });

            await hubConnection.StartAsync();
            await hubConnection.SendAsync("ConnectMember", member, hubConnection.ConnectionId, talks);
            connectionId = hubConnection.ConnectionId;
        }

        public void SelectMemberParticipant(ChangeEventArgs e)
        {
            if (MemberController != null && e.Value != null)
            {
                MemberModel member = MemberController.GetById(Convert.ToInt32(e.Value));

                TalkModel talk = TalkController.GetByName($"{sender.NickName}_{member.NickName}")
                              ?? TalkController.GetByName($"{member.NickName}_{sender.NickName}");

                if (talk != null)
                {
                    participant = new Participant()
                    {
                        Id = member.Id,
                        Name = member.NickName,
                        Type = ParticipantType.Member,
                        TalkId = talk.Id,
                        TalkName = talk.Name
                    };

                    pageHandler = new PageHandler(MESSAGES_ON_PAGE);
                    LoadAllMessages();

                    isSelectedOwnMessage = false;
                    isSelectedNotOwnMessage = false;
                }
                else
                {
                    allMessages.Clear();
                    visibleMessages.Clear();
                }
            }
        }

        public void SelectTalkParticipant(ChangeEventArgs e)
        {
            if (TalkController != null && e.Value != null)
            {
                TalkModel talk = TalkController.GetById(Convert.ToInt32(e.Value));

                participant = new Participant()
                {
                    Id = talk.Id,
                    Name = talk.Name,
                    Type = ParticipantType.Talk,
                    TalkId = talk.Id,
                    TalkName = talk.Name
                };

                talkMembers = talk.Members.ToList();
                pageHandler = new PageHandler(MESSAGES_ON_PAGE);
                LoadAllMessages();

                isSelectedOwnMessage = false;
                isSelectedNotOwnMessage = false;
            }
        }

        public async Task SendMessageAsync(Participant participant, MessageModel message)
        {
            switch (participant.Type)
            {
                case ParticipantType.Member:
                    await hubConnection.SendAsync("SendMessageToMemberAsync",
                                                  participant.Name,
                                                  sender.NickName,
                                                  message.Text);
                    break;
                case ParticipantType.Talk:
                    await hubConnection.SendAsync("SendMessageToTalkAsync",
                                                  participant.Name,
                                                  sender.NickName,
                                                  message.Text);
                    break;
                default:
                    await hubConnection.SendAsync("SendMessageAsync", sender.NickName, message.Text);
                    break;
            }
        }

        public async Task SendMessageButtonClick()
        {
            if (hubConnection != null)
            {
                MessageModel message = new()
                {
                    Text = outputMessage,
                    Sender = sender.NickName,
                    Created = DateTime.Now,
                    MemberId = sender.Id,
                    TalkId = participant.TalkId
                };

                MessageController.Create(message);

                LoadAllMessages();

                await SendMessageAsync(participant, message);
                outputMessage = string.Empty;
            }
        }

        public void OnMessageClick(int selectedMessageId)
        {
            selectedMessage = visibleMessages.FirstOrDefault(m => m.Id == selectedMessageId);
            if (participant.Type == ParticipantType.Member)
            {
                if (sender.Id == selectedMessage.MemberId)
                {
                    editedMessage = selectedMessage.Text;
                    isSelectedOwnMessage = true;
                }
                else
                {
                    editedMessage = string.Empty;
                    isSelectedOwnMessage = false;
                }
            }
            if (participant.Type == ParticipantType.Talk)
            {
                editedMessage = selectedMessage.Text;
                if (sender.Id == selectedMessage.MemberId)
                {
                    isSelectedOwnMessage = true;
                    isSelectedNotOwnMessage = false;
                }
                else
                {
                    isSelectedOwnMessage = false;
                    isSelectedNotOwnMessage = true;
                }
            }
        }

        public void PreviousPageButtonClick()
        {
            pageHandler.CurrentPage = pageHandler.PrevPage;
            LoadVisibleMessages(pageHandler.SkipElements);
        }

        public void NextPageButtonClick()
        {
            pageHandler.CurrentPage = pageHandler.NextPage;
            LoadVisibleMessages(pageHandler.SkipElements);
        }

        public void LoadVisibleMessages(int skipMessages)
        {
            visibleMessages = allMessages.Skip(skipMessages)
                                         .Take(MESSAGES_ON_PAGE)
                                         .ToList();
        }

        public void LoadAllMessages()
        {
            allMessages = MessageController.GetAll()
                                           .Where(m => m.TalkId == participant.TalkId)
                                           .Select(m => m)
                                           .Reverse()
                                           .ToList();
            pageHandler.ElementsCount = allMessages.Count;
            LoadVisibleMessages(pageHandler.SkipElements);
        }

        public async Task SendAnswerToOwnerButtonClick()
        {
            string outText = $"{answerMessage}\n(Reply in {participant.Name} on {selectedMessage.Created.ToString()})";

            TalkModel talk = TalkController.GetByName($"{sender.NickName}_{selectedMessage.Sender}")
                          ?? TalkController.GetByName($"{selectedMessage.Sender}_{sender.NickName}");

            if (talk != null)
            {
                MessageModel messageModel = new()
                {
                    Text = outText,
                    Sender = sender.NickName,
                    Created = DateTime.Now,
                    MemberId = sender.Id,
                    TalkId = talk.Id
                };

                MessageController.Create(messageModel);
                answerMessage = string.Empty;
                isSelectedOwnMessage = false;

                await hubConnection.SendAsync("SendMessageToMemberAsync", selectedMessage.Sender, sender.NickName, outText);
            }
        }

        public async Task SendAnswerToAllButtonClick()
        {
            string outText = $"{answerMessage}\n(Reply on {selectedMessage.Sender}, {selectedMessage.Created.ToString()})";
            MessageModel messageModel = new()
            {
                Text = outText,
                Sender = sender.NickName,
                Created = DateTime.Now,
                MemberId = sender.Id,
                TalkId = participant.TalkId
            };

            MessageController.Create(messageModel);
            answerMessage = string.Empty;
            isSelectedOwnMessage = false;

            await hubConnection.SendAsync("SendMessageToTalkAsync", participant.Name, sender.NickName, outText);

        }

        public async Task UpdateMessageButtonClick()
        {
            selectedMessage.Text = editedMessage;
            MessageController.Update(selectedMessage);
            editedMessage = string.Empty;
            await hubConnection.SendAsync("SendMessageAsync", sender.NickName, "UpdateMessageClick");
        }

        public async Task DeleteMessageButtonClick()
        {
            if (selectedMessage.MemberId == sender.Id)
            {
                MessageController.Delete(selectedMessage.Id);
                editedMessage = string.Empty;
                await hubConnection.SendAsync("SendMessageAsync", sender.NickName, "DeleteMessageClick");
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
