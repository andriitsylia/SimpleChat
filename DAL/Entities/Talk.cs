using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class Talk
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public bool IsPrivate { get; set; }
        public ICollection<TalkMember> TalkMembers { get; set; } = new List<TalkMember>();
        public ICollection<Message> Messages { get; set; } = new List<Message>();

    }
}
