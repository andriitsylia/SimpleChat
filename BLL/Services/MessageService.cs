using AutoMapper;
using BLL.Interfaces;
using DAL.Entities;
using DAL.Interfaces;
using DTO.Message;

namespace BLL.Services
{
    public class MessageService : IMessageService
    {
        private readonly ISimpleChatRepository<Message> _repository;
        private readonly IMapper _mapper;

        public MessageService(ISimpleChatRepository<Message> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public void Create(MessageDTO item)
        {
            Message message = _mapper.Map<Message>(item);
            _repository.Create(message);
            _repository.Save();
        }

        public void Update(MessageDTO item)
        {
            Message message = _repository.GetById(item.Id);
            if (message != null)
            {
                _mapper.Map(item, message);
                _repository.Update(message);
                _repository.Save();
            }
        }

        public void Delete(int id)
        {
            Message message = _repository.GetById(id);
            if (message != null)
            {
                _repository.Delete(message);
                _repository.Save();
            }
        }

        public MessageDTO GetById(int id)
        {
            Message message = _repository.GetById(id);
            MessageDTO result = _mapper.Map<MessageDTO>(message);
            return result;
        }

        public MessageDTO GetByIdWithMemberAndTalks(int id)
        {
            Message message = _repository.Get(filter: m => m.Id == id, includeProperties: "Member, Talk").FirstOrDefault();
            MessageDTO result = _mapper.Map<MessageDTO>(message);
            return result;
        }

        public IEnumerable<MessageDTO> GetAll()
        {
            IEnumerable<Message> messages = _repository.GetAll();
            IEnumerable<MessageDTO> result = _mapper.Map<IEnumerable<MessageDTO>>(messages);
            return result;
        }

        public IEnumerable<MessageDTO> GetAllWithMembersAndTalks()
        {
            IEnumerable<Message> messages = _repository.Get(includeProperties: "Member, Talk");
            IEnumerable<MessageDTO> result = _mapper.Map<IEnumerable<MessageDTO>>(messages);
            return result;
        }
    }
}
