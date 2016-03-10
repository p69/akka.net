using AkkaChat.Features.Common;

namespace AkkaChat.Features.Menu.Item
{
    public class DesignMenuItemVm : BindableBase, IMenuItemVm
    {
        public string Name { get; } = "Menu item";
        public void OnTap()
        {
        }

        public bool IsSelected { get; set; } = true;
        public string Path { get; } = "/path";
    }
}