using System;
using System.Reactive.Linq;
using Windows.ApplicationModel;
using Windows.UI.Xaml;
using AkkaChat.Features.Common;

namespace AkkaChat.Features.Chats
{
    public class ChatsViewBase : ViewBase
    {
        private IChatsVm _vm;
        private static readonly DependencyProperty VmProperty;

        static ChatsViewBase()
        {
            if (DesignMode.DesignModeEnabled)
            {
                VmProperty = DependencyProperty.Register(
                    nameof(Vm),
                    typeof(IChatsVm),
                    typeof(ChatsViewBase),
                    new PropertyMetadata(default(IChatsVm)));
            }
        }

        public IChatsVm Vm
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

        public ChatsViewBase()
        {
            if (DesignMode.DesignModeEnabled)
            {
                this.RegisterPropertyChangedCallback(VmProperty, (s, e) => Vm = Vm);
                if (typeof(ChatsViewBase) == GetType())
                {
                    this.Vm = new DesignChatsVm();
                }
            }
        }
    }

    public sealed partial class ChatsView : IChatsView
    {
        public ChatsView()
        {
            this.InitializeComponent();
            Loaded += OnLoaded;
            UserActions = Observable.FromEventPattern<RoutedEventHandler, RoutedEventArgs>(
                handler => ChangeTitleButton.Click += handler,
                handler => ChangeTitleButton.Click -= handler)
                .Select(_ => ChatsViewAction.ChangeTitle);
        }

        private void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
            
        }

        public IObservable<ChatsViewAction> UserActions { get; }
    }

    public interface IChatsView : IView
    {
        IChatsVm Vm { get; set; }
        IObservable<ChatsViewAction> UserActions { get; }
    }

    public enum ChatsViewAction
    {
        ChangeTitle
    }
}
