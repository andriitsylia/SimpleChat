﻿namespace BLL.Interfaces
{
    public interface ISimpleChatService<T> where T : class
    {
        void Create(T item);
        void Update(T item);
        void Delete(int id);
        T GetById(int id);
        IEnumerable<T> GetAll();
    }
}
