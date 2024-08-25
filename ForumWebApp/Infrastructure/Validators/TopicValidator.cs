using FluentValidation;
using Forum.Application.MainTopics;
using ForumWebApp.Infrastructure.Localization;


namespace ForumWebApp.Infrastructure.Validators
{
    public class TopicCreateValidator : AbstractValidator<TopicCreateModel>
    {
        public TopicCreateValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage(ErrorMessages.TitleNotEmpty)
                .MaximumLength(20).WithMessage(ErrorMessages.TitleLength);

            RuleFor(x => x.Content)
                .NotEmpty().WithMessage(ErrorMessages.ContentNotEmpty);
        }
    }
    public class TopicUpdateValidator : AbstractValidator<TopicUpdateModel>
    {
        public TopicUpdateValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage(ErrorMessages.TitleNotEmpty)
                .MaximumLength(20).WithMessage(ErrorMessages.TitleLength);

            RuleFor(x => x.Content)
                .NotEmpty().WithMessage(ErrorMessages.ContentNotEmpty);
        }
    }
}
