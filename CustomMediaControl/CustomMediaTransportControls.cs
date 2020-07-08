using MyToolkit.Multimedia;
using MyToolkit.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel.Core;
using Windows.UI.Core;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Documents;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;

// The Templated Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234235

namespace CustomMediaControl
{
    public sealed class CustomMediaTransportControls : MediaTransportControls
    {
        public CustomMediaTransportControls()
        {
            this.DefaultStyleKey = typeof(CustomMediaTransportControls);
        }

        private AppBarButton compactOverlayButton;
        CommandBar commandBar;

        //overriding OnApplyTemplate
        protected override void OnApplyTemplate()
        {
            //Setting Controls variables for Buttons and MenuFlyoutItem
            commandBar = GetTemplateChild("MediaControlsCommandBar") as CommandBar;
            compactOverlayButton = GetTemplateChild("CompactOverlayButton") as AppBarButton;

            Window.Current.VisibilityChanged -= this.minimized;
            Window.Current.VisibilityChanged += this.minimized;

            if (CompactOverlayButtonVisibility != Visibility.Visible)
                commandBar.PrimaryCommands.Remove(compactOverlayButton);
            else if (!commandBar.PrimaryCommands.Contains(compactOverlayButton))
                commandBar.PrimaryCommands.Insert(4, compactOverlayButton);
            compactOverlayButton.Visibility = Visibility.Visible;

            base.OnApplyTemplate();
        }

        private void minimized(object sender, VisibilityChangedEventArgs e)
        {
            if (!e.Visible)
            {
                this.compactOverlayButton.Command?.Execute(this.compactOverlayButton.CommandParameter);
            }
        }

        //Button click event for CompactOverlayButton to Create a Frame in CompactOverlay mode
        public async void CompactOverlayButton_ClickAsync(object sender, RoutedEventArgs e)
        {
            int compactViewId = ApplicationView.GetForCurrentView().Id;      //Initializing compactViewId to the Current View ID
            await CoreApplication.CreateNewView().Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                var frame = new Frame();
                compactViewId = ApplicationView.GetForCurrentView().Id;
                frame.Navigate(typeof(VideosPage));
                Window.Current.Content = frame;
                Window.Current.Activate();
                ApplicationView.GetForCurrentView().Title = "";
            });
            bool viewShown = await ApplicationViewSwitcher.TryShowAsViewModeAsync(compactViewId, ApplicationViewMode.CompactOverlay);
        }

        public static readonly DependencyProperty CompactOverlayButtonVisibilityProperty = DependencyProperty.Register(nameof(CompactOverlayButtonVisibility), typeof(Visibility), typeof(CustomMediaTransportControls), new PropertyMetadata(Visibility.Visible, OnVisibisityChanged));

        public Visibility CompactOverlayButtonVisibility
        {
            get
            {
                return (Visibility)GetValue(CompactOverlayButtonVisibilityProperty);
            }
            set
            {
                SetValue(CompactOverlayButtonVisibilityProperty, value);
            }
        }

        internal static void OnVisibisityChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (((CustomMediaTransportControls)d).compactOverlayButton != null)
            {
                if ((Visibility)e.NewValue != Visibility.Visible)
                    ((CustomMediaTransportControls)d).commandBar.PrimaryCommands.Remove(((CustomMediaTransportControls)d).compactOverlayButton);
                else if (!((CustomMediaTransportControls)d).commandBar.PrimaryCommands.Contains(((CustomMediaTransportControls)d).compactOverlayButton))
                    ((CustomMediaTransportControls)d).commandBar.PrimaryCommands.Insert(4, ((CustomMediaTransportControls)d).compactOverlayButton);
                ((CustomMediaTransportControls)d).compactOverlayButton.Visibility = Visibility.Visible;
            }
        }
    }
}

