using DTO.Member;
using DTO.Talk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.TalkMember
{
    public class TalkMemberModel
    {
        public int TalkId { get; set; }
        public TalkModel? Talk { get; set; }
        public int MemberId { get; set; }
        public MemberModel? Member { get; set; }
    }
}
