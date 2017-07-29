using System;
using System.Collections.Generic;

using TestTaskApp.BLL.DTO;

namespace TestTaskApp.BLL.Interfases
{
    public interface IManagerService : IServiceWithSort<ManagerDTO>, IDisposable
    {
        IEnumerable<ManagerDTO> GetManagers();
    }
}
