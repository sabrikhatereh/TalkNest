using FluentValidation;
using FluentValidation.Validators;
using System;
using System.Collections.Generic;
using System.Linq;

namespace TalkNest.Application.Validators
{
    public class ForbiddenWordsValidator<T> : PropertyValidator<T, string>
    {
        private readonly IEnumerable<string> _forbiddenWords;

        public ForbiddenWordsValidator(IEnumerable<string> forbiddenWords)
        {
            _forbiddenWords = forbiddenWords;
        }

        public override string Name => "ForbiddenWordsValidator";

        public override bool IsValid(ValidationContext<T> context, string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                return true;

            // Check if the value contains any forbidden words
            return !_forbiddenWords.Any(word =>
                value.Contains(word, StringComparison.OrdinalIgnoreCase));
        }

        protected override string GetDefaultMessageTemplate(string errorCode)
            => "'{PropertyName}' contains forbidden words.";
    }
}
