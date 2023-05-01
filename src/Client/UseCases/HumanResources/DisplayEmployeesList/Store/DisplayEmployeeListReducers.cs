using Fluxor;

namespace REA.Accounting.Client.UseCases.HumanResources.DisplayEmployeesList.Store
{
    public static class DisplayEmployeeListReducers
    {
        [ReducerMethod(typeof(SetLoadingFlagAction))]
        public static DisplayEmployeeListState OnGetEmployeeListAction
        (
            DisplayEmployeeListState state
        )
        {
            return state with
            {
                Loading = true
            };
        }

        [ReducerMethod]
        public static DisplayEmployeeListState OnGetEmployeeListSuccessAction
        (
            DisplayEmployeeListState state,
            GetEmployeeListSuccessAction action
        )
        {
            return state with
            {
                EmployeeList = action.Employees,
                MetaData = action.MetaData,
                Loading = false,
                Initialized = true,
                SearchTerm = action.SearchTerm
            };
        }

        [ReducerMethod]
        public static DisplayEmployeeListState OnGetEmployeeListFailureMessageAction
        (
            DisplayEmployeeListState state,
            GetEmployeeListFailureAction action
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