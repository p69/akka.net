using Windows.UI.Xaml.Controls;
using AkkaChat.Features.Common;
using AkkaChat.Features.Menu;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace AkkaChat.Features.Layout
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Layout : Page
    {
        public Layout()
        {
            this.InitializeComponent();
        }

        public void ChangeMainView(ViewBase view)
        {
            MainFrameHost.Children.Clear();
            MainFrameHost.Children.Add(view);
        }

        public void SetMenuVm(IMenuVm menuVm)
        {
            Menu.Vm = menuVm;
        }
    }
}
