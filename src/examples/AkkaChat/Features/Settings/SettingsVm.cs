using AkkaChat.Features.Common;

namespace AkkaChat.Features.Settings
{
    public class SettingsVm : BindableBase, ISettingsVm
    {
        public SettingsVm()
        {
            Title = "Settings page";
            About = "Using Akka.net and MVC routing for Universal Windows Application!";
        }

        public string Title { get; }
        public string About { get; }
    }
}