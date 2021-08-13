using FluentValidation.Results;
using System.Collections.Generic;

namespace Core.Extensions
{
    public class ValidationErrorDetails : ErrorDetails
    {
        public List<string> ValidationErrors { get; set; }
    }
}