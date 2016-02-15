using System;

namespace AkkaChat.Features.Menu.Item
{
    public interface IMenuItemVm
    {
        string Name { get; }
        Uri Uri { get; }
        void OnTap();
    }
}