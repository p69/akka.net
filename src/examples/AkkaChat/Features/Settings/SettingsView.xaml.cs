using System;
using System.Reactive.Linq;
using Windows.ApplicationModel;
using Windows.UI.Xaml;
using AkkaChat.Features.Common;
using JetBrains.Annotations;

namespace AkkaChat.Features.Settings
{
    public class SettingsViewBase : ViewBase
    {
        private ISettingsVm _vm;
        private static readonly DependencyProperty VmProperty;

        static SettingsViewBase()
        {
            if (DesignMode.DesignModeEnabled)
            {
                VmProperty = DependencyProperty.Register(
                    nameof(Vm),
                    typeof(ISettingsVm),
                    typeof(SettingsViewBase),
                    new PropertyMetadata(default(ISettingsVm)));
            }
        }

        public ISettingsVm Vm
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

        public SettingsViewBase()
        {
            if (DesignMode.DesignModeEnabled)
            {
                this.RegisterPropertyChangedCallback(VmProperty, (s, e) => Vm = Vm);
                if (typeof(SettingsViewBase) == GetType())
                {
                    this.Vm = new DesignSettingsVm();
                }
            }
        }
    }

    public sealed partial class SettingsView : ISettingsView
    {
        public SettingsView()
        {
            this.InitializeComponent();
            OnUserConnect = Observable.FromEventPattern<RoutedEventHandler, RoutedEventArgs>(
                handler => ConnectButton.Click += handler,
                handler => ConnectButton.Click -= handler)
                .Select(_ => UserName.Text)
                .Where(x => !string.IsNullOrWhiteSpace(x));
        }

        public IObservable<string> OnUserConnect { get; }
    }

    public interface ISettingsView : IView
    {
        ISettingsVm Vm { get; }

        [NotNull]
        IObservable<string> OnUserConnect { get; } 
    }
}
