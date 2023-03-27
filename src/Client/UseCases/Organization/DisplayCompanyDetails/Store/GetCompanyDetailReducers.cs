using Fluxor;

namespace REA.Accounting.Client.UseCases.Organization.DisplayCompanyDetails.Store
{
    public static class GetCompanyDetailReducers
    {
        [ReducerMethod(typeof(SetInitializeAction))]
        public static CompanyDetailState OnSetInitializeAction
        (
            CompanyDetailState state
        )
        {
            return state with
            {
                Initialized = true
            };
        }

        [ReducerMethod]
        public static CompanyDetailState OnLoadingCompanyDetailsAction
        (
            CompanyDetailState state,
            SetLoadingFlagAction action
        )
        {
            return state with
            {
                Loading = action.Loading
            };
        }

        [ReducerMethod]
        public static CompanyDetailState OnGetCompanyDetailsSuccessAction
        (
            CompanyDetailState state,
            GetCompanyDetailsSuccessAction action
        )
        {
            return state with
            {
                DetailsModel = action.DetailModel,
                Loading = false
            };
        }

        [ReducerMethod]
        public static CompanyDetailState OnGetCompanyDetailsFailureMessageAction
        (
            CompanyDetailState state,
            GetCompanyDetailsFailureMessageAction action
        )
        {
            return state with
            {
                ErrorMessage = action.ErrorMessage,
                Loading = false
            };
        }
    }
}