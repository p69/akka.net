﻿//-----------------------------------------------------------------------
// <copyright file="InetAddressDnsResolver.cs" company="Akka.NET Project">
//     Copyright (C) 2009-2015 Typesafe Inc. <http://www.typesafe.com>
//     Copyright (C) 2013-2015 Akka.NET project <https://github.com/akkadotnet/akka.net>
// </copyright>
//-----------------------------------------------------------------------

using System.Linq;
using System.Net;
using System.Net.Sockets;
using Akka.Actor;
using Akka.Configuration;

namespace Akka.IO
{
    public class InetAddressDnsResolver : ActorBase
    {
        private readonly SimpleDnsCache _cache;
        private readonly long _positiveTtl;
        private readonly long _negativeTtl;

        public InetAddressDnsResolver(SimpleDnsCache cache, Config config)
        {
            _cache = cache;
            _positiveTtl = (long) config.GetTimeSpan("positive-ttl").TotalMilliseconds;
            _negativeTtl = (long) config.GetTimeSpan("negative-ttl").TotalMilliseconds;
        }

        protected override bool Receive(object message)
        {
            var resolve = message as Dns.Resolve;
            if (resolve != null)
            {
                var answer = _cache.Cached(resolve.Name);
                if (answer == null)
                {
                    try
                    {
#if !NETFX_CORE
                        //TODO: IP6
                        //TODO: what to do about async here?
                        answer = Dns.Resolved.Create(resolve.Name, System.Net.Dns.GetHostEntryAsync(resolve.Name).Result.AddressList.Where(x => x.AddressFamily == AddressFamily.InterNetwork));
#else
                        //TODO: fix it
                        answer = Dns.Resolved.Create(resolve.Name, new[] {new IPAddress(12345)});
#endif
                        _cache.Put(answer, _positiveTtl);
                    }
                    catch (SocketException ex)
                    {
                        if (ex.SocketErrorCode != SocketError.HostNotFound) throw;
                         answer = new Dns.Resolved(resolve.Name, Enumerable.Empty<IPAddress>(), Enumerable.Empty<IPAddress>());
                        _cache.Put(answer, _negativeTtl);
                    }
                }
                Sender.Tell(answer);
                return true;
            }
            return false;
        }
    }
}