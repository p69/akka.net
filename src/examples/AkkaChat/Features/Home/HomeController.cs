using System;
using Akka.Actor;
using AkkaChat.ActorModel.UI.Routing.Messages;
using AkkaChat.Bootstrapping;
using AkkaChat.Features.Layout.Messages;

namespace AkkaChat.Features.Home
{
    public class HomeController : ReceiveActor
    {
        private IHomeView _view;
        private IHomeVm _vm;
        private IDisposable _viewActionsSub;

        public HomeController()
        {
            Receive<NavigateMessage>(_ => OnNavigated());
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
                AppRoot.LayoutController.Tell(new ShowView(_view));
            }
            else
            {
                _view = new HomeView();
                _viewActionsSub = _view.UserActions.Subscribe(OnUserAction);
                _vm = new HomeVm();
                _view.Vm = _vm;
                AppRoot.LayoutController.Tell(new ShowView(_view));
            }
        }

        private void OnUserAction(HomeViewAction action)
        {
            switch (action)
            {
                case HomeViewAction.ChangeTitle:
                    HandleChangeTitle();
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(action), action, null);
            }
        }

        private void HandleChangeTitle()
        {
            var rnd = new Random();
            _vm.Title = $"Home page {rnd.Next()}";
        }
    }
}