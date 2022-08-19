using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class TalkMember
    {
        public int TalkId { get; set; }
        public Talk? Talk { get; set; }
        public int MemberId { get; set; }
        public Member? Member { get; set; }
    }
}
