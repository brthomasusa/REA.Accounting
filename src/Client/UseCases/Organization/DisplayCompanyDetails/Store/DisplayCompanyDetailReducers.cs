using Fluxor;

namespace REA.Accounting.Client.UseCases.Organization.DisplayCompanyDetails.Store
{
    public static class DisplayCompanyDetailReducers
    {
        [ReducerMethod(typeof(SetLoadingFlagAction))]
        public static DisplayCompanyDetailState OnLoadingCompanyDetailsAction
        (
            DisplayCompanyDetailState state
        )
        {
            return state with
            {
                Loading = true
            };
        }

        [ReducerMethod]
        public static DisplayCompanyDetailState OnSetInitializeFlagAction
        (
            DisplayCompanyDetailState state,
            SetDisplayInitializeFlagAction action
        )
        {
            return state with
            {
                Initialized = action.IsInitialized
            };
        }

        [ReducerMethod]
        public static DisplayCompanyDetailState OnGetCompanyDetailsSuccessAction
        (
            DisplayCompanyDetailState state,
            DisplayCompanyDetailsSuccessAction action
        )
        {
            return state with
            {
                DetailsModel = action.DetailModel,
                Loading = false,
                Initialized = true
            };
        }

        [ReducerMethod]
        public static DisplayCompanyDetailState OnGetCompanyDetailsFailureMessageAction
        (
            DisplayCompanyDetailState state,
            DisplayCompanyDetailsFailureMessageAction action
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