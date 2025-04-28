using FluentValidation;
using TalkNest.Application.Validators;
using TalkNest.Core.Abstractions.Services;
using TalkNest.Core.Models;

namespace TalkNest.Application.Posts.Commands.CreatePost
{
    public class CreatePostCommandValidator : AbstractValidator<CreatePostCommand>
    {
        private readonly IForbidWords _forbidWords;
        public CreatePostCommandValidator(IForbidWords forbidWords)
        {
            _forbidWords = forbidWords;
            var forbidWordsList = _forbidWords.LoadForbidWords().Result;
            RuleFor(x => x.Title)
            .NotNull()
            .NotEmpty()
            .WithMessage("Title is required")
            .MaximumLength(100)
            .WithMessage("Title cannot exceed 100 characters.")
            .MustNotContainForbiddenWords(forbidWordsList);

            RuleFor(x => x.Content).NotNull()
           .NotEmpty()
           .WithMessage("Content is required")
           .MaximumLength(500)
           .WithMessage("Content cannot exceed 500 characters.")
           .MustNotContainForbiddenWords(forbidWordsList);

            RuleFor(x => x.Id)
                .NotNull()
                .NotEmpty()
                .WithMessage("Id is required.");

        }
    }
}
