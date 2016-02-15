using System;
using AkkaChat.Features.Common;

namespace AkkaChat.Features.Menu.Item
{
    public class MenuItemVm : BindableBase, IMenuItemVm
    {
        public string Name { get; }
        public Uri Uri { get; }
        public void OnTap()
        {
            //TODO: navigate
        }
    }
}