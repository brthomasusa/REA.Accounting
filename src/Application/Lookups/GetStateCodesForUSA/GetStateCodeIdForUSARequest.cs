using REA.Accounting.Application.Interfaces.Messaging;
using REA.Accounting.Shared.Models.Lookups;

namespace REA.Accounting.Application.Lookups.GetStateCodesForUSA
{
    public sealed record GetStateCodeIdForUSARequest() : IQuery<List<StateCode>>;
}