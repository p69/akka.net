using Akka.Actor;
using AkkaChat.Features.Common.Messages.Navigation;

namespace AkkaChat.Features.About
{
    public class AboutContoller : ReceiveActor
    {
        private Index _view;
        private IIndexVm _vm;

        public AboutContoller()
        {
            Receive<OnNavigatedTo>(_ => OnNavigated());
        }

        private void OnNavigated()
        {
            if (_view == null)
            {
                _view = new Index();
            }
            _vm = new IndexVm();
            _view.Vm = _vm;
            Context.Parent.Tell(new ShowView(_view));
        }
    }
}