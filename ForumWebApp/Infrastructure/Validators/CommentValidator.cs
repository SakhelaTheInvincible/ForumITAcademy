using FluentValidation;
using Forum.Application.MainComments;
using ForumWebApp.Infrastructure.Localization;

namespace ForumWebApp.Infrastructure.Validators
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
