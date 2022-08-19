using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTOs
{
    public class TalkMemberDTO
    {
        public int TalkId { get; set; }
        public TalkDTO? Talk { get; set; }
        public int MemberId { get; set; }
        public MemberDTO? Member { get; set; }
    }
}
