using FluentValidation;

namespace REA.Accounting.Application.HumanResources.CreateEmployee
{
    public class CreateEmployeeCommandDataValidator : AbstractValidator<CreateEmployeeCommand>
    {
        public CreateEmployeeCommandDataValidator()
        {
            RuleFor(employee => employee.EmployeeID)
                                        .Equal(0)
                                        .WithMessage("ID for new employee should be zero.");

            RuleFor(employee => employee.PersonType)
                                        .NotEmpty()
                                        .WithMessage("Missing person type flag; this is required.")
                                        .Equal("EM").WithMessage("Valid person type is EM for employee");

            RuleFor(employee => employee.Title)
                                        .MaximumLength(8).WithMessage("Title cannot be longer than 8 characters");

            RuleFor(employee => employee.FirstName)
                                        .NotEmpty().WithMessage("Employee first name; this is required.")
                                        .MaximumLength(50).WithMessage("Employee first name cannot be longer than 50 characters");

            RuleFor(employee => employee.LastName)
                                        .NotEmpty().WithMessage("Employee last name; this is required.")
                                        .MaximumLength(50).WithMessage("Employee last name cannot be longer than 50 characters");

            RuleFor(employee => employee.MiddleName)
                                        .MaximumLength(50).WithMessage("Employee middle name cannot be longer than 50 characters");

            RuleFor(employee => employee.Suffix)
                                        .MaximumLength(10).WithMessage("Suffix cannot be longer than 10 characters");

            RuleFor(employee => employee.EmailPromotion)
                                        .Must(emailPromo => emailPromo >= 0 && emailPromo <= 2)
                                        .WithMessage("Valid email promo codes are 0, 1, or 2.");

            RuleFor(employee => employee.NationalID)
                                        .NotEmpty().WithMessage("National ID; this is required.")
                                        .MaximumLength(50).WithMessage("National ID cannot be longer than 15 characters");

            RuleFor(employee => employee.Login)
                                        .NotEmpty().WithMessage("Employee login; this is required.")
                                        .MaximumLength(50).WithMessage("Employee login cannot be longer than 256 characters");

            RuleFor(employee => employee.JobTitle)
                                        .NotEmpty().WithMessage("Employee job title; this is required.")
                                        .MaximumLength(50).WithMessage("Employee job title cannot be longer than 50 characters");

            RuleFor(employee => employee.BirthDate)
                                        .NotEmpty().WithMessage("Employee birth date is required.")
                                        .GreaterThanOrEqualTo(new DateTime(1930, 1, 1)).WithMessage("Birth date must be on or after 1-1-1930.")
                                        .LessThanOrEqualTo(DateTime.Now.AddYears(-18)).WithMessage("Employee must be at least 18 years old.");

            RuleFor(employee => employee.MaritalStatus)
                                        .NotEmpty().WithMessage("Employee marital status is required.")
                                        .Must(status => status.ToUpper() == "S" || status.ToUpper() == "M")
                                        .WithMessage("Marital status must be S for single or M for married.");

            RuleFor(employee => employee.Gender)
                                        .NotEmpty().WithMessage("Employee gender is required.")
                                        .Must(gender => gender.ToUpper() == "F" || gender.ToUpper() == "M")
                                        .WithMessage("Gender must be F for female or M for male.");

            RuleFor(employee => employee.HireDate)
                                        .NotEmpty().WithMessage("Employee hire date is required.")
                                        .Must(hireDate => hireDate >= new DateTime(1996, 7, 1))
                                        .WithMessage("Hire date must be on or after July 1, 1996.");

            RuleFor(employee => employee.Vacation)
                                        .Must(vacation => vacation >= -40 && vacation <= 240)
                                        .WithMessage("Valid vacation hours are between negative 40 and 240.");

            RuleFor(employee => employee.SickLeave)
                                        .Must(sickleave => sickleave >= 0 && sickleave <= 120)
                                        .WithMessage("Valid sick leave hours are between 0 and 120.");

            RuleFor(employee => employee.PayRate)
                                        .Must(pay => pay >= 6.50M && pay <= 200M)
                                        .WithMessage("Valid pay rate is between $6.50 and $200.00 per hour.");

            RuleFor(employee => employee.PayFrequency)
                                        .Must(freq => freq == 1 || freq == 2)
                                        .WithMessage("Valid pay frequencies are 1 for monthly and 2 for biweekly.");

            RuleFor(employee => employee.DepartmentID)
                                        .GreaterThan(0)
                                        .WithMessage("A department ID is required.");

            RuleFor(employee => employee.ShiftID)
                                        .GreaterThan(0)
                                        .WithMessage("A shift ID is required.");

            RuleFor(employee => employee.AddressType)
                                        .Equal(2)
                                        .WithMessage("Address type for an employee is 2 (Home).");

            RuleFor(employee => employee.AddressLine1)
                                        .NotEmpty().WithMessage("Address line 1; this is required.")
                                        .MaximumLength(60).WithMessage("Address line cannot be longer than 60 characters");

            RuleFor(employee => employee.AddressLine2)
                                        .MaximumLength(60).WithMessage("Address line cannot be longer than 60 characters");

            RuleFor(employee => employee.City)
                                        .NotEmpty().WithMessage("City name; this is required.")
                                        .MaximumLength(30).WithMessage("City name cannot be longer than 30 characters");

            RuleFor(employee => employee.StateCode)
                                        .Must(code => code >= 1 && code <= 181)
                                        .WithMessage("Valid state province codes are between 1 and 181.");

            RuleFor(employee => employee.PostalCode)
                                        .NotEmpty().WithMessage("Postal code; this is required.")
                                        .MaximumLength(15).WithMessage("Post code cannot be longer than 15 characters");

            RuleFor(employee => employee.EmailAddress).EmailAddress();

            RuleFor(employee => employee.PhoneNumber).Matches("^\\+?\\d{1,4}?[-.\\s]?\\(?\\d{1,3}?\\)?[-.\\s]?\\d{1,4}[-.\\s]?\\d{1,4}[-.\\s]?\\d{1,9}$");

            RuleFor(employee => employee.PhoneNumberType)
                                        .Must(phType => phType >= 1 && phType <= 3)
                                        .WithMessage("Valid phone number types are 1, 2, and 3 (cell, home, work).");
        }
    }
}