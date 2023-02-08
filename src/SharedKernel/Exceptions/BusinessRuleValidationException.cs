namespace REA.Accounting.SharedKernel.Exceptions
{
    public class BusinessRuleValidationException : Exception
    {
        public BusinessRuleValidationException(string message, Exception ex) : base(message, ex)
        {

        }

        public BusinessRuleValidationException(string message) : base(message)
        {

        }
    }
}