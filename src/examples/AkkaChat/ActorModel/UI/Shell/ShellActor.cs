using System;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Animation;
using Akka.Actor;

namespace AkkaChat.ActorModel.UI.Shell
{
    public class ShellActor : ReceiveActor
    {
        private readonly Frame _frame;

        public ShellActor(Frame frame)
        {
            _frame = frame;
            Receive<GoToPageMessage>(msg => GoToPage(msg));
        }

        private void GoToPage(GoToPageMessage message)
        {
            var transition = GetNavigationTransition(message.TransitionAnimation);
            _frame.Navigate(message.PageType, message.Vm, transition);
        }

        private NavigationTransitionInfo GetNavigationTransition(ApperanceTransition apperanceTransition)
        {
            switch (apperanceTransition)
            {
                case ApperanceTransition.None:
                    return new SuppressNavigationTransitionInfo();
                case ApperanceTransition.Common:
                    return new CommonNavigationTransitionInfo();
                case ApperanceTransition.Continuum:
                    return new ContinuumNavigationTransitionInfo();
                case ApperanceTransition.DrillIn:
                    return new DrillInNavigationTransitionInfo();
                case ApperanceTransition.Entrance:
                    return new EntranceNavigationTransitionInfo();
                case ApperanceTransition.Slide:
                    return new SlideNavigationTransitionInfo();
                default:
                    throw new ArgumentOutOfRangeException(nameof(apperanceTransition), apperanceTransition, null);
            }
        }
    }
}