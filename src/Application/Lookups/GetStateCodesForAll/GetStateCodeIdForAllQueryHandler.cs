using REA.Accounting.Application.Interfaces.Messaging;
using REA.Accounting.Infrastructure.Persistence.Interfaces;
using REA.Accounting.Shared.Models.Lookups;
using REA.Accounting.SharedKernel.Utilities;

namespace REA.Accounting.Application.Lookups.GetStateCodesForAll
{
    public sealed class GetStateCodeIdForAllQueryHandler : IQueryHandler<GetStateCodeIdForAllRequest, List<StateCode>>
    {
        private readonly IReadRepositoryManager _repo;

        public GetStateCodeIdForAllQueryHandler(IReadRepositoryManager repo)
            => _repo = repo;

        public async Task<Result<List<StateCode>>> Handle
        (
            GetStateCodeIdForAllRequest request,
            CancellationToken cancellationToken
        )
        {
            try
            {
                Result<List<StateCode>> result = await _repo.LookupsReadRepository.GetStateCodeIdForAll();

                if (result.IsFailure)
                {
                    return Result<List<StateCode>>.Failure<List<StateCode>>(
                        new Error("GetStateCodeIdForAllQueryHandler.Handle", result.Error.Message)
                    );
                }

                return result.Value;

            }
            catch (Exception ex)
            {
                return Result<List<StateCode>>.Failure<List<StateCode>>(
                    new Error("GetStateCodeIdForAllQueryHandler.Handle", Helpers.GetExceptionMessage(ex))
                );
            }
        }
    }
}