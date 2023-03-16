using REA.Accounting.Application.Organization.UpdateCompany;
using REA.Accounting.Core.Organization;
using REA.Accounting.Infrastructure.Persistence.Interfaces;
using REA.Accounting.SharedKernel.Utilities;
using REA.Accounting.UnitTests.TestHelpers;

namespace REA.Accounting.UnitTests.CommandHandlers.Organization
{
    public class CompanyCommandHandler_Tests
    {
        private readonly Mock<IWriteRepositoryManager> _repositoryMock;

        public CompanyCommandHandler_Tests() => _repositoryMock = new();

        [Fact]
        public async Task Handle_Should_ReturnSuccess_WhenCompanyID_IsValid()
        {
            // Arrange
            UpdateCompanyCommand command = OrganizationTestData.GetUpdateCompanyCommandWithValidData();

            _repositoryMock.Setup
            (
                repoMgr => repoMgr.CompanyAggregateRepository.GetByIdAsync(It.IsAny<int>(), It.IsAny<bool>())
            ).ReturnsAsync(OrganizationTestData.GetCompanyResultWithValidData());

            _repositoryMock.Setup
            (
                repoMgr => repoMgr.CompanyAggregateRepository.Update(It.IsAny<Company>())
            ).ReturnsAsync(0);

            UpdateCompanyCommandHandler handler = new(_repositoryMock.Object);

            // Act
            Result<int> result = await handler.Handle(command, default);

            // Assert
            Assert.True(result.IsSuccess);

            _repositoryMock.Verify(
                x => x.CompanyAggregateRepository.Update(It.IsAny<Company>()),
                Times.Once);
        }

        [Fact]
        public async Task Handle_Should_ReturnFailure_WhenCompanyID_IsInvalid()
        {
            // Arrange
            UpdateCompanyCommand command = OrganizationTestData.GetUpdateCompanyCommandWithValidData();
            Result<Company> getCompanyResult = Result<Company>.Failure<Company>(new Error("UpdateCompanyCommandHandler.Handle", "Company not found"));

            _repositoryMock.Setup
            (
                repoMgr => repoMgr.CompanyAggregateRepository.GetByIdAsync(It.IsAny<int>(), It.IsAny<bool>())
            ).ReturnsAsync(getCompanyResult);

            _repositoryMock.Setup
            (
                repoMgr => repoMgr.CompanyAggregateRepository.Update(It.IsAny<Company>())
            ).ReturnsAsync(0);

            UpdateCompanyCommandHandler handler = new(_repositoryMock.Object);

            // Act
            Result<int> result = await handler.Handle(command, default);

            // Assert
            Assert.True(result.IsFailure);

            _repositoryMock.Verify(
                x => x.CompanyAggregateRepository.Update(It.IsAny<Company>()),
                Times.Never);
        }

        [Fact]
        public async Task Handle_Should_ReturnFailure_WhenCommand_HasInvalidData()
        {
            // Arrange
            UpdateCompanyCommand command = OrganizationTestData.GetUpdateCompanyCommandWithValidData();

            _repositoryMock.Setup
            (
                repoMgr => repoMgr.CompanyAggregateRepository.GetByIdAsync(It.IsAny<int>(), It.IsAny<bool>())
            ).ReturnsAsync(OrganizationTestData.GetCompanyResultWithValidData());

            Result<int> updateDbResult = Result<int>.Failure<int>(new Error("Repository", "Something when horribly wrong!!"));
            _repositoryMock.Setup
            (
                repoMgr => repoMgr.CompanyAggregateRepository.Update(It.IsAny<Company>())
            ).ReturnsAsync(updateDbResult);

            UpdateCompanyCommandHandler handler = new(_repositoryMock.Object);

            // Act
            Result<int> result = await handler.Handle(command, default);

            // Assert
            Assert.True(result.IsFailure);

            _repositoryMock.Verify(
                x => x.CompanyAggregateRepository.Update(It.IsAny<Company>()),
                Times.Once);
        }

    }
}