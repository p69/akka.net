using AkkaChat.Features.Common;

namespace AkkaChat.Features.Home
{
    public class HomeVm : BindableBase, IHomeVm
    {
        private string _title;

        public HomeVm()
        {
            _title = "Home page";
        }

        public string Title
        {
            get { return _title; }
            set
            {
                if (_title != value)
                {
                    _title = value;
                    OnPropertyChanged();
                }
            }
        }
    }
}