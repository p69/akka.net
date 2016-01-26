using System;
using Windows.UI.Xaml.Controls;

namespace AkkaChat.ActorModel.UI.Shell
{
    public sealed class AppLoadedMessage
    {
        public Frame Frame { get; }
        public TimeSpan LoadingTime { get; }

        public AppLoadedMessage(Frame frame, TimeSpan loadingTime)
        {
            Frame = frame;
            LoadingTime = loadingTime;
        }
    }
}