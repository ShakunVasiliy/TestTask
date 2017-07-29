using System;

namespace TestTaskApp.BLL.Infranstructure
{
    public class ValidationException: Exception
    {
        public string Property { get; protected set; }

        public ValidationException(string message, string property)
            : base(message)
        {
            this.Property = property;
        }
    }
}
