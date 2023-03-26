namespace REA.Accounting.Client.Utilities
{
    public static class Helpers
    {
        public static string GetExceptionMessage(Exception ex)
            => ex.InnerException == null ? ex.Message : ex.InnerException.Message;
    }
}