using System;

namespace TestTaskApp.BLL.Interfases
{
    public interface IValidator<T>
        where T : class
    {
        void Validate(T obj);
    }
}
