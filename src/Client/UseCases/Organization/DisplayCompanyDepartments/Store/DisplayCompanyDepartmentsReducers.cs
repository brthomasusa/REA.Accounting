using Fluxor;

namespace REA.Accounting.Client.UseCases.Organization.DisplayCompanyDepartments.Store
{
    public static class DisplayCompanyDepartmentsReducers
    {
        [ReducerMethod(typeof(SetLoadingFlagAction))]
        public static DisplayCompanyDepartmentsState OnLoadingCompanyDepartmentsAction
        (
            DisplayCompanyDepartmentsState state
        )
        {
            return state with
            {
                Loading = true
            };
        }

        [ReducerMethod]
        public static DisplayCompanyDepartmentsState OnGetCompanyDepartmentsSuccessAction
        (
            DisplayCompanyDepartmentsState state,
            GetDepartmentsSuccessAction action
        )
        {
            return state with
            {
                DepartmentList = action.Departments,
                MetaData = action.MetaData,
                Loading = false,
                Initialized = true
            };
        }

        [ReducerMethod]
        public static DisplayCompanyDepartmentsState OnGetNextPageOfDataSuccessAction
        (
            DisplayCompanyDepartmentsState state,
            GetNextPageOfDataSuccessAction action
        )
        {
            return state with
            {
                DepartmentList = action.Departments,
                MetaData = action.MetaData
            };
        }

        [ReducerMethod]
        public static DisplayCompanyDepartmentsState OnGetCompanyDepartmentsFailureMessageAction
        (
            DisplayCompanyDepartmentsState state,
            GetDepartmentsFailureAction action
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