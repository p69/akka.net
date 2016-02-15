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


namespace AkkaChat.Features.About
{
    public class IndexBase : ViewBase
    {
        private IIndexVm _vm;
        private static readonly DependencyProperty VmProperty;

        static IndexBase()
        {
            if (DesignMode.DesignModeEnabled)
            {
                VmProperty = DependencyProperty.Register(
                    nameof(Vm),
                    typeof(IIndexVm),
                    typeof(IndexBase),
                    new PropertyMetadata(default(IIndexVm)));
            }
        }

        public IIndexVm Vm
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

        public IndexBase()
        {
            if (DesignMode.DesignModeEnabled)
            {
                this.RegisterPropertyChangedCallback(VmProperty, (s, e) => Vm = Vm);
                if (typeof(IndexBase) == GetType())
                {
                    this.Vm = new DesignIndexVm();
                }
            }
        }
    }

    public sealed partial class Index
    {
        public Index()
        {
            this.InitializeComponent();
        }
    }
}
