using Microsoft.AspNetCore.Components;

namespace REA.Accounting.Client.Components.Common
{
    public partial class ReadOnlyDetailsForm<TItem>
    {
        [Parameter] public string? FormTitle { get; set; }
        [Parameter] public RenderFragment? FormFields { get; set; }
        [Parameter] public TItem? ViewModel { get; set; }
    }
}