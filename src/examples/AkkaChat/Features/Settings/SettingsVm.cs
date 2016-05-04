using Akka.Actor;
using AkkaChat.Features.Common;
using AkkaChat.Features.Settings.Messages;
using JetBrains.Annotations;

namespace AkkaChat.Features.Settings
{
    public class SettingsVm : BindableBase, ISettingsVm
    {
        [NotNull]
        private readonly IActorRef _controller;

        private string _userName;
        private bool _isOnline;
        public SettingsVm([NotNull] IActorRef controller)
        {
            _controller = controller;
            Title = "Settings page";
        }

        public string Title { get; }
        public void Join()
        {
            if (!string.IsNullOrWhiteSpace(UserName))
            {
                _controller.Tell(new JoinChatMessage(UserName));
            }
        }

        public bool IsOnline
        {
            get { return _isOnline; }
            set
            {
                if (_isOnline != value)
                {
                    _isOnline = value;
                    OnPropertyChanged();
                }
            }
        }

        public string UserName
        {
            get { return _userName; }
            set
            {
                if (_userName != value)
                {
                    _userName = value;
                    OnPropertyChanged();
                }
            }
        }
    }
}