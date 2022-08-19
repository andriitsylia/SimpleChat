using DTO.Member;
using DTO.Talk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.TalkMember
{
    public class TalkMemberDTO
    {
        public int TalkId { get; set; }
        public TalkDTO? Talk { get; set; }
        public int MemberId { get; set; }
        public MemberDTO? Member { get; set; }
    }
}
