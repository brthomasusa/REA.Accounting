using Grpc.Core;
using GoogleDateTime = Google.Protobuf.WellKnownTypes.Timestamp;
using gRPC.Contracts.Shared;
using gRPC.Contracts.Organization;
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
        public CompanyContractService(ISender sender) => _sender = sender;

        public override async Task<CompanyDetail> GetCompanyDetailById(ItemRequest request, ServerCallContext context)
        {
            Result<GetCompanyDetailByIdResponse> result = await _sender.Send(new GetCompanyDetailByIdRequest(CompanyID: request.Id));

            return new CompanyDetail()
            {
                Id = result.Value.CompanyID,
                CompanyName = result.Value.CompanyName,
                LegalName = result.Value.LegalName,
                Ein = result.Value.EIN,
                CompanyWebSite = result.Value.WebsiteUrl,
                MailAddressLine1 = result.Value.MailAddressLine1,
                MailAddressLine2 = result.Value.MailAddressLine2 ?? string.Empty,
                MailCity = result.Value.MailCity,
                MailStateProvinceCode = result.Value.MailStateProvinceCode,
                MailPostalCode = result.Value.MailPostalCode,
                DeliveryAddressLine1 = result.Value.DeliveryAddressLine1,
                DeliveryAddressLine2 = result.Value.DeliveryAddressLine2 ?? string.Empty,
                DeliveryCity = result.Value.DeliveryCity,
                DeliveryStateProvinceCode = result.Value.DeliveryStateProvinceCode,
                DeliveryPostalCode = result.Value.DeliveryPostalCode,
                Telephone = result.Value.Telephone,
                Fax = result.Value.Fax
            };
        }

        public override async Task<CompanyCommand> GetCompanyCommandById(ItemRequest request, ServerCallContext context)
        {
            Result<GetCompanyCommandByIdResponse> result = await _sender.Send(new GetCompanyCommandByIdRequest(CompanyID: request.Id));

            return new CompanyCommand()
            {
                Id = result.Value.CompanyID,
                CompanyName = result.Value.CompanyName,
                LegalName = result.Value.LegalName,
                Ein = result.Value.EIN,
                CompanyWebSite = result.Value.WebsiteUrl,
                MailAddressLine1 = result.Value.MailAddressLine1,
                MailAddressLine2 = result.Value.MailAddressLine2 ?? string.Empty,
                MailCity = result.Value.MailCity,
                MailStateProvinceID = result.Value.MailStateProvinceID,
                MailPostalCode = result.Value.MailPostalCode,
                DeliveryAddressLine1 = result.Value.DeliveryAddressLine1,
                DeliveryAddressLine2 = result.Value.DeliveryAddressLine2 ?? string.Empty,
                DeliveryCity = result.Value.DeliveryCity,
                DeliveryStateProvinceID = result.Value.DeliveryStateProvinceID,
                DeliveryPostalCode = result.Value.DeliveryPostalCode,
                Telephone = result.Value.Telephone,
                Fax = result.Value.Fax
            };
        }

        public async override Task<grpc_GetCompanyDepartmentsResponse> GetCompanyDepartments(grpc_PagingParameters request, ServerCallContext context)
        {
            PagingParameters pagingParameters = new(request.PageNumber, request.PageSize);
            GetCompanyDepartmentsRequest result = new(PagingParameters: pagingParameters);
            Result<PagedList<GetCompanyDepartmentsResponse>> getDepartmentsResult = await _sender.Send(result);

            grpc_GetCompanyDepartmentsResponse grpcResponse = new();
            List<grpc_Department> grpcDepartmentList = new();

            getDepartmentsResult.Value.ForEach(dept => grpcDepartmentList.Add(new grpc_Department()
            {
                DepartmentId = dept.DepartmentID,
                Name = dept.Name,
                GroupName = dept.GroupName,
                ModifiedDate = GoogleDateTime.FromDateTime(dept.ModifiedDate.ToUniversalTime())
            }));

            grpcResponse.GrpcDepartments.AddRange(grpcDepartmentList);
            grpcResponse.GrpcMetaData.Add(LoadMetaData(getDepartmentsResult.Value.MetaData));

            return grpcResponse;
        }

        public override async Task<grpc_GetCompanyDepartmentsResponse> GetCompanyDepartmentsBySearchTerm(grpc_StringSearchTerm request, ServerCallContext context)
        {
            PagingParameters pagingParameters = new(request.PageNumber, request.PageSize);
            GetCompanyDepartmentsSearchByNameRequest requestParameter = new(DepartmentName: request.Criteria, PagingParameters: pagingParameters);
            Result<PagedList<GetCompanyDepartmentsResponse>> getDepartmentsResult = await _sender.Send(requestParameter);

            grpc_GetCompanyDepartmentsResponse grpcResponse = new();
            List<grpc_Department> grpcDepartmentList = new();

            getDepartmentsResult.Value.ForEach(dept => grpcDepartmentList.Add(new grpc_Department()
            {
                DepartmentId = dept.DepartmentID,
                Name = dept.Name,
                GroupName = dept.GroupName,
                ModifiedDate = GoogleDateTime.FromDateTime(dept.ModifiedDate.ToUniversalTime())
            }));

            grpcResponse.GrpcDepartments.AddRange(grpcDepartmentList);
            grpcResponse.GrpcMetaData.Add(LoadMetaData(getDepartmentsResult.Value.MetaData));

            return grpcResponse;
        }

        public async override Task<grpc_GetCompanyShiftsResponse> GetCompanyShifts(grpc_PagingParameters request, ServerCallContext context)
        {
            PagingParameters pagingParameters = new(request.PageNumber, request.PageSize);
            GetCompanyShiftsRequest result = new(PagingParameters: pagingParameters);
            Result<PagedList<GetCompanyShiftsResponse>> getShiftsResult = await _sender.Send(result);

            grpc_GetCompanyShiftsResponse grpcResponse = new();
            List<grpc_Shift> grpcShiftList = new();

            getShiftsResult.Value.ForEach(shift => grpcShiftList.Add(new grpc_Shift()
            {
                ShiftId = shift.ShiftID,
                Name = shift.Name,
                StartTime = shift.StartTime,
                EndTime = shift.EndTime,
                ModifiedDate = GoogleDateTime.FromDateTime(shift.ModifiedDate.ToUniversalTime())
            }));

            grpcResponse.GrpcShifts.AddRange(grpcShiftList);
            grpcResponse.GrpcMetaData.Add(LoadMetaData(getShiftsResult.Value.MetaData));

            return grpcResponse;
        }

        public override async Task<GenericResponse> Update(CompanyCommand request, ServerCallContext context)
        {
            UpdateCompanyCommand cmd = new
            (
                request.Id,
                request.CompanyName,
                request.LegalName,
                request.Ein,
                request.CompanyWebSite,
                request.MailAddressLine1,
                request.MailAddressLine2,
                request.MailCity,
                request.MailStateProvinceID,
                request.MailPostalCode,
                request.DeliveryAddressLine1,
                request.DeliveryAddressLine2,
                request.DeliveryCity,
                request.DeliveryStateProvinceID,
                request.DeliveryPostalCode,
                request.Telephone,
                request.Fax
            );

            Result<int> result = await _sender.Send(cmd);

            if (result.IsFailure)
                return new GenericResponse { Success = false };

            return new GenericResponse { Success = true };
        }

        private static Dictionary<string, int> LoadMetaData(MetaData data)
        {
            Dictionary<string, int> metaData = new()
            {
                { "TotalCount", data.TotalCount },
                { "PageSize", data.PageSize },
                { "CurrentPage", data.CurrentPage },
                { "TotalPages", data.TotalPages }
            };

            return metaData;
        }
    }
}