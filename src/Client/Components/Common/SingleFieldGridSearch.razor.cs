using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace REA.Accounting.Client.Components.Common
{
    public partial class SingleFieldGridSearch
    {
        private string SearchTerm = string.Empty;
        [Parameter] public string? CachedSearchTerm { get; set; }
        [Parameter] public string? PlaceHolderText { get; set; }
        [Parameter] public EventCallback<string> OnSearchTermChangedEventHandler { get; set; }

        private async Task OnSearchTermChanged(KeyboardEventArgs e)
        {
            if (e.Code == "Enter" || e.Code == "NumpadEnter")
            {
                await OnSearchButtonClick();
                await InvokeAsync(StateHasChanged);
            }
        }

        private async Task OnSearchButtonClick()
        {
            await OnSearchTermChangedEventHandler.InvokeAsync(SearchTerm);
        }

        private async Task OnClearSearch()
        {
            SearchTerm = string.Empty;
            await OnSearchButtonClick();
        }

        private string ClearSearchButtonClass()
            => string.IsNullOrEmpty(CachedSearchTerm) ? "btn btn-sm btn-success" : "btn btn-sm btn-secondary";
    }
}