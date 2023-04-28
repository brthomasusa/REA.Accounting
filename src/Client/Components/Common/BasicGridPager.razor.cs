#pragma warning disable CS8601, CS8602

using Microsoft.AspNetCore.Components;
using REA.Accounting.Client.Utilities;

namespace REA.Accounting.Client.Components.Common
{
    public partial class BasicGridPager
    {
        private const string PREVIOUS = "previous";
        private const string NEXT = "next";
        private string? currentPage = "1";
        private int totalPages;
        private int totalCount;
        private int pageSize;

        [Parameter] public MetaData? MetaData { get; set; }
        [Parameter] public Func<int, int, Task>? PagerChangedEventHandler { get; set; }

        protected override void OnParametersSet()
        {
            if (MetaData is not null)
            {
                currentPage = MetaData.CurrentPage.ToString();
                totalPages = MetaData.TotalPages;
                totalCount = MetaData.TotalCount;
                pageSize = MetaData.PageSize;
            }
        }

        private bool IsActive(string page) => currentPage == page;

        private bool IsPageNavigationDisabled(string navigation)
        {
            if (navigation.Equals(PREVIOUS))
            {
                return currentPage.Equals("1");
            }
            else if (navigation.Equals(NEXT))
            {
                return currentPage.Equals(totalPages.ToString());
            }
            return false;
        }

        private async Task Previous()
        {
            if (currentPage != null)
            {
                int currentPageAsInt = int.Parse(currentPage);

                if (currentPageAsInt > 1)
                {
                    currentPage = (currentPageAsInt - 1).ToString();
                    await PagerChangedEventHandler!.Invoke(currentPageAsInt - 1, pageSize);
                }
            }
            else
            {
                Console.WriteLine("BasicGridPager.Previous: currentPage property is null!");
            }

        }

        private async Task Next()
        {
            if (currentPage != null)
            {
                var currentPageAsInt = int.Parse(currentPage);

                if (currentPageAsInt < totalPages)
                {
                    currentPage = (currentPageAsInt + 1).ToString();
                    await PagerChangedEventHandler!.Invoke(currentPageAsInt + 1, pageSize);
                }
            }
            else
            {
                Console.WriteLine("BasicGridPager.Next: currentPage property is null!");
            }
        }

        private async Task SetActive(string? page)
        {
            if (page is not null)
            {
                currentPage = page;

                if (PagerChangedEventHandler is not null)
                    await PagerChangedEventHandler.Invoke(int.Parse(currentPage), pageSize);
            }
            else
            {
                Console.WriteLine($"BasicGridPager.SetActive(string page) called with null parameter!");
            }
        }

        private async Task OnPageSizeChanged(int value)
        {
            pageSize = value;
            await PagerChangedEventHandler!.Invoke(1, pageSize);
        }
    }
}