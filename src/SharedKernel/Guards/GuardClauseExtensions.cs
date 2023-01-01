using REA.Accounting.SharedKernel.Exceptions;

namespace REA.Accounting.SharedKernel.Guards
{
    public static partial class GuardClauseExtensions
    {
        private static void Error(string message)
        {
            throw new DomainException(message);
        }
    }
}
