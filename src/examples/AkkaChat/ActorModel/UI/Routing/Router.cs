using System.Collections.Generic;
using System.Linq;
using Akka.Actor;
using Akka.Util.Internal;
using AkkaChat.ActorModel.UI.Routing.Messages;

namespace AkkaChat.ActorModel.UI.Routing
{
    public class Router : ReceiveActor
    {
        private readonly IActorRef _layoutController;
        private IDictionary<string, RouteEntry> _routesMap;
        private readonly HashSet<IActorRef> _subscribers;
        private string _currentRoute;

        public Router(IActorRef layoutController)
        {
            _layoutController = layoutController;
            _subscribers = new HashSet<IActorRef>();
            Become(NotInited);
        }

        private void NotInited()
        {
            Receive<InitRouting>(msg => Init(msg));
        }

        private void Inited()
        {
            Receive<GoToMessage>(msg => HandleRoute(msg));
            Receive<SubscribeToRouteChanges>(msg => SubscribeToChanges(msg));
        }

        private void Init(InitRouting message)
        {
            _routesMap = message.Routes.ToDictionary(x => x.Path);
            _layoutController.Tell(
                new RouterReady(
                    _routesMap.Values.Select(x => new RouteItem(x.Name, x.Path)).ToList(),
                    _currentRoute));
            Become(Inited);
        }

        private void HandleRoute(GoToMessage message)
        {
            RouteEntry route;
            if (_routesMap.TryGetValue(message.Path, out route))
            {
                _currentRoute = message.Path;
                _layoutController.Tell(new OnNavigatedMessage(route, ""));
                if (_subscribers.Any())
                {
                    var routeChangedMsg = new RouteChangedMessage(_currentRoute);
                    _subscribers.ForEach(x => x.Tell(routeChangedMsg));
                }
            }
            else
            {
                Sender.Tell(new NotFoundMessage(message.Path));
            }
        }

        private void SubscribeToChanges(SubscribeToRouteChanges msg)
        {
            if (_subscribers.Contains(msg.Subscriber)) return;
            _subscribers.Add(msg.Subscriber);
        }
    }
}