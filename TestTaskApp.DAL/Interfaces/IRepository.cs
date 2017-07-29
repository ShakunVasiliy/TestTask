using System;
using System.Collections.Generic;

namespace TestTaskApp.DAL.Interfaces
{
    public interface IRepository<T>
        where T : class
    {
        IEnumerable<T> GetAll();
    }
}
