namespace REA.Accounting.SharedKernel.Utilities
{
    public static class Helpers
    {
        public static string GetExceptionMessage(Exception ex)
            => ex.InnerException == null ? ex.Message : ex.InnerException.Message;

        public static Dictionary<string, int> LoadMetaData(MetaData data)
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