using MediatR;
using Carter;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using System.Reflection;

using REA.Accounting.Application.Organization.GetCompany;
using REA.Accounting.Application.Organization.UpdateCompany;
using REA.Accounting.Application.Organization.GetCompanyDepartments;
using REA.Accounting.Application.Organization.GetCompanyShifts;
using REA.Accounting.Infrastructure.Persistence.Queries.Organization;
using REA.Accounting.SharedKernel.Utilities;

namespace REA.Accounting.Presentation.Organization
{
    public sealed class CompanyModule : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("api/companies/details/{id}", async (int id, ISender sender) =>
            {
                Result<GetCompanyDetailByIdResponse> result = await sender.Send(new GetCompanyDetailByIdRequest(CompanyID: id));

                if (result.IsSuccess)
                    return Results.Ok(result.Value);

                return Results.Problem(result.Error);
            });

            app.MapGet("api/companies/command/{id}", async (int id, ISender sender) =>
            {
                Result<GetCompanyCommandByIdResponse> result = await sender.Send(new GetCompanyCommandByIdRequest(CompanyID: id));

                if (result.IsSuccess)
                    return Results.Ok(result.Value);

                return Results.Problem(result.Error);
            });

            app.MapGet("api/companies/departments", async (QueryParameters parameters, ISender sender) =>
            {
                PagingParameters pagingParameters = new(parameters.PageNumber, parameters.PageSize);
                GetCompanyDepartmentsRequest request = new(PagingParameters: pagingParameters);
                Result<PagedList<GetCompanyDepartmentsResponse>> result = await sender.Send(request);

                if (result.IsSuccess)
                    return Results.Ok(result.Value);

                return Results.Problem(result.Error);
            });

            app.MapGet("api/companies/shifts", async (QueryParameters parameters, ISender sender) =>
            {
                PagingParameters pagingParameters = new(parameters.PageNumber, parameters.PageSize);
                GetCompanyShiftsRequest request = new(PagingParameters: pagingParameters);
                Result<PagedList<GetCompanyShiftsResponse>> result = await sender.Send(request);

                if (result.IsSuccess)
                    return Results.Ok(result.Value);

                return Results.Problem(result.Error);
            });

            app.MapPut("api/companies/update", async (UpdateCompanyCommand cmd, ISender sender) =>
            {
                Result<int> result = await sender.Send(cmd);

                if (result.IsSuccess)
                    return Results.Ok();

                return Results.Problem(result.Error);
            });
        }
    }
}
