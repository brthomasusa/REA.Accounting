

namespace REA.Accounting.SharedKernel.Guards
{
    public static partial class GuardClauseExtensions
    {
        public static DateTime DefaultDateTime(this IGuardClause guardClause, DateTime input, string parameterName = "value", string message = null!)
        {
            if (input == default)
            {
                Error(message ?? $"Required input '{parameterName}' is missing.");
            }
            return input;
        }

        public static DateOnly DefaultDateTime(this IGuardClause guardClause, DateOnly input, string parameterName = "value", string message = null!)
        {
            if (input == default)
            {
                Error(message ?? $"Required input '{parameterName}' is missing.");
            }
            return input;
        }
    }
}