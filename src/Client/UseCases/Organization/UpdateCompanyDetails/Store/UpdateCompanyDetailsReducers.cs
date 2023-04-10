using System.Collections.Generic;
using Fluxor;

namespace REA.Accounting.Client.UseCases.Organization.UpdateCompanyDetails.Store
{
    public static class UpdateCompanyDetailsReducers
    {
        [ReducerMethod]
        public static UpdateCompanyDetailsState OnLoadingStateCodesSuccessAction
        (
            UpdateCompanyDetailsState state,
            LoadStateCodesSuccessAction action
        )
        {
            return state with
            {
                StateCodes = action.StateCodes
            };
        }

        [ReducerMethod]
        public static UpdateCompanyDetailsState OnLoadingStateCodesFailureAction
        (
            UpdateCompanyDetailsState state,
            LoadStateCodesFailureAction action
        )
        {
            return state with
            {
                ErrorMessage = action.ErrorMessage
            };
        }

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
        public static UpdateCompanyDetailsState OnSetInitializationFlag
        (
            UpdateCompanyDetailsState state,
            SetUpdateInitializeFlagAction action
        )
        {
            return state with
            {
                Initialized = action.IsInitialized
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
                Initialized = true,
                Loading = false,
                Submitting = false,
                Submitted = false,
                ErrorMessage = string.Empty,
                CommandModel = action.CommandModel,
            };
        }

        [ReducerMethod]
        public static UpdateCompanyDetailsState OnGetCompanyDetailsInitializeFailureMessageAction
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

        [ReducerMethod(typeof(UpdateCompanyDetailsSubmitAction))]
        public static UpdateCompanyDetailsState OnSubmit(UpdateCompanyDetailsState state)
        {
            return state with
            {
                Submitting = true
            };
        }

        [ReducerMethod]
        public static UpdateCompanyDetailsState OnSubmitSuccess
        (
            UpdateCompanyDetailsState state,
            UpdateCompanyDetailsSubmitSuccessAction _
        )
        {
            return state with
            {
                Initialized = false,
                Submitting = false,
                Submitted = true,
            };
        }

        [ReducerMethod]
        public static UpdateCompanyDetailsState OnSubmitFailure
        (
            UpdateCompanyDetailsState state,
            UpdateCompanyDetailsSubmitFailureAction action
        )
        {
            return state with
            {
                Submitting = false,
                Submitted = false,
                ErrorMessage = action.ErrorMessage
            };
        }
    }
}