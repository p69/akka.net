//-----------------------------------------------------------------------
// <copyright file="DatagramChannel.cs" company="Akka.NET Project">
//     Copyright (C) 2009-2015 Typesafe Inc. <http://www.typesafe.com>
//     Copyright (C) 2013-2015 Akka.NET project <https://github.com/akkadotnet/akka.net>
// </copyright>
//-----------------------------------------------------------------------

using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace Akka.IO
{
    public class DatagramChannel : SocketChannel
    {
#if NETFX_CORE
        private static ManualResetEvent _sendDone = new ManualResetEvent(false);
        private static ManualResetEvent _receiveDone = new ManualResetEvent(false);
#endif
        private DatagramChannel(Socket socket) : base(socket)
        {
            
        }

        public static DatagramChannel Open()
        {
            return new DatagramChannel(new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp));
        }

        public override bool IsOpen()
        {
            return true;
        }

        public int Send(ByteBuffer buffer, EndPoint target)
        {
#if NETFX_CORE
            if (!Socket.Connected)
            {
                return 0;
            }
            var args = new SocketAsyncEventArgs {RemoteEndPoint = target};
            var dataToSend = new byte[buffer.Remaining];
            buffer.Get(dataToSend);
            args.SetBuffer(dataToSend, 0, dataToSend.Length);
            args.Completed += (sender, e) =>
            {
                _sendDone.Set();
            };
            if (Socket.SendToAsync(args))
            {
                _sendDone.WaitOne();
            }
            return args.BytesTransferred;
#else
            if (!Socket.Poll(0, SelectMode.SelectWrite))
                return 0;
            var data = new byte[buffer.Remaining];
            buffer.Get(data);
            return Socket.SendTo(data, target);
#endif
        }

        public EndPoint Receive(ByteBuffer buffer)
        {
#if NETFX_CORE
            if (!Socket.Connected)
            {
                return null;
            }
            var args = new SocketAsyncEventArgs {RemoteEndPoint = Socket.LocalEndPoint};
            var socketBuffer = new byte[buffer.Remaining];
            args.SetBuffer(socketBuffer, 0, socketBuffer.Length);
            args.Completed += (sender, e) =>
            {
                _receiveDone.Set();
            };
            if (Socket.ReceiveAsync(args))
            {
                _receiveDone.WaitOne();
            }
            buffer.Put(socketBuffer);
            return args.RemoteEndPoint;
#else
            if (!Socket.Poll(0, SelectMode.SelectRead))
                return null;
            var ep = Socket.LocalEndPoint;
            var data = new byte[buffer.Remaining];
            var length = Socket.ReceiveFrom(data, ref ep);
            buffer.Put(data, 0, length);
            return ep;
#endif
        }
    }
}