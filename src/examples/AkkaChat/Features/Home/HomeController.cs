using Akka.Actor;
using AkkaChat.ActorModel.UI.Routing.Messages;
using AkkaChat.Bootstrapping;
using AkkaChat.Features.Layout.Messages;

namespace AkkaChat.Features.Home
{
    public class HomeController : ReceiveActor
    {
        private Index _view;
        private IIndexVm _vm;

        public HomeController()
        {
            Receive<NavigateMessage>(_ => OnNavigated());
        }

        private void OnNavigated()
        {
            if (_view == null)
            {
                _view = new Index();
            }
            _vm = new IndexVm();
            _view.Vm = _vm;
            AppRoot.LayoutController.Tell(new ShowView(_view));
        }
    }
}