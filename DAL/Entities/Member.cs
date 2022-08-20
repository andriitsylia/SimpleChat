using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class Member
    {
        public int Id { get; set; }
        public string NickName { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public ICollection<Talk> Talks { get; set; } = new List<Talk>();
        public ICollection<TalkMember> TalkMembers { get; set; } = new List<TalkMember>();
        public ICollection<Message> Messages { get; set; } = new List<Message>();
    }
}
