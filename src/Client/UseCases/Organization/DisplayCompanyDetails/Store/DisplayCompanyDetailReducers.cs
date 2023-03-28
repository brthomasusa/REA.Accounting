using System.Collections.Generic;
using Fluxor;

namespace REA.Accounting.Client.UseCases.Organization.DisplayCompanyDetails.Store
{
    public static class DisplayCompanyDetailReducers
    {
        [ReducerMethod(typeof(SetLoadingFlagAction))]
        public static CompanyDetailState OnLoadingCompanyDetailsAction
        (
            CompanyDetailState state
        )
        {
            return state with
            {
                Loading = true
            };
        }

        [ReducerMethod]
        public static CompanyDetailState OnGetCompanyDetailsSuccessAction
        (
            CompanyDetailState state,
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
        public static CompanyDetailState OnGetCompanyDetailsFailureMessageAction
        (
            CompanyDetailState state,
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