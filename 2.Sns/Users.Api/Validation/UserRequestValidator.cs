using FluentValidation;
using System.Text.RegularExpressions;
using Users.Api.Contracts.Requests;

namespace Users.Api.Validation
{
    public partial class UserRequestValidator : AbstractValidator<UserRequest>
    {
        public UserRequestValidator()
        {
            RuleFor(x => x.Name)
            .Matches(UsernameRegex());

            RuleFor(x => x.Email)
                .EmailAddress();

            RuleFor(x => x.Surname)
                .Matches(UsernameRegex());

            RuleFor(x => x.DateOfBirth)
                .LessThan(DateTime.Now)
                .WithMessage("Your date of birth cannot be in the future");

        }

        [GeneratedRegex("^[a-z\\d](?:[a-z\\d]|-(?=[a-z\\d])){0,38}$", RegexOptions.IgnoreCase | RegexOptions.Compiled, "en-US")]
        private static partial Regex UsernameRegex();
    }
}
