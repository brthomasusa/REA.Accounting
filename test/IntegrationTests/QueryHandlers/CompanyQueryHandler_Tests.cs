using REA.Accounting.Application.Organization.GetCompany;
using REA.Accounting.Application.Organization.GetCompanyDepartments;
using REA.Accounting.Application.Organization.GetCompanyShifts;
using REA.Accounting.Infrastructure.Persistence.Interfaces;
using REA.Accounting.Infrastructure.Persistence.Repositories;
using REA.Accounting.Infrastructure.Persistence.Queries.Organization;
using REA.Accounting.SharedKernel.Utilities;

namespace REA.Accounting.IntegrationTests.QueryHandlers
{
    public class CompanyQueryHandler_Tests : TestBase
    {
        private readonly IReadRepositoryManager _repository;

        public CompanyQueryHandler_Tests()
            => _repository = new ReadRepositoryManager(_dapperCtx, new NullLogger<ReadRepositoryManager>());

        [Fact]
        public async Task Handle_GetCompanyDetailByIdQueryHandler_ShouldSucceed()
        {
            GetCompanyDetailByIdRequest request = new(CompanyID: 1);
            GetCompanyDetailByIdQueryHandler handler = new(_repository);

            Result<GetCompanyDetailByIdResponse> response = await handler.Handle(request, new CancellationToken());

            Assert.True(response.IsSuccess);
        }

        [Fact]
        public async Task Handle_GetCompanyDetailByIdQueryHandler_ShouldFail_WithInvalidCompanyID()
        {
            GetCompanyDetailByIdRequest request = new(CompanyID: 2);
            GetCompanyDetailByIdQueryHandler handler = new(_repository);

            Result<GetCompanyDetailByIdResponse> response = await handler.Handle(request, new CancellationToken());

            Assert.True(response.IsFailure);
        }

        [Fact]
        public async Task Handle_GetCompanyCommandByIdQueryHandler_ShouldSucceed()
        {
            GetCompanyCommandByIdRequest request = new(CompanyID: 1);
            GetCompanyCommandByIdQueryHandler handler = new(_repository);

            Result<GetCompanyCommandByIdResponse> response = await handler.Handle(request, new CancellationToken());

            Assert.True(response.IsSuccess);
        }

        [Fact]
        public async Task Handle_GetCompanyCommandByIdQueryHandler_ShouldFail_WithInvalidCompanyID()
        {
            GetCompanyCommandByIdRequest request = new(CompanyID: 2);
            GetCompanyCommandByIdQueryHandler handler = new(_repository);

            Result<GetCompanyCommandByIdResponse> response = await handler.Handle(request, new CancellationToken());

            Assert.True(response.IsFailure);
        }

        [Fact]
        public async Task Handle_GetCompanyDepartmentsQueryHandler_ShouldSucceed()
        {
            PagingParameters pagingParameters = new(1, 10);
            GetCompanyDepartmentsRequest request = new(PagingParameters: pagingParameters);
            GetCompanyDepartmentsQueryHandler handler = new(_repository);

            Result<PagedList<GetCompanyDepartmentsResponse>> response = await handler.Handle(request, new CancellationToken());

            Assert.True(response.IsSuccess);

            int departments = response.Value.Count;
            Assert.Equal(16, departments);
        }

        [Fact]
        public async Task Handle_GetCompanyShiftsQueryHandler_ShouldSucceed()
        {
            PagingParameters pagingParameters = new(1, 10);
            GetCompanyShiftsRequest request = new(PagingParameters: pagingParameters);
            GetCompanyShiftsQueryHandler handler = new(_repository);

            Result<PagedList<GetCompanyShiftsResponse>> response = await handler.Handle(request, new CancellationToken());

            Assert.True(response.IsSuccess);

            int shifts = response.Value.Count;
            Assert.Equal(3, shifts);
        }
    }
}