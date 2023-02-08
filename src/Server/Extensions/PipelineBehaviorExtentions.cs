using REA.Accounting.Application.Interfaces.Messaging;
using REA.Accounting.Application.HumanResources.CreateEmployee;
using REA.Accounting.Application.HumanResources.DeleteEmployee;
using REA.Accounting.Application.HumanResources.UpdateEmployee;

namespace REA.Accounting.Server.Extensions
{
    public static class PipelineBehaviorExtentions
    {
        public static IServiceCollection AddPipelineBehaviorServices(this IServiceCollection services)
        {
            return services
                .AddScoped(typeof(CommandValidator<CreateEmployeeCommand>), typeof(CreateEmployeeBusinessRuleValidator))
                .AddScoped(typeof(CommandValidator<DeleteEmployeeCommand>), typeof(DeleteEmployeeBusinessRuleValidator))
                .AddScoped(typeof(CommandValidator<UpdateEmployeeCommand>), typeof(UpdateEmployeeBusinessRuleValidator));
        }
    }
}