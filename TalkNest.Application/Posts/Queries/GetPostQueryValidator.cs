using FluentValidation;

namespace TalkNest.Application.Posts.Queries
{
    public class GetPostQueryValidator : AbstractValidator<GetPostQuery>
    {
        public GetPostQueryValidator()
        {

            RuleFor(x => x.Id)
            .NotNull()
            .NotEmpty()
            .WithMessage("Id is required");
        }
    }
}