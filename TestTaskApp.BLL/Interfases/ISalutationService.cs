using System;
using System.Collections.Generic;

namespace TestTaskApp.BLL.Interfases
{
    public interface ISalutationService : IDisposable
    {
        IEnumerable<string> GetSalutations();
    }
}
