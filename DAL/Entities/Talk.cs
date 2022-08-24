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
        public ICollection<Member> Members { get; set; } = new List<Member>();
        public ICollection<Message> Messages { get; set; } = new List<Message>();

    }
}
