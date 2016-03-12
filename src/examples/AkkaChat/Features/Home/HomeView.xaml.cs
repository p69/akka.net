using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reactive.Linq;
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

namespace AkkaChat.Features.Home
{
    public class HomeViewBase : ViewBase
    {
        private IHomeVm _vm;
        private static readonly DependencyProperty VmProperty;

        static HomeViewBase()
        {
            if (DesignMode.DesignModeEnabled)
            {
                VmProperty = DependencyProperty.Register(
                    nameof(Vm),
                    typeof(IHomeVm),
                    typeof(HomeViewBase),
                    new PropertyMetadata(default(IHomeVm)));
            }
        }

        public IHomeVm Vm
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

        public HomeViewBase()
        {
            if (DesignMode.DesignModeEnabled)
            {
                this.RegisterPropertyChangedCallback(VmProperty, (s, e) => Vm = Vm);
                if (typeof(HomeViewBase) == GetType())
                {
                    this.Vm = new DesignHomeVm();
                }
            }
        }
    }

    public sealed partial class HomeView : IHomeView
    {
        public HomeView()
        {
            this.InitializeComponent();
            Loaded += OnLoaded;
            UserActions = Observable.FromEventPattern<RoutedEventHandler, RoutedEventArgs>(
                handler => ChangeTitleButton.Click += handler,
                handler => ChangeTitleButton.Click -= handler)
                .Select(_ => HomeViewAction.ChangeTitle);
        }

        private void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
            
        }

        public IObservable<HomeViewAction> UserActions { get; }
    }

    public interface IHomeView : IView
    {
        IHomeVm Vm { get; set; }
        IObservable<HomeViewAction> UserActions { get; }
    }

    public enum HomeViewAction
    {
        ChangeTitle
    }
}
