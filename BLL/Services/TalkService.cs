using AutoMapper;
using BLL.Interfaces;
using DAL.Entities;
using DAL.Interfaces;
using DTO.Talk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class TalkService : ITalkService
    {
        private readonly ISimpleChatRepository<Talk> _repository;
        private readonly IMapper _mapper;

        public TalkService(ISimpleChatRepository<Talk> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public void Create(TalkDTO item)
        {
            Talk talk = _mapper.Map<Talk>(item);
            _repository.Create(talk);
            _repository.Save();
        }

        public void Update(TalkDTO item)
        {
            Talk talk = _repository.GetById(item.Id);
            if (talk != null)
            {
                _mapper.Map(item, talk);
                _repository.Update(talk);
                _repository.Save();
            }
        }

        public void Delete(int id)
        {
            Talk talk = _repository.GetById(id);
            if (talk != null)
            {
                _repository.Delete(talk);
                _repository.Save();
            }
        }

        public TalkDTO GetById(int id)
        {
            Talk talk = _repository.GetById(id);
            TalkDTO result = _mapper.Map<TalkDTO>(talk);
            return result;
        }

        public TalkDTO GetByIdWithMembers(int id)
        {
            Talk talk = _repository.Get(filter: t => t.Id == id, includeProperties: "Members").FirstOrDefault();
            TalkDTO result = _mapper.Map<TalkDTO>(talk);
            return result;
        }

        public IEnumerable<TalkDTO> GetAll()
        {
            IEnumerable<Talk> talks = _repository.GetAll();
            IEnumerable<TalkDTO> result = _mapper.Map<IEnumerable<TalkDTO>>(talks);
            return result;
        }

        public IEnumerable<TalkDTO> GetAllWithMembers()
        {
            IEnumerable<Talk> talks = _repository.Get(includeProperties: "Members");
            IEnumerable<TalkDTO> result = _mapper.Map<IEnumerable<TalkDTO>>(talks);
            return result;
        }

        public IEnumerable<TalkDTO> GetPrivate()
        {
            IEnumerable<Talk> talks = _repository.Get(filter: t => t.IsPrivate, includeProperties: "Members");
            IEnumerable<TalkDTO> result = _mapper.Map<IEnumerable<TalkDTO>>(talks);
            return result;
        }

        public IEnumerable<TalkDTO> GetNonPrivate()
        {
            IEnumerable<Talk> talks = _repository.Get(filter: t => !t.IsPrivate, includeProperties: "Members");
            IEnumerable<TalkDTO> result = _mapper.Map<IEnumerable<TalkDTO>>(talks);
            return result;
        }
    }
}
