using FluentValidation;
using REA.Accounting.Shared.Models.Organization;

namespace REA.Accounting.Client.UseCases.Organization.UpdateCompanyDetails.Validators
{
    public class CompanyCommandModelValidator : AbstractValidator<CompanyCommandModel>
    {
        public CompanyCommandModelValidator()
        {
            RuleFor(company => company.Id)
                                      .GreaterThan(0)
                                      .WithMessage("An ID is required in order to locate the company to be updated.");

            RuleFor(company => company.CompanyName)
                                      .NotEmpty().WithMessage("Company name; this is required.")
                                      .MaximumLength(50).WithMessage("Company name cannot be longer than 50 characters");

            RuleFor(company => company.LegalName)
                                      .MaximumLength(50).WithMessage("Legal name cannot be longer than 50 characters");

            RuleFor(company => company.EIN)
                                      .NotEmpty().WithMessage("Employer identification number; this is required.")
                                      .Matches(@"^\d{9}|\d{2}-\d{7}$").WithMessage("Valid EIN has 9 digits (with, or without, a dash between the 2nd and 3rd digit)");

            RuleFor(company => company.CompanyWebSite)
                                      .Matches(@"^(?:http(s)?:\/\/)?[\w.-]+(?:\.[\w\.-]+)+[\w\-\._~:/?#[\]@!\$&'\(\)\*\+,;=.]+$").WithMessage("Invalid url.")
                                      .MaximumLength(50).WithMessage("Website uri cannot be longer than 50 characters");

            RuleFor(company => company.MailAddressLine1)
                                      .NotEmpty().WithMessage("Mail address line 1; this is required.")
                                      .MaximumLength(60).WithMessage("Mail address line cannot be longer than 60 characters");

            RuleFor(company => company.MailAddressLine2)
                                      .MaximumLength(60).WithMessage("Mail address line cannot be longer than 60 characters");

            RuleFor(company => company.MailCity)
                                      .NotEmpty().WithMessage("Mail city; this is required.")
                                      .MaximumLength(30).WithMessage("Mail city cannot be longer than 30 characters");

            RuleFor(company => company.MailStateProvinceID)
                                      .GreaterThan(0)
                                      .WithMessage("A state province ID is required.");

            RuleFor(company => company.MailPostalCode)
                                      .NotEmpty().WithMessage("Mail postal code; this is required.")
                                      .MaximumLength(15).WithMessage("Mail postal code cannot be longer than 15 characters");

            RuleFor(company => company.DeliveryAddressLine1)
                                      .NotEmpty().WithMessage("Delivery address line 1; this is required.")
                                      .MaximumLength(60).WithMessage("Delivery address line cannot be longer than 60 characters");

            RuleFor(company => company.DeliveryAddressLine2)
                                      .MaximumLength(60).WithMessage("Delivery address line cannot be longer than 60 characters");

            RuleFor(company => company.DeliveryCity)
                                      .NotEmpty().WithMessage("Delivery city; this is required.")
                                      .MaximumLength(30).WithMessage("Delivery city cannot be longer than 30 characters");

            RuleFor(company => company.DeliveryStateProvinceID)
                                      .GreaterThan(0)
                                      .WithMessage("A state province ID is required.");

            RuleFor(company => company.DeliveryPostalCode)
                                      .NotEmpty().WithMessage("Delivery postal code; this is required.")
                                      .MaximumLength(15).WithMessage("Delivery postal code cannot be longer than 15 characters");

            RuleFor(company => company.Telephone)
                                      .NotEmpty().WithMessage("Telephone number; this is required.")
                                      .MaximumLength(25).WithMessage("Telephone number cannot be longer than 25 characters");

            RuleFor(company => company.Fax)
                                      .NotEmpty().WithMessage("Fax; this is required.")
                                      .MaximumLength(25).WithMessage("Fax cannot be longer than 25 characters");
        }
    }
}