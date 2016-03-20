using System;
using Akka.Actor;
using AkkaChat.Features.Common.Messages.Navigation;

namespace AkkaChat.Features.Chats
{
    public class ChatsController : ReceiveActor
    {
        private IChatsView _view;
        private IChatsVm _vm;
        private IDisposable _viewActionsSub;

        public ChatsController()
        {
            Receive<OnNavigatedTo>(_ => OnNavigated());
        }

        protected override void PostStop()
        {
            base.PostStop();
            _view = null;
            _vm = null;
            _viewActionsSub?.Dispose();
        }

        private void OnNavigated()
        {
            if (_view != null)
            {
                Context.Parent.Tell(new ShowView(_view));
            }
            else
            {
                _view = new ChatsView();
                _viewActionsSub = _view.UserActions.Subscribe(OnUserAction);
                _vm = new ChatsVm();
                _view.Vm = _vm;
                Context.Parent.Tell(new ShowView(_view));
            }
        }

        private void OnUserAction(ChatsViewAction action)
        {
            switch (action)
            {
                case ChatsViewAction.ChangeTitle:
                    HandleChangeTitle();
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(action), action, null);
            }
        }

        private void HandleChangeTitle()
        {
            var rnd = new Random();
            _vm.Title = $"Chats page {rnd.Next()}";
        }
    }
}