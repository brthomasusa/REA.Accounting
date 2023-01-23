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
                GetEmployeeByIdResponse result = await sender.Send(new GetEmployeeByIdQuery(EmployeeID: id));

                return Results.Ok(result);
            });
        }
    }
}