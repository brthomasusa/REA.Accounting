using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace REA.Accounting.Client.Components.Common
{
    public partial class SingleFieldGridSearch
    {
        [Parameter] public string? PlaceHolderText { get; set; }
        [Parameter] public EventCallback<string> OnSearchTermChangedEventHandler { get; set; }

        private string SearchTerm = string.Empty;

        private async Task OnSearchTermChanged(KeyboardEventArgs e)
        {
            if (e.Code == "Enter" || e.Code == "NumpadEnter")
            {
                if (!string.IsNullOrEmpty(SearchTerm))
                {
                    await OnSearchTermChangedEventHandler.InvokeAsync(SearchTerm);
                }
            }
        }
    }
}