using AkkaChat.Features.Common;

namespace AkkaChat.Features.Settings
{
    public class DesignSettingsVm : BindableBase, ISettingsVm
    {
        public string Title { get; } = "Design title";
        public void Connect(string userName)
        {
        }
    }
}