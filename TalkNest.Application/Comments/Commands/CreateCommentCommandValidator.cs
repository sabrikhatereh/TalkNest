using FluentValidation;
using TalkNest.Core.Abstractions.Services;
using TalkNest.Application.Validators;

namespace TalkNest.Application.Comments
{
    public class CreateCommentCommandValidator : AbstractValidator<CreateCommentCommand>
    {
        private readonly IForbidWords _forbidWords;
        public CreateCommentCommandValidator(IForbidWords forbidWords)
        {
            _forbidWords = forbidWords;
            var forbidWordsList = _forbidWords.LoadForbidWords().Result;
            RuleFor(x => x.Text)
            .NotNull()
            .NotEmpty()
            .WithMessage("comment text is required")
            .MaximumLength(100)
            .WithMessage("comment text exceed 200 characters.")
            .MustNotContainForbiddenWords(forbidWordsList);

            RuleFor(x => x.PostId)
                .NotNull()
                .NotEmpty()
                .WithMessage("Post Id is required.");

        }
    }
}
