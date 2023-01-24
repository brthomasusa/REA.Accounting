using MediatR;
using Carter;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

using REA.Accounting.Application.HumanResources.CreateEmployee;
using REA.Accounting.Application.HumanResources.GetEmployeeById;
using REA.Accounting.SharedKernel.Utilities;

namespace REA.Accounting.Presentation.HumanResources
{
    public class HumanResourcesModule : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("api/employees/{id}", async (int id, ISender sender) =>
            {
                return await sender.Send(new GetEmployeeByIdQuery(EmployeeID: id))
                    is GetEmployeeByIdResponse response ? Results.Ok(response) : Results.NotFound();
            });

            app.MapPost("api/employees/create", async (CreateEmployeeCommand cmd, ISender sender) =>
            {
                int newPrimaryKey = await sender.Send(cmd);
                return Results.Created($"api/employees/{newPrimaryKey}", cmd);
            });
        }
    }
}