using AutoMapper;
using BLL.Interfaces;
using DTO.Talk;
using SimpleChat.Interfaces;

namespace SimpleChat.Controllers
{
    public class TalkController : ITalkController
    {
        private readonly ISimpleChatService<TalkDTO> _service;
        private readonly IMapper _mapper;

        public TalkController(ISimpleChatService<TalkDTO> service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        public void Create(TalkModel talk)
        {
            TalkDTO talkDTO = _mapper.Map<TalkDTO>(talk);
            _service.Create(talkDTO);
        }

        public void Delete(int id)
        {
            TalkDTO talkDTO = _service.GetById(id);
            if (talkDTO != null)
            {
                _service.Delete(id);
            }
        }

        public IEnumerable<TalkModel> GetAll()
        {
            IEnumerable<TalkModel> result = _mapper.Map<IEnumerable<TalkModel>>(_service.GetAll());
            return result;
        }

        public TalkModel GetById(int id)
        {
            TalkDTO talkDTO = _service.GetById(id);
            TalkModel result = _mapper.Map<TalkModel>(talkDTO);
            return result;
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
    }
}
