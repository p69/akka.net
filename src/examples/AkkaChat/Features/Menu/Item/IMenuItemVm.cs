namespace AkkaChat.Features.Menu.Item
{
    public interface IMenuItemVm
    {
        string Name { get; }
        void OnTap();
    }
}