using AutoMapper;
using BLL.Interfaces;
using DTO.TalkMember;
using SimpleChat.Interfaces;

namespace SimpleChat.Controllers
{
    public class TalkMemberController : ITalkMemberController
    {
        private readonly ITalkMemberService _service;
        private readonly IMapper _mapper;

        public TalkMemberController(ITalkMemberService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        public void Create(TalkMemberModel talkMember)
        {
            TalkMemberDTO talkMemberDTO = _mapper.Map<TalkMemberDTO>(talkMember);
            _service.Create(talkMemberDTO);
        }

        public void Update(TalkMemberModel talkMember)
        {
            TalkMemberDTO talkMemberDTO = _mapper.Map<TalkMemberDTO>(talkMember);
            _service.Update(talkMemberDTO);
        }

        public void Delete(int id)
        {
        }

        public TalkMemberModel GetByIds(int talkId, int memberId)
        {
            TalkMemberDTO talkMemberDTO = _service.GetByIds(talkId, memberId);
            TalkMemberModel result = _mapper.Map<TalkMemberModel>(talkMemberDTO);
            return result;
        }

        public IEnumerable<TalkMemberModel> GetAll()
        {
            IEnumerable<TalkMemberModel> result = _mapper.Map<IEnumerable<TalkMemberModel>>(_service.GetAll());
            return result;
        }
    }
}
