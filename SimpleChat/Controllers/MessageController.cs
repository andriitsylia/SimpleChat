using AutoMapper;
using BLL.Interfaces;
using DTO.Message;
using DTO.Message;
using SimpleChat.Interfaces;

namespace SimpleChat.Controllers
{
    public class MessageController : IMessageController
    {
        private readonly IMessageService _service;
        private readonly IMapper _mapper;

        public MessageController(IMessageService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        public void Create(MessageModel message)
        {
            MessageDTO messageDTO = _mapper.Map<MessageDTO>(message);
            _service.Create(messageDTO);
        }

        public void Update(MessageModel message)
        {
            MessageDTO messageDTO = _service.GetById(message.Id);
            if (messageDTO != null)
            {
                _mapper.Map(message, messageDTO);
                _service.Update(messageDTO);
            }
        }

        public void Delete(int id)
        {
            MessageDTO messageDTO = _service.GetById(id);
            if (messageDTO != null)
            {
                _service.Delete(id);
            }
        }

        public MessageModel GetById(int id)
        {
            MessageDTO messageDTO = _service.GetById(id);
            MessageModel result = _mapper.Map<MessageModel>(messageDTO);
            return result;
        }

        public IEnumerable<MessageModel> GetAll()
        {
            IEnumerable<MessageModel> result = _mapper.Map<IEnumerable<MessageModel>>(_service.GetAll());
            return result;
        }
    }
}
