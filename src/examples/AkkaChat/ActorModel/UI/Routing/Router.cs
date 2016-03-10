using System.Collections.Generic;
using System.Linq;
using Akka.Actor;
using Akka.Util.Internal;
using AkkaChat.ActorModel.UI.Routing.Messages;

namespace AkkaChat.ActorModel.UI.Routing
{
    public class Router : ReceiveActor
    {
        private IDictionary<string, RouteEntry> _routesMap;
        private IDictionary<string, IActorRef> _routeeCache;
        private readonly HashSet<IActorRef> _subscribers;
        private string _currentRoute;

        public Router()
        {
            _subscribers = new HashSet<IActorRef>();
            Become(NotInited);
        }

        private void NotInited()
        {
            Receive<InitRouting>(msg => Init(msg));
        }

        private void Inited()
        {
            Receive<RouteMessage>(msg => HandleRoute(msg));
            Receive<SubscribeToRouteChanges>(msg => SubscribeToChanges(msg));
        }

        private void Init(InitRouting message)
        {
            _routesMap = message.Routes.ToDictionary(x => x.Path);
            _routeeCache = new Dictionary<string, IActorRef>(message.Routes.Count);
            Become(Inited);
        }

        private void HandleRoute(RouteMessage message)
        {
            RouteEntry route;
            if (_routesMap.TryGetValue(message.Path, out route))
            {
                IActorRef routee;
                if (message.CacheActor)
                {
                    if (!_routeeCache.TryGetValue(message.Path, out routee))
                    {
                        routee = Context.ActorOf(route.Props, route.Path);
                        _routeeCache.Add(message.Path, routee);
                    }
                }
                else
                {
                    routee = Context.ActorOf(route.Props);
                }
                _currentRoute = message.Path;
                routee.Tell(new NavigateMessage());
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
            Sender.Tell(
                new AllRoutesMessageReply(
                    _routesMap.Values.Select(x => new RouteItem(x.Name, x.Path)).ToList(),
                    _currentRoute));
        }
    }
}