using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace REA.Accounting.Client.UseCases.Organization.UpdateCompanyDetails.Store
{
    public record UpdateCompanyDetailsSubmitFailureAction(string ErrorMessage);
}