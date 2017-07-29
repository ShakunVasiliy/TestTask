using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestTaskApp.BLL.Infranstructure
{
    public class SortParameter
    {
        public string FieldName { get; set; }
        public SortType Type { get; set; }

        public SortParameter()
            : this ("Name")
        { }

        public SortParameter(string fieldName)
            : this(fieldName, SortType.DESC)
        { }

        public SortParameter(string fieldName, SortType sortType)
        {
            this.FieldName = fieldName;
            this.Type = sortType;
        }

        public void TogleType()
        {
            switch (Type)
            {
                case SortType.ASC:
                    Type = SortType.DESC;
                    break;
                case SortType.DESC:
                    Type = SortType.ASC;
                    break;
            }
        }

        public override int GetHashCode()
        {
            return (FieldName + Type.ToString()).GetHashCode() ;
        }

        public override bool Equals(object obj)
        {
            var parameter = obj as SortParameter;

            if (parameter == null) return false;

            if (this.FieldName != parameter.FieldName) return false;
            if (this.Type != parameter.Type) return false;

            return true;
        }

        public static IEnumerable<SortParameter> CreateSortParameters(SortParameter currentSortParameter, IEnumerable<string> fieldNames)
        {
            var sortParameters = new List<SortParameter>();

            foreach (var fieldName in fieldNames)
            {
                sortParameters.Add(new SortParameter(fieldName, SortType.ASC));
            }

            SortParameter nextSortParameter = sortParameters.Find(sp => sp.FieldName == currentSortParameter.FieldName);

            if (nextSortParameter != null)
            {
                if (nextSortParameter.Type == currentSortParameter.Type)
                {
                    nextSortParameter.TogleType();
                }
            }

            return sortParameters;
        }
    }
}
