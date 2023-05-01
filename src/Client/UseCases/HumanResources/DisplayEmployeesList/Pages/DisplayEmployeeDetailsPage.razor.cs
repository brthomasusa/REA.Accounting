using Microsoft.AspNetCore.Components;
using Fluxor;

namespace REA.Accounting.Client.UseCases.HumanResources.DisplayEmployeesList.Pages
{
    public partial class DisplayEmployeeDetailsPage
    {
        [Parameter] public int EmployeeId { get; set; }
    }
}