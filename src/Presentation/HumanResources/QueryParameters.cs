using Microsoft.AspNetCore.Http;

namespace REA.Accounting.Presentation.HumanResources
{
    public sealed class QueryParameters
    {
        public sealed class PaginationParameters
        {
            public static ValueTask<PaginationParameters?> BindAsync(HttpContext context)
            {
                _ = int.TryParse(context.Request.Query["PageNumber"], out var page);
                _ = int.TryParse(context.Request.Query["PageSize"], out var size);

                var result = new PaginationParameters
                {
                    PageNumber = page,
                    PageSize = size
                };

                return ValueTask.FromResult<PaginationParameters?>(result);
            }

            public int PageNumber { get; set; }
            public int PageSize { get; set; }
        }

        public sealed class FilterEmployeesByNameParameters
        {
            public static ValueTask<FilterEmployeesByNameParameters?> BindAsync(HttpContext context)
            {
                _ = int.TryParse(context.Request.Query["PageNumber"], out var page);
                _ = int.TryParse(context.Request.Query["PageSize"], out var size);

                var result = new FilterEmployeesByNameParameters
                {
                    PageNumber = page,
                    PageSize = size,
                    LastName = context.Request.Query["LastName"]
                };

                return ValueTask.FromResult<FilterEmployeesByNameParameters?>(result);
            }

            public int PageNumber { get; set; }
            public int PageSize { get; set; }
            public string? LastName { get; set; }
        }
    }
}