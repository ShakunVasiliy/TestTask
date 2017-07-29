using System;
using System.Collections.Generic;

using TestTaskApp.DAL.Entities;

namespace TestTaskApp.DAL.Interfaces
{
    public interface IManagerRepository : IRepository<Manager>
    {
        IEnumerable<ManagerStatistics> GetStatistics();
    }
}
