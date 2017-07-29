using System;
using System.Collections.Generic;

using TestTaskApp.BLL.Infranstructure;

namespace TestTaskApp.BLL.Interfases
{
    public interface ISorter<T>
        where T : class
    {
        IEnumerable<T> Sort(IEnumerable<T> source, SortParameter parameter);
    }
}
