
using TalkNest.Core.Shared.Result;

namespace TalkNest.Application.Exceptions
{
    public class ValidationException : CustomException
    {
        public ValidationException(ValidationResultModel validationResultModel)
        {
            ValidationResultModel = validationResultModel;
        }
        public ValidationResultModel ValidationResultModel { get; }
    }
}

