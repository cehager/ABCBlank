using Common.Requests;
using FluentValidation;

namespace BankUI.Pages.Banking.Validator
{
    public class CreateAccountHolderValidator : AbstractValidator<CreateAccountHolder>
    {
       public CreateAccountHolderValidator()
        { 
            RuleFor(x => x.FirstName).Must(p => !string.IsNullOrEmpty(p)).WithMessage("First Name is required");
            RuleFor(x => x.LastName).Must(p => !string.IsNullOrEmpty(p)).WithMessage("Last Name is required");
            RuleFor(x => x.DateOfBirth)
                .Must(p => p.HasValue).WithMessage("Date of Birth is required")
                .LessThanOrEqualTo(DateTime.Now.AddYears(-18)).WithMessage("You must be 18 years or older to open an account")
                .LessThanOrEqualTo(System.DateTime.Now.AddDays(1)).WithMessage("Date of Birth cannot be in the future");
              
            RuleFor(x => x.Email).EmailAddress().MaximumLength(60).WithMessage("Email too long. 60 characters maximum.")
                .Must(p => !string.IsNullOrEmpty(p)).WithMessage("Email is required");
            RuleFor(x => x.ContactNumber).Must(p => !string.IsNullOrEmpty(p)).WithMessage("Phone number is required")
                .MaximumLength(16).WithMessage("Phone number too long. 16 characters maximum.")
                .MinimumLength(7).WithMessage("Phone number is too short. 7 characters minimum, include area code.");

            //.Matches(@"^(\d{10})$").WithMessage("Phone number is invalid");


        }

        public Func<object, string, Task<IEnumerable<string>>> ValidateValue => async (requestModel, propertyName) =>
        {
            var result = await ValidateAsync(ValidationContext<CreateAccountHolder>
                .CreateWithOptions((CreateAccountHolder)requestModel, options => options.IncludeProperties(propertyName)));

            if (result.IsValid)
            {
                return Array.Empty<string>();
            }
            return result.Errors
               // .Where(x => x.PropertyName == propertyName)
                .Select(x => x.ErrorMessage);
        };
    }
}
