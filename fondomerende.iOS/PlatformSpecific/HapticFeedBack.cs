using System;
using System.Threading.Tasks;
using fondomerende.Main.Utilities;
using UIKit;
using Xamarin.Essentials;

namespace fondomerende.iOS.PlatformSpecific
{
    public class HapticFeedBack : HapticFeedbackGen
    {
        public HapticFeedBack()
        {
        }

        public async void HapticFeedbackGenSuccessAsync()
        {
            var feedback = new UINotificationFeedbackGenerator();
            feedback.Prepare();
            switch ((UIDevice.CurrentDevice.ValueForKey(new Foundation.NSString("_feedbackSupportLevel")) as Foundation.NSNumber).Int16Value)
            {
                case 1:
                    using (var feedbackLow = new UISelectionFeedbackGenerator())
                    {
                        feedbackLow.SelectionChanged();
                    }
                    break;
                case 2:
                    {
                        feedback.NotificationOccurred(UINotificationFeedbackType.Success);
                    }
                    break;
                default:
                    Vibration.Vibrate(40);
                    await Task.Delay(20);
                    Vibration.Vibrate(40);
                    break;
            }
        }
    }
}
