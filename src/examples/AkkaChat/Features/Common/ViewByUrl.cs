using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace AkkaChat.Features.Common
{
    public class ViewByUrl : ContentControl, INotifyPropertyChanged
    {
        private string _uri;

        public string Uri
        {
            get { return _uri; }
            set
            {
                if (_uri != value)
                {
                    _uri = value;
                    OnPropertyChanged();
                    OnUriChanged();
                }
            }
        }

        private void OnUriChanged()
        {
            //TODO: ask router actor for view
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}