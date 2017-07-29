using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TestTaskApp.BLL.Infranstructure;

namespace TestTaskApp.BLL.Interfases
{
    public interface IServiceWithSort<T>
        where T : class
    {
        IEnumerable<T> GetWithSortBy(SortParameter sortParameter);
    }
}
