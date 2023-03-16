using REA.Accounting.Application.Interfaces.Messaging;
using REA.Accounting.SharedKernel.Utilities;

namespace REA.Accounting.Application.Organization.UpdateCompany
{
    public sealed class UpdateCompanyBusinessRuleValidator : CommandValidator<UpdateCompanyCommand>
    {
        public override async Task<Result> Validate(UpdateCompanyCommand command)
        {
            return await Task.FromResult(Result.Success());
        }
    }
}