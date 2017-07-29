using System;

namespace TestTaskApp.DAL.Interfaces
{
    public interface ICRUDRepository<T>
        where T : class
    {
        T Read(int id);
        void Create(T item);
        void Update(T item);
        void Delete(int id);
    }
}
