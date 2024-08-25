using FluentValidation;
using Forum.Application.MainComments;
using Forum.API.Infrastructure.Localization;

namespace Forum.API.Infrastructure.Validators
{
    public class CommentCreateValidator : AbstractValidator<CommentUpdateModel>
    {
        public CommentCreateValidator()
        {
            RuleFor(x => x.Content)
                .NotEmpty().WithMessage(ErrorMessages.ContentNotEmpty);
        }

    }

    public class CommentUpdateValidator : AbstractValidator<CommentUpdateModel>
    {
        public CommentUpdateValidator()
        {
            RuleFor(x => x.Content)
                .NotEmpty().WithMessage(ErrorMessages.ContentNotEmpty);
        }

    }
}
