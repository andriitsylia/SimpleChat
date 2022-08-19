using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTOs
{
    public class MessageDTO
    {
        public int Id { get; set; }
        public int Sender { get; set; }
        public string? Text { get; set; }
        public DateTime Created { get; set; }
        public int MemberId { get; set; }
        public MemberDTO? Member { get; set; }
        public int TalkId { get; set; }
        public TalkDTO? Talk { get; set; }
    }
}
