using System;
using System.Collections.Generic;
using System.Linq;
using Foundation;
using Plugin.CrossPlatformTintedImage.iOS;
using UIKit;
using Lottie.Forms.iOS.Renderers;
using KeyboardOverlap.Forms.Plugin.iOSUnified;
using Xamarin.Forms;

namespace fondomerende.iOS
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the
    // User Interface of the application, as well as listening (and optionally responding) to
    // application events from iOS.
    [Register("AppDelegate")]
    public class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        public static string FolderPath { get; private set; }

        public static AppDelegate instance { get; private set; }
        UINotificationFeedbackGenerator _UINotificationFeedbackGenerator;
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            
            Rg.Plugins.Popup.Popup.Init();
            Forms.Init();
            AppDelegate.instance = this;
            KeyboardOverlapRenderer.Init();
            AnimationViewRenderer.Init();
            TintedImageRenderer.Init();
            FormsControls.Touch.Main.Init();
            LoadApplication(new App());
            var notification = new UINotificationFeedbackGenerator();
            notification.Prepare();
            notification.NotificationOccurred(UINotificationFeedbackType.Success);

            return base.FinishedLaunching(app, options);
            
        }
    }
}
