using Akka.Actor;
using AkkaChat.ActorModel.UI.Routing.Messages;
using AkkaChat.Bootstrapping;
using AkkaChat.Features.Layout.Messages;

namespace AkkaChat.Features.About
{
    public class AboutContoller : ReceiveActor
    {
        private Index _view;
        private IIndexVm _vm;

        public AboutContoller()
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