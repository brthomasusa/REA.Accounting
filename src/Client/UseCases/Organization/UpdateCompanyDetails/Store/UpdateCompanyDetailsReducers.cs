using Fluxor;

namespace REA.Accounting.Client.UseCases.Organization.UpdateCompanyDetails.Store
{
    public static class UpdateCompanyDetailsReducers
    {
        [ReducerMethod(typeof(SetLoadingFlagAction))]
        public static UpdateCompanyDetailsState OnLoadingCompanyDetailsAction
        (
            UpdateCompanyDetailsState state
        )
        {
            return state with
            {
                Loading = true
            };
        }

        [ReducerMethod]
        public static UpdateCompanyDetailsState OnUpdateCompanyDetailsInitializeSuccessAction
        (
            UpdateCompanyDetailsState state,
            UpdateCompanyDetailsInitializeSuccessAction action
        )
        {
            return state with
            {
                CommandModel = action.CommandModel,
                StateCodes = action.StateCodes,
                Loading = false,
                Initialized = true
            };
        }

        [ReducerMethod]
        public static UpdateCompanyDetailsState OnGetCompanyDetailsFailureMessageAction
        (
            UpdateCompanyDetailsState state,
            UpdateCompanyDetailsInitializeFailureAction action
        )
        {
            return state with
            {
                ErrorMessage = action.ErrorMessage,
                Loading = false,
                Initialized = false
            };
        }
    }
}