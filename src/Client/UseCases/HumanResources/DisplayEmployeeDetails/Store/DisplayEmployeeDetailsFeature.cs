using Fluxor;

namespace REA.Accounting.Client.UseCases.HumanResources.DisplayEmployeeDetails.Store
{
    public class DisplayEmployeeDetailsFeature : Feature<DisplayEmployeeDetailsState>
    {
        public override string GetName() => "DisplayEmployeeDetails";

        protected override DisplayEmployeeDetailsState GetInitialState() =>
            new()
            {
                Initialized = false,
                Loading = false,
                ErrorMessage = string.Empty,
                DetailsModel = null,
                EmployeeID = 0
            };
    }
}