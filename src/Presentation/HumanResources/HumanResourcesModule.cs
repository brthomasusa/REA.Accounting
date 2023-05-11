using Carter;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using REA.Accounting.Application.HumanResources.CreateEmployee;
using REA.Accounting.Application.HumanResources.DeleteEmployee;
using REA.Accounting.Application.HumanResources.GetEmployeeDetailsById;
using REA.Accounting.Application.HumanResources.UpdateEmployee;
using REA.Accounting.Infrastructure.Persistence.Queries.HumanResources;
using REA.Accounting.Shared.Models.HumanResources;
using REA.Accounting.SharedKernel.Utilities;

namespace REA.Accounting.Presentation.HumanResources
{
    public sealed class HumanResourcesModule : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("api/employees/allinfo/{id}", async (int id, ISender sender) =>
            {
                Result<EmployeeDetailReadModel> result =
                    await sender.Send(new GetEmployeeDetailsByIdWithAllInfoRequest(EmployeeID: id));

                if (result.IsSuccess)
                    return Results.Ok(result.Value);

                return Results.Problem(result.Error);
            });

            app.MapGet("api/employees/filterbylastname", async (QueryParameters.FilterEmployeesByNameParameters parameters, ISender sender) =>
            {
                PagingParameters pagingParameters = new(parameters.PageNumber, parameters.PageSize);
                GetEmployeeListItemsRequest request = new(LastName: parameters.LastName!, PagingParameters: pagingParameters);

                Result<PagedList<EmployeeListItemReadModel>> result = await sender.Send(request);

                if (result.IsSuccess)
                    return Results.Ok(result.Value);

                return Results.Problem(result.Error);
            });

            app.MapPost("api/employees/create", async (CreateEmployeeCommand cmd, ISender sender) =>
            {
                Result<int> result = await sender.Send(cmd);

                if (result.IsSuccess)
                {
                    return Results.Created($"api/employees/allinfo/{result.Value}", new GetEmployeeDetailsByIdWithAllInfoRequest(EmployeeID: result.Value));
                }

                return Results.Problem(result.Error);
            });

            app.MapPut("api/employees/update", async (UpdateEmployeeCommand cmd, ISender sender) =>
            {
                Result<int> result = await sender.Send(cmd);

                if (result.IsSuccess)
                    return Results.Ok();

                return Results.Problem(result.Error);
            });

            app.MapDelete("api/employees/delete", async ([FromBody] DeleteEmployeeCommand cmd, ISender sender) =>
            {
                Result<int> result = await sender.Send(cmd);

                if (result.IsSuccess)
                    return Results.Ok();

                return Results.Problem(result.Error);
            });
        }
    }
}