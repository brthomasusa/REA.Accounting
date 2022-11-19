#pragma warning disable CS8618

using REA.Accounting.SharedKernel;
using REA.Accounting.SharedKernel.Utilities;

namespace REA.Accounting.Infrastructure.Persistence.DataModels.Agents
{
    public class EconomicAgent : Entity<int>
    {
        protected EconomicAgent() { }

        public EconomicAgent(int id, EconomicAgentType agentType)
            : this()
        {
            Id = id;
            AgentType = agentType;
        }

        public EconomicAgentType AgentType { get; protected set; }
    }


    public abstract class EconomicAgentType : Enumeration<EconomicAgentType>
    {
        protected EconomicAgentType(int value, string name)
            : base(value, name)
        {

        }

        public static readonly EconomicAgentType Company = new CompanyAgentType();
        public static readonly EconomicAgentType Employee = new EmployeeAgentType();
        public static readonly EconomicAgentType Customer = new CustomerAgentType();
        public static readonly EconomicAgentType Vendor = new VendorAgentType();
        public static readonly EconomicAgentType Financier = new FinancierAgentType();

        public abstract string EconomicEvents { get; }

        // public abstract ValidationResult Validate();

        private sealed class CompanyAgentType : EconomicAgentType
        {
            public CompanyAgentType() : base(1, "Company")
            {

            }

            // Return list of economic events raised by company, such as, hire employee, pay employee.
            public override string EconomicEvents { get; }
        }

        private sealed class EmployeeAgentType : EconomicAgentType
        {
            public EmployeeAgentType() : base(2, "Employee")
            {

            }

            public override string EconomicEvents { get; }
        }

        private sealed class CustomerAgentType : EconomicAgentType
        {
            public CustomerAgentType() : base(3, "Customer")
            {

            }

            public override string EconomicEvents { get; }
        }

        private sealed class VendorAgentType : EconomicAgentType
        {
            public VendorAgentType() : base(4, "Vendor")
            {

            }

            public override string EconomicEvents { get; }
        }

        private sealed class FinancierAgentType : EconomicAgentType
        {
            public FinancierAgentType() : base(5, "Financier")
            {

            }

            public override string EconomicEvents { get; }
        }
    }
}