using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using DTO.Member;
using System.Xml.Linq;

namespace SimpleChat.Hubs
{
    public class ChatManager
    {
        public List<ChatMember> ChatMembers { get; set; } = new();

        public void ConnectMember(string name, string connectionId, IEnumerable<string> talks)
        {
            var memberAlreadyExists = GetConnectedMemberByName(name);
            if (memberAlreadyExists != null)
            {
                memberAlreadyExists.AppendConnection(connectionId);
                memberAlreadyExists.AppendTalks(talks);
                return;
            }

            var member = new ChatMember(name);
            if (!string.IsNullOrWhiteSpace(connectionId))
            {
                member.AppendConnection(connectionId);
            }
            if (talks != null)
            {
                member.AppendTalks(talks);
            }
            ChatMembers.Add(member);
        }

        public bool DisconnectMember(string connectionId)
        {
            var memberExists = GetConnectedMemberById(connectionId);
            
            if (memberExists == null)
            {
                return false;
            }

            if (!memberExists.Connections.Any())
            {
                return false;
            }

            if (memberExists.Connections.Count() == 1)
            {
                ChatMembers.Remove(memberExists);
                return true;
            }

            memberExists.RemoveConnection(connectionId);
            return false;
        }

        public ChatMember? GetConnectedMemberByName(string name)
        {
            return ChatMembers.FirstOrDefault(
                cm => string.Equals(cm.Name, name, StringComparison.CurrentCultureIgnoreCase));
        }

        public ChatMember? GetConnectedMemberById(string connectionId)
        {
            return ChatMembers.FirstOrDefault(
                cm => cm.Connections.Select(c => c.ConnectionId).Contains(connectionId));
        }
    }
}
