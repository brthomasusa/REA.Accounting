using Grpc.Core;
using gRPC.Contracts.Shared;
using gRPC.Contracts.Organization;
using MapsterMapper;
using MediatR;

using REA.Accounting.Application.Organization.GetCompany;
using REA.Accounting.Application.Organization.GetCompanyDepartments;
using REA.Accounting.Application.Organization.GetCompanyShifts;
using REA.Accounting.Application.Organization.UpdateCompany;
using REA.Accounting.Infrastructure.Persistence.Queries.Organization;
using REA.Accounting.SharedKernel.Utilities;

namespace REA.Accounting.Server.Contracts
{
    public sealed class CompanyContractService : CompanyContract.CompanyContractBase
    {
        private readonly ISender _sender;
        private readonly IMapper _mapper;

        public CompanyContractService(ISender sender, IMapper mapper)
            => (_sender, _mapper) = (sender, mapper);

        public override async Task<CompanyDetail> GetCompanyDetailById(ItemRequest request, ServerCallContext context)
        {
            Result<GetCompanyDetailByIdResponse> result = await _sender.Send(new GetCompanyDetailByIdRequest(CompanyID: request.Id));
            return _mapper.Map<CompanyDetail>(result.Value);
        }

        public override async Task<CompanyCommand> GetCompanyCommandById(ItemRequest request, ServerCallContext context)
        {
            Result<GetCompanyCommandByIdResponse> result = await _sender.Send(new GetCompanyCommandByIdRequest(CompanyID: request.Id));
            return _mapper.Map<CompanyCommand>(result.Value);
        }

        public async override Task<grpc_GetCompanyDepartmentsResponse> GetCompanyDepartments(grpc_PagingParameters request, ServerCallContext context)
        {
            PagingParameters pagingParameters = new(request.PageNumber, request.PageSize);
            GetCompanyDepartmentsRequest requestParameter = new(PagingParameters: pagingParameters);
            Result<PagedList<GetCompanyDepartmentsResponse>> getDepartmentsResult = await _sender.Send(requestParameter);

            grpc_GetCompanyDepartmentsResponse grpcResponse = new();
            List<grpc_Department> grpcDepartmentList = new();

            getDepartmentsResult.Value.ForEach(dept => grpcDepartmentList.Add(_mapper.Map<grpc_Department>(dept)));

            grpcResponse.GrpcDepartments.AddRange(grpcDepartmentList);
            grpcResponse.GrpcMetaData.Add(Helpers.LoadMetaData(getDepartmentsResult.Value.MetaData));

            return grpcResponse;
        }

        public override async Task<grpc_GetCompanyDepartmentsResponse> GetCompanyDepartmentsBySearchTerm(grpc_StringSearchTerm request, ServerCallContext context)
        {
            PagingParameters pagingParameters = new(request.PageNumber, request.PageSize);
            GetCompanyDepartmentsSearchByNameRequest requestParameter = new(DepartmentName: request.Criteria, PagingParameters: pagingParameters);
            Result<PagedList<GetCompanyDepartmentsResponse>> getDepartmentsResult = await _sender.Send(requestParameter);

            grpc_GetCompanyDepartmentsResponse grpcResponse = new();
            List<grpc_Department> grpcDepartmentList = new();

            getDepartmentsResult.Value.ForEach(dept => grpcDepartmentList.Add(_mapper.Map<grpc_Department>(dept)));

            grpcResponse.GrpcDepartments.AddRange(grpcDepartmentList);
            grpcResponse.GrpcMetaData.Add(Helpers.LoadMetaData(getDepartmentsResult.Value.MetaData));

            return grpcResponse;
        }

        public async override Task<grpc_GetCompanyShiftsResponse> GetCompanyShifts(grpc_PagingParameters request, ServerCallContext context)
        {
            PagingParameters pagingParameters = new(request.PageNumber, request.PageSize);
            GetCompanyShiftsRequest result = new(PagingParameters: pagingParameters);
            Result<PagedList<GetCompanyShiftsResponse>> getShiftsResult = await _sender.Send(result);

            grpc_GetCompanyShiftsResponse grpcResponse = new();
            List<grpc_Shift> grpcShiftList = new();

            getShiftsResult.Value.ForEach(shift => grpcShiftList.Add(_mapper.Map<grpc_Shift>(shift)));

            grpcResponse.GrpcShifts.AddRange(grpcShiftList);
            grpcResponse.GrpcMetaData.Add(Helpers.LoadMetaData(getShiftsResult.Value.MetaData));

            return grpcResponse;
        }

        public override async Task<GenericResponse> Update(CompanyCommand request, ServerCallContext context)
        {
            UpdateCompanyCommand cmd = _mapper.Map<UpdateCompanyCommand>(request);

            Result<int> result = await _sender.Send(cmd);

            if (result.IsFailure)
                return new GenericResponse { Success = false };

            return new GenericResponse { Success = true };
        }
    }
}