using MediatR;
using Carter;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

using REA.Accounting.Application.HumanResources.CreateEmployee;
using REA.Accounting.Application.HumanResources.DeleteEmployee;
using REA.Accounting.Application.HumanResources.GetEmployeeById;
using REA.Accounting.Application.HumanResources.UpdateEmployee;
using REA.Accounting.SharedKernel.Utilities;

namespace REA.Accounting.Presentation.HumanResources
{
    public class HumanResourcesModule : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("api/employees/{id}", async (int id, ISender sender) =>
            {
                OperationResult<GetEmployeeByIdResponse> getResult = await sender.Send(new GetEmployeeByIdQuery(EmployeeID: id));
                if (getResult.Success)
                    return Results.Ok(getResult.Result);

                return Results.Problem(getResult.NonSuccessMessage!);
            });

            app.MapPost("api/employees/create", async (CreateEmployeeCommand cmd, ISender sender) =>
            {
                OperationResult<int> postResult = await sender.Send(cmd);

                if (postResult.Success)
                    return Results.Created($"api/employees/{postResult.Result}", cmd);

                return Results.Problem(postResult.NonSuccessMessage!);
            });

            app.MapPut("api/employees/update", async (UpdateEmployeeCommand cmd, ISender sender) =>
            {
                OperationResult<int> putResult = await sender.Send(cmd);

                if (putResult.Success)
                    return Results.Ok();

                return Results.Problem(putResult.NonSuccessMessage!);
            });

            app.MapDelete("api/employees/delete", async ([FromBody] DeleteEmployeeCommand cmd, ISender sender) =>
            {
                OperationResult<int> deleteResult = await sender.Send(cmd);
                if (deleteResult.Success)
                    return Results.Ok();

                return Results.Problem(deleteResult.NonSuccessMessage!);
            });
        }
    }
}