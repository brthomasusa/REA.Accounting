using MediatR;
using Carter;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

using REA.Accounting.Application.Lookups.GetStateCodesForAll;
using REA.Accounting.Application.Lookups.GetStateCodesForUSA;
using REA.Accounting.Shared.Models.Lookups;
using REA.Accounting.SharedKernel.Utilities;

namespace REA.Accounting.Presentation.Lookups
{
    public sealed class LookupsModule : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("api/lookups/statecodes/all", async (ISender sender) =>
            {
                Result<List<StateCode>> result = await sender.Send(new GetStateCodeIdForAllRequest());

                if (result.IsSuccess)
                    return Results.Ok(result.Value);

                return Results.Problem(result.Error);
            });

            app.MapGet("api/lookups/statecodes/usa", async (ISender sender) =>
            {
                Result<List<StateCode>> result = await sender.Send(new GetStateCodeIdForUSARequest());

                if (result.IsSuccess)
                    return Results.Ok(result.Value);

                return Results.Problem(result.Error);
            });
        }
    }
}