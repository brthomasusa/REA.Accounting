#pragma warning disable CS8618

namespace REA.Accounting.SharedKernel.Utilities
{
    public class OperationResult<TResult>
    {
        private OperationResult() { }

        public bool Success { get; private set; }
        public TResult Result { get; private set; }
        public string? NonSuccessMessage { get; private set; }
        public Exception Exception { get; private set; }

        public static OperationResult<TResult> CreateSuccessResult(TResult result)
            => new OperationResult<TResult> { Success = true, Result = result };

        public static OperationResult<TResult> CreateFailure(string nonSuccessMessage)
            => new OperationResult<TResult> { Success = false, NonSuccessMessage = nonSuccessMessage };

        public static OperationResult<TResult> CreateFailure(Exception ex) =>
            new OperationResult<TResult>
            {
                Success = false,
                NonSuccessMessage = String.Format("{0}{1}{1}{2}", ex.Message, Environment.NewLine, ex.StackTrace),
                Exception = ex
            };
    }
}