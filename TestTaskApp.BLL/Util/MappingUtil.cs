using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AutoMapper;

namespace TestTaskApp.BLL.Util
{
    public static class MappingUtil
    {
        private static void InitializeMap<TSource, TTurget>()
        {
            Mapper.Initialize(cfg => cfg.CreateMap<TSource, TTurget>());
        }

        public static TTurget MapToInstance<TSource, TTurget>(TSource source)
        {
            InitializeMap<TSource, TTurget>();

            return Mapper.Map<TSource, TTurget>(source);
        }

        public static IEnumerable<TTurget> MapToCollection<TSource, TTurget>(IEnumerable<TSource> sourceCollection)
        {
            InitializeMap<TSource, TTurget>();

            return Mapper.Map<IEnumerable<TSource>, List<TTurget>>(sourceCollection);
        }
    }
}
