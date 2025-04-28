using FluentValidation;
using System.Collections.Generic;

namespace TalkNest.Application.Validators
{
    public static class CustomValidatorsExtensions
    {
        public static IRuleBuilderOptions<T, string> MustNotContainForbiddenWords<T>(
            this IRuleBuilder<T, string> ruleBuilder, IEnumerable<string> forbiddenWords)
        {
            return ruleBuilder.SetValidator(new ForbiddenWordsValidator<T>(forbiddenWords));
        }
    }
}
