using FluentValidation;
using Forum.Application.MainUsers;
using ForumWebApp.Infrastructure.Localization;

namespace ForumWebApp.Infrastructure.Validators
{
    public class UserCreateValidator : AbstractValidator<UserCreateModel>
    {
        public UserCreateValidator()
        {
            RuleFor(x => x.UserName)
                .NotEmpty().WithMessage(ErrorMessages.UserNameNotEmpty)
                .MaximumLength(15).WithMessage(ErrorMessages.UserNameLength);

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage(ErrorMessages.UserPasswordNotEmpty)
                .MinimumLength(6).WithMessage(ErrorMessages.UserPasswordLength);

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage(ErrorMessages.UserEmail)
                .EmailAddress().WithMessage(ErrorMessages.UserEmail);
        }

    }

    public class UserUpdateValidator : AbstractValidator<UserUpdateModel>
    {
        public UserUpdateValidator()
        {
            RuleFor(x => x.UserName)
                .NotEmpty().WithMessage(ErrorMessages.UserNameNotEmpty)
                .MaximumLength(15).WithMessage(ErrorMessages.UserNameLength);

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage(ErrorMessages.UserPasswordNotEmpty)
                .MinimumLength(6).WithMessage(ErrorMessages.UserPasswordLength);

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage(ErrorMessages.UserEmail)
                .EmailAddress().WithMessage(ErrorMessages.UserEmail);
        }
    }
}
