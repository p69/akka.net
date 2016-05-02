namespace AkkaChat.Features.Settings
{
    public interface ISettingsVm
    {
        string Title { get; }
        void Connect(string userName);
    }
}