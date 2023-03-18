using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace ReaAccountingSys.Client.Components.Common
{
    public partial class SingleFieldGridSearch
    {
        [Parameter] public string? PlaceHolderText { get; set; }
        [Parameter] public EventCallback<string> OnSearchTermChangedEventHandler { get; set; }
    }
}