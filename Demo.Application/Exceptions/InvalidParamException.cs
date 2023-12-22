using System.Text.Json;

namespace Demo.Application.Exceptions
{
    /// <summary>
    /// Thrown as an application wrapper of <see cref="ArgumentNullException"/> and <see cref="ArgumentException"/>
    /// </summary>
    public class InvalidParamException : ArgumentException
    {
        public InvalidParamException(string paramName, object? paramValue) 
            : base(SetMessageField(paramValue), paramName)
        {
        }

        private static string SetMessageField(object? paramValue) => paramValue is null ? "Parameter null" : $"Invalid parameter value : {JsonSerializer.Serialize(paramValue)}";
    }
}
