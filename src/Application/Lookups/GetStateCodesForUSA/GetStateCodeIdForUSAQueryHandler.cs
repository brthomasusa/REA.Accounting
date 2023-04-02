using REA.Accounting.Application.Interfaces.Messaging;
using REA.Accounting.Infrastructure.Persistence.Interfaces;
using REA.Accounting.Shared.Models.Lookups;
using REA.Accounting.SharedKernel.Utilities;

namespace REA.Accounting.Application.Lookups.GetStateCodesForUSA
{
    public sealed class GetStateCodeIdForUSAQueryHandler : IQueryHandler<GetStateCodeIdForUSARequest, List<StateCode>>
    {
        private readonly IReadRepositoryManager _repo;

        public GetStateCodeIdForUSAQueryHandler(IReadRepositoryManager repo)
            => _repo = repo;

        public async Task<Result<List<StateCode>>> Handle
        (
            GetStateCodeIdForUSARequest request,
            CancellationToken cancellationToken
        )
        {
            try
            {
                Result<List<StateCode>> result = await _repo.LookupsReadRepository.GetStateCodeIdForUSA();

                if (result.IsFailure)
                {
                    return Result<List<StateCode>>.Failure<List<StateCode>>(
                        new Error("GetStateCodeIdForUSAQueryHandler.Handle", result.Error.Message)
                    );
                }

                return result.Value;

            }
            catch (Exception ex)
            {
                return Result<List<StateCode>>.Failure<List<StateCode>>(
                    new Error("GetStateCodeIdForUSAQueryHandler.Handle", Helpers.GetExceptionMessage(ex))
                );
            }
        }
    }
}