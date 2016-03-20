using AkkaChat.Features.Common;

namespace AkkaChat.Features.Settings
{
    public class DesignSettingsVm : BindableBase, ISettingsVm
    {
        public string Title { get; } = "Design title";
        public string About { get; } = "Design long about description. Wow, magic.";
    }
}