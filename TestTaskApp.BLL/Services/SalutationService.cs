using System;
using System.Collections.Generic;

using TestTaskApp.BLL.Interfases;

namespace TestTaskApp.BLL.Services
{
    public class SalutationService : ISalutationService
    {
        #region ISalutationService

        public IEnumerable<string> GetSalutations()
        {
            return new List<string> { "Mr.", "Ms." };
        }

        #endregion ISalutationService

        #region IDisposable

        public void Dispose()
        { }

        #endregion IDisposable
    }
}
