using Microsoft.AspNetCore.Http;

namespace REA.Accounting.Presentation
{
    public sealed class QueryParameters
    {
        public static ValueTask<QueryParameters?> BindAsync(HttpContext context)
        {
            _ = int.TryParse(context.Request.Query["PageNumber"], out var page);
            _ = int.TryParse(context.Request.Query["PageSize"], out var size);

            var result = new QueryParameters
            {
                PageNumber = page,
                PageSize = size
            };

            return ValueTask.FromResult<QueryParameters?>(result);
        }

        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
}