using Grpc.Core;
using gRPC.Contracts.Shared;
using gRPC.Contracts.HumanResources;
using MapsterMapper;
using MediatR;

using REA.Accounting.Application.HumanResources.GetEmployeeDetailsById;
using REA.Accounting.Infrastructure.Persistence.Queries.HumanResources;
using REA.Accounting.SharedKernel.Utilities;
using REA.Accounting.Shared.Models.HumanResources;

namespace REA.Accounting.Server.Contracts
{
    public sealed class EmployeeContractService : EmployeeContract.EmployeeContractBase
    {
        private readonly ISender _sender;
        private readonly IMapper _mapper;

        public EmployeeContractService(ISender sender, IMapper mapper)
            => (_sender, _mapper) = (sender, mapper);

        public override async Task<grpc_EmployeeDetailResponse> GetEmployeeDetailsByIdWithAllInfo(ItemRequest request, ServerCallContext context)
        {
            Result<EmployeeDetailReadModel> result =
                await _sender.Send(new GetEmployeeDetailsByIdWithAllInfoRequest(EmployeeID: request.Id));

            return _mapper.Map<grpc_EmployeeDetailResponse>(result.Value);
        }

        public override async Task<grpc_EmployeeListItemsResponse> GetEmployeeListItemsByLastName(grpc_StringSearchTerm request, ServerCallContext context)
        {
            PagingParameters pagingParameters = new(request.PageNumber, request.PageSize);
            GetEmployeeListItemsRequest requestParameter = new(LastName: request.Criteria, PagingParameters: pagingParameters);
            Result<PagedList<EmployeeListItemReadModel>> result = await _sender.Send(requestParameter);

            grpc_EmployeeListItemsResponse grpcResponse = new();
            List<grpc_EmployeeListItem> grpcEmployeeListItems = new();

            result.Value.ForEach(employee => grpcEmployeeListItems.Add(_mapper.Map<grpc_EmployeeListItem>(employee)));

            grpcResponse.GrpcEmployees.AddRange(grpcEmployeeListItems);
            grpcResponse.GrpcMetaData.Add(Helpers.LoadMetaData(result.Value.MetaData));

            return grpcResponse;
        }
    }
}