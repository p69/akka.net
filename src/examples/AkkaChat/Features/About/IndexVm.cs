using AkkaChat.Features.Common;

namespace AkkaChat.Features.About
{
    public class IndexVm : BindableBase, IIndexVm
    {
        public IndexVm()
        {
            Title = "About page";
            About = "Using Akka.net and MVC routing for Universal Windows Application!";
        }

        public string Title { get; }
        public string About { get; }
    }
}