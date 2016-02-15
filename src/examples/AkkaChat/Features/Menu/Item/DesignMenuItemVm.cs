using System;
using AkkaChat.Features.Common;

namespace AkkaChat.Features.Menu.Item
{
    public class DesignMenuItemVm : BindableBase, IMenuItemVm
    {
        public string Name { get; } = "Menu item";
        public Uri Uri { get; } = new Uri("app:designcontroller/index");
        public void OnTap()
        {
        }
    }
}