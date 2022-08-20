using AutoMapper;
using BLL.Interfaces;
using DTO.Talk;
using SimpleChat.Interfaces;

namespace SimpleChat.Controllers
{
    public class TalkController : ITalkController
    {
        private readonly IPrivateTalkService _service;
        private readonly IMapper _mapper;

        public TalkController(IPrivateTalkService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        public void Create(TalkModel talk)
        {
            TalkDTO talkDTO = _mapper.Map<TalkDTO>(talk);
            _service.Create(talkDTO);
        }

        public void Update(TalkModel talk)
        {
            TalkDTO talkDTO = _service.GetById(talk.Id);
            if (talkDTO != null)
            {
                _mapper.Map(talk, talkDTO);
                _service.Update(talkDTO);
            }
        }

        public void Delete(int id)
        {
            TalkDTO talkDTO = _service.GetById(id);
            if (talkDTO != null)
            {
                _service.Delete(id);
            }
        }

        public TalkModel GetById(int id)
        {
            TalkDTO talkDTO = _service.GetById(id);
            TalkModel result = _mapper.Map<TalkModel>(talkDTO);
            return result;
        }

        public IEnumerable<TalkModel> GetAll()
        {
            IEnumerable<TalkModel> result = _mapper.Map<IEnumerable<TalkModel>>(_service.GetAll());
            return result;
        }

        public IEnumerable<TalkModel> GetPrivate()
        {
            IEnumerable<TalkModel> result = _mapper.Map<IEnumerable<TalkModel>>(_service.GetPrivate());
            return result;
        }

        public IEnumerable<TalkModel> GetNonPrivate()
        {
            IEnumerable<TalkModel> result = _mapper.Map<IEnumerable<TalkModel>>(_service.GetNonPrivate());
            return result;
        }


    }
}
