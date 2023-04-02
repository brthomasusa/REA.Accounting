using REA.Accounting.Application.Interfaces.Messaging;
using REA.Accounting.Shared.Models.Lookups;

namespace REA.Accounting.Application.Lookups.GetStateCodesForAll
{
    public sealed record GetStateCodeIdForAllRequest() : IQuery<List<StateCode>>;
}