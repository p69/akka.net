using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using AkkaChat.Features.Common;


namespace AkkaChat.Features.Menu.Item
{
    public class MenuItemBase : ViewBase
    {
        private IMenuItemVm _vm;
        private static readonly DependencyProperty VmProperty;

        static MenuItemBase()
        {
            if (DesignMode.DesignModeEnabled)
            {
                VmProperty = DependencyProperty.Register(
                    nameof(Vm),
                    typeof(IMenuItemVm),
                    typeof(MenuItemBase),
                    new PropertyMetadata(default(IMenuItemVm)));
            }
        }

        public IMenuItemVm Vm
        {
            get { return this.GetValueForXBind(VmProperty, _vm); }
            set
            {
                if (this.SetValueForXBind(VmProperty, ref _vm, value))
                {
                    this.OnPropertyChanged();
                }
            }
        }

        public MenuItemBase()
        {
            if (DesignMode.DesignModeEnabled)
            {
                this.RegisterPropertyChangedCallback(VmProperty, (s, e) => Vm = Vm);
                if (typeof(MenuItemBase) == GetType())
                {
                    this.Vm = new DesignMenuItemVm();
                }
            }
        }
    }

    public sealed partial class MenuItem
    {
        public MenuItem()
        {
            this.InitializeComponent();
        }
    }
}
