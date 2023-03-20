using Grpc.Core;
using gRPC.Contracts;
using gRPC.Contracts.Clients;
using gRPC.Contracts.Clients.Organization;

namespace REA.Accounting.Server.Contracts
{
    public interface ICompanyContractService
    {
        AsyncUnaryCall<GenericResponse> UpdateAsync(CompanyCommand request, Metadata? headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default);
        AsyncUnaryCall<CompanyCommand> GetCompanyCommandByIdAsync(ItemRequest request, Metadata? headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default);
        AsyncUnaryCall<CompanyDetail> GetCompanyDetailsByIdAsync(ItemRequest request, Metadata? headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default);
    }
}