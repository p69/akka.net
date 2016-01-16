//-----------------------------------------------------------------------
// <copyright file="SocketChannel.cs" company="Akka.NET Project">
//     Copyright (C) 2009-2015 Typesafe Inc. <http://www.typesafe.com>
//     Copyright (C) 2013-2015 Akka.NET project <https://github.com/akkadotnet/akka.net>
// </copyright>
//-----------------------------------------------------------------------

using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using Akka.Actor;

namespace Akka.IO
{
    /* 
     * SocketChannel does not exists in the .NET BCL - This class is an adapter to hide the differences in CLR & JVM IO.
     * This implementation uses blocking IO calls, and then catch SocketExceptions if the socket is set to non blocking. 
     * This might introduce performance issues, with lots of thrown exceptions
     * TODO: Implements this class with .NET Async calls
     */
    public class SocketChannel 
    {
        private readonly Socket _socket;
        private IActorRef _connection;
        private bool _connected;
        private IAsyncResult _connectResult;
#if NETFX_CORE
        private static ManualResetEvent _connectDone = new ManualResetEvent(false);
        private static ManualResetEvent _sendDone = new ManualResetEvent(false);
        private static ManualResetEvent _receiveDone = new ManualResetEvent(false);
        private static ManualResetEvent _acceptDone = new ManualResetEvent(false);
#endif

        public SocketChannel(Socket socket) 
        {
            _socket = socket;
        }

        public static SocketChannel Open()
        {
            return new SocketChannel(new Socket(SocketType.Stream, ProtocolType.Tcp));
        }

        public SocketChannel ConfigureBlocking(bool block)
        {
#if !NETFX_CORE
            _socket.Blocking = block;
#endif
            return this;
        }

        public Socket Socket
        {
            get { return _socket; }
        }

        public void Register(IActorRef connection, SocketAsyncOperation? initialOps)
        {
            _connection = connection;
        }


        public virtual bool IsOpen()
        {
            return _connected;
        }

        public bool Connect(EndPoint address)
        {
#if DNXCORE50
            _socket.Connect(address);
            _connected = true;
            return true;
#endif
#if NETFX_CORE
            //TODO: check this
            var args = new SocketAsyncEventArgs
            {
                RemoteEndPoint = address
            };
            args.Completed += (sender, e) =>
            {
                _connected = e.ConnectSocket != null;
                _connectDone.Set();
            };
            _socket.ConnectAsync(args);
            _connectDone.WaitOne();
            return _connected;
#else
            _connectResult = _socket.BeginConnect(address, ar => { }, null);
            if (_connectResult.CompletedSynchronously)
            {
                _socket.EndConnect(_connectResult);
                _connected = true;
                return true;
            }
            return false;
#endif
        }

       
        public bool FinishConnect()
        {
#if DNXCORE50 || NETFX_CORE
            return _connected;
#else
            if (_connectResult.CompletedSynchronously)
                return true;
            if (_connectResult.IsCompleted)
            {
                _socket.EndConnect(_connectResult);
                _connected = true;
            }
            return _connected;
#endif
        }

        public SocketChannel Accept()
        {
#if NETFX_CORE
            //TODO: check this
            if (_socket.Connected)
            {
                var args = new SocketAsyncEventArgs();
                SocketChannel acceptedChannel = null;
                args.Completed += (sender, e) =>
                {
                    acceptedChannel = new SocketChannel(e.AcceptSocket);
                    _acceptDone.Set();
                };
                if (!_socket.AcceptAsync(args))
                {
                    return new SocketChannel(args.AcceptSocket);
                }
                _acceptDone.WaitOne();
                return acceptedChannel;
            }
            return null;
#else
    //TODO: Investigate. If we don't wait 1ms we get intermittent test failure in TcpListenerSpec.
            return _socket.Poll(1, SelectMode.SelectRead)
                ? new SocketChannel(_socket.Accept()) {_connected = true}
                : null;
#endif
        }

        public int Read(ByteBuffer buffer)
        {
#if NETFX_CORE
            if (!_socket.Connected)
            {
                return 0;
            }
            var args = new SocketAsyncEventArgs();
            var socketBuffer = new byte[buffer.Remaining];
            args.SetBuffer(socketBuffer, 0, socketBuffer.Length);
            args.Completed += (sender, e) =>
            {
                _receiveDone.Set();
            };
            if (_socket.ReceiveAsync(args))
            {
                _receiveDone.WaitOne();
            }
            buffer.Put(socketBuffer);
            return args.BytesTransferred;
#else
            if (!_socket.Poll(0, SelectMode.SelectRead))
                return 0;
            var data = new byte[buffer.Remaining];
            var length = _socket.Receive(data);
            if (length == 0)
                return -1;
            buffer.Put(data, 0, length);
            return length;
#endif
        }

        public int Write(ByteBuffer buffer)
        {
#if NETFX_CORE
            if (!_socket.Connected)
            {
                return 0;
            }
            var args = new SocketAsyncEventArgs();
            var dataToSend = new byte[buffer.Remaining];
            buffer.Get(dataToSend);
            args.SetBuffer(dataToSend, 0, dataToSend.Length);
            args.Completed += (sender, e) =>
            {
                _sendDone.Set();
            };
            if (_socket.SendAsync(args))
            {
                _sendDone.WaitOne();
            }
            return args.BytesTransferred;
#else
            if (!_socket.Poll(0, SelectMode.SelectWrite))
                return 0;
            var data = new byte[buffer.Remaining];
            buffer.Get(data);
            return _socket.Send(data);
#endif
        }

        public void Close()
        {
            _connected = false;
            _socket.Dispose();
        }
        internal IActorRef Connection { get { return _connection; } }
    }
}