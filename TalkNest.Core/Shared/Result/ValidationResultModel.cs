using FluentValidation.Results;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.Json;

namespace TalkNest.Core.Shared.Result
{
    public class ValidationResultModel
    {
        public int StatusCode { get; set; } = (int)HttpStatusCode.BadRequest;
        public string Message { get; set; } = "Validation Failed.";

        public List<ValidationError> Errors { get; }

        public ValidationResultModel(ValidationResult[] validationResult = null)
        {
            Errors = validationResult
             .SelectMany(result => result.Errors)
             .Where(f => f != null)
             .Select(error => new ValidationError(error.PropertyName, error.ErrorMessage))
                .ToList();
        }

        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }
    }
}