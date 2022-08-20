using AutoMapper;
using BLL.Interfaces;
using DAL.Entities;
using DAL.Interfaces;
using DTO.Talk;
using DTO.TalkMember;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class TalkMemberService : ITalkMemberService
    {
        private readonly ISimpleChatRepository<TalkMember> _repository;
        IMapper _mapper;

        public TalkMemberService(ISimpleChatRepository<TalkMember> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public void Create(TalkMemberDTO item)
        {
            TalkMember talkMember = _mapper.Map<TalkMember>(item);
            _repository.Create(talkMember);
            _repository.Save();
        }

        public void Update(TalkMemberDTO item)
        {
            TalkMember talkMember = _repository.Get(filter: t => t.TalkId == item.TalkId && t.MemberId == item.MemberId)
                                               .FirstOrDefault();
            if (talkMember != null)
            {
                _mapper.Map(item, talkMember);
                _repository.Update(talkMember);
                _repository.Save();
            }
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public TalkMemberDTO GetById(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TalkMemberDTO> GetAll()
        {
            IEnumerable<TalkMember> talkMembers = _repository.GetAll();
            IEnumerable<TalkMemberDTO> result = _mapper.Map<IEnumerable<TalkMemberDTO>>(talkMembers);
            return result;
        }

        public TalkMemberDTO GetByIds(int talkId, int memberId)
        {
            TalkMember talkMember = _repository.Get(filter: t => t.TalkId == talkId && t.MemberId == memberId,
                                                    includeProperties: "Talk, Member")
                                                .FirstOrDefault();
            TalkMemberDTO result = _mapper.Map<TalkMemberDTO>(talkMember);
            return result;
        }
    }
}
