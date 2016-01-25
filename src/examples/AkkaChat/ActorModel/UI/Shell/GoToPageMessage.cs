using System;

namespace AkkaChat.ActorModel.UI.Shell
{
    public sealed class GoToPageMessage
    {
        public Type PageType { get; }
        public object Vm { get; }
        public ApperanceTransition TransitionAnimation { get; }

        public GoToPageMessage(
            Type pageType,
            object vm,
            ApperanceTransition transitionAnimation = ApperanceTransition.None)
        {
            PageType = pageType;
            Vm = vm;
            TransitionAnimation = transitionAnimation;
        }
    }

    public enum ApperanceTransition
    {
        None,
        Common,
        Continuum,
        DrillIn,
        Entrance,
        Slide
    }
}