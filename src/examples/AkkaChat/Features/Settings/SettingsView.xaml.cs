using Windows.ApplicationModel;
using Windows.UI.Xaml;
using AkkaChat.Features.Common;

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

    public sealed partial class SettingsView
    {
        public SettingsView()
        {
            this.InitializeComponent();
        }
    }
}
