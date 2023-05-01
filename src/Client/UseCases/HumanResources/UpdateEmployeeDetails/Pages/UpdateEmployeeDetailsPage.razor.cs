using Fluxor;
using Microsoft.AspNetCore.Components;
// using REA.Accounting.Client.UseCases.HumanResources.UpdateEmployeeDetails.Store;
using REA.Accounting.Shared.Models.HumanResources;

namespace REA.Accounting.Client.UseCases.HumanResources.UpdateEmployeeDetails.Pages
{
    public partial class UpdateEmployeeDetailsPage
    {
        [Parameter] public int EmployeeId { get; set; }
    }
}