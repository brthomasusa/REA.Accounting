using Microsoft.AspNetCore.Http;

namespace REA.Accounting.Presentation.Organization
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

        public sealed class FilterDepartmentByNameParameters
        {
            public static ValueTask<FilterDepartmentByNameParameters?> BindAsync(HttpContext context)
            {
                _ = int.TryParse(context.Request.Query["PageNumber"], out var page);
                _ = int.TryParse(context.Request.Query["PageSize"], out var size);

                var result = new FilterDepartmentByNameParameters
                {
                    PageNumber = page,
                    PageSize = size,
                    DepartmentName = context.Request.Query["DepartmentName"]
                };

                return ValueTask.FromResult<FilterDepartmentByNameParameters?>(result);
            }

            public int PageNumber { get; set; }
            public int PageSize { get; set; }
            public string? DepartmentName { get; set; }
        }
    }
}