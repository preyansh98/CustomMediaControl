using MyToolkit.Multimedia;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Media.Core;
using Windows.UI.Core;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace CustomMediaControl
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class VideosPage : Page
    {
        public VideosPage()
        {
            this.InitializeComponent();
        }    

        protected override async void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
            base.OnNavigatingFrom(e);

            await Windows.ApplicationModel.Core.CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                MyMediaPlayer?.MediaPlayer.Dispose();
            });
        }

        private async void Page_LoadedAsync(object sender, RoutedEventArgs e)
        {
            if (ApplicationView.GetForCurrentView().IsViewModeSupported(ApplicationViewMode.CompactOverlay))
            {
                CustomMediaControl.IsCompactOverlayButtonVisible = true;
                CustomMediaControl.IsCompactOverlayEnabled = true;
            }
            else
                CustomMediaControl.IsCompactOverlayButtonVisible = false;

            this.MyMediaPlayer.Source = MediaSource.CreateFromUri(new Uri("ms-appx:///Assets/fishes.wmv"));
        }

        private void ClickMeButton_Click(object sender, RoutedEventArgs e)
        {
            //do nothing
        }
    }
}
