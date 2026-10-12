using Microsoft.AspNetCore.Components;

namespace jumptest.Components
{
    public class TabItem
    {
        public string Title { get; set; } = string.Empty;
        public RenderFragment? Content { get; set; }
        public string? Icon { get; set; }
        public bool Disabled { get; set; } = false;
    }
}
