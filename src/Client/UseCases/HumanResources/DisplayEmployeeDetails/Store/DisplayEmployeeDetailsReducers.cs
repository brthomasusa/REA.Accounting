using Fluxor;

namespace REA.Accounting.Client.UseCases.HumanResources.DisplayEmployeeDetails.Store
{
    public static class DisplayEmployeeDetailsReducers
    {
        [ReducerMethod(typeof(SetLoadingFlagAction))]
        public static DisplayEmployeeDetailsState OnLoadingEmployeeDetailsAction
        (
            DisplayEmployeeDetailsState state
        )
        {
            return state with
            {
                Loading = true
            };
        }

        [ReducerMethod]
        public static DisplayEmployeeDetailsState OnSetInitializeFlagAction
        (
            DisplayEmployeeDetailsState state,
            SetInitializeFlagAction action
        )
        {
            return state with
            {
                Initialized = action.IsInitialized
            };
        }

        [ReducerMethod]
        public static DisplayEmployeeDetailsState OnGetEmployeeDetailsSuccessAction
        (
            DisplayEmployeeDetailsState state,
            GetEmployeeDetailsSuccessAction action
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
        public static DisplayEmployeeDetailsState OnGetEmployeeDetailsFailureMessageAction
        (
            DisplayEmployeeDetailsState state,
            GetEmployeeDetailsFailureAction action
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